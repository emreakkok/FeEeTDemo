using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using FeEeTDemo.Models;

namespace FeEeTDemo.Controllers
{
    public class UserController : Controller
    {
        EventManager em = new EventManager(new EfEventRepository());
        //profil
        public IActionResult Index()
        {
            var _usermail = User.Identity.Name;
            Context c = new Context();
            //var _username = c.Users.Where(x => x.Email == _usermail).Select(y => y.Username).FirstOrDefault();
            var user = c.Users.FirstOrDefault(x => x.Email == _usermail);

            if (user != null)
            {
                ViewData["UserId"] = user.Id;
                ViewData["Username"] = user.Username;
                ViewData["Email"] = user.Email;
                ViewData["Password"] = "**********";  
                ViewData["CreatedAt"] = user.CreatedAt.ToString("yyyy-MM-dd");

            }
            return View();
        }
        //listeleme
        public async Task<IActionResult> EventList()
        {
            var userEmail = User.Identity.Name;
            using (var c = new Context())
            {
                var user = c.Users.FirstOrDefault(x => x.Email == userEmail);
                if (user != null)
                {
                    var userEvents = em.GetEventsByUserId(user.Id);
                    return View(userEvents);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        //oluşturma

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View(new CreateEventViewModel
            {
                DateTimeOptions = new List<DateTime> { DateTime.Now }
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var currentUserEmail = User.Identity.Name;
            using (var c = new Context())
            {
                var currentUser = c.Users.FirstOrDefault(x => x.Email == currentUserEmail);
                if (currentUser == null) return RedirectToAction("Index", "Login");

                // Yeni event oluştur
                var newEvent = new Event
                {
                    Title = model.Title,
                    Description = model.Description,
                    Location = model.Location,
                    CreatedAt = DateTime.Now,
                    CreatedById = currentUser.Id,
                    DateTimeOptions = model.DateTimeOptions.Select(dto => new DateTimeOption
                    {
                        DateTime = dto
                    }).ToList()
                };

                em.TAdd(newEvent);

                return RedirectToAction("EventDetails", new { id = newEvent.Id });
            }
        }


        //detaylar
        public async Task<IActionResult> EventDetails(int id)
        {
            var currentUserEmail = User.Identity.Name;
            using (var c = new Context())
            {
                var currentUser = c.Users.FirstOrDefault(x => x.Email == currentUserEmail);
                if (currentUser == null) return RedirectToAction("Index", "Login");

                var eventDetails = em.GetEventDetailsById(id);
                if (eventDetails == null) return NotFound();

                ViewData["CurrentUserId"] = currentUser.Id;
                return View(eventDetails);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> SubmitVote(int dateTimeOptionId)
        {
            var currentUserEmail = User.Identity.Name;
            using (var c = new Context())
            {
                // Kullanıcıyı bul
                var currentUser = c.Users.FirstOrDefault(x => x.Email == currentUserEmail);
                if (currentUser == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
                }

                // Kullanıcının oylama geçmişini kontrol et
                var existingVote = c.SurveyResponses
                    .FirstOrDefault(sr => sr.UserId == currentUser.Id && sr.DateTimeOptionId == dateTimeOptionId);

                if (existingVote != null)
                {
                    // Eğer daha önce oy verilmişse, oyu kaldır
                    c.SurveyResponses.Remove(existingVote);
                }
                else
                {
                    // Yeni oy ekle
                    var newVote = new SurveyResponse
                    {
                        UserId = currentUser.Id,
                        DateTimeOptionId = dateTimeOptionId
                    };
                    c.SurveyResponses.Add(newVote);
                }

                // Değişiklikleri kaydet
                await c.SaveChangesAsync();

                // Güncel oy sayısını ve kimlerin oy verdiğini al
                var voteCount = c.SurveyResponses.Count(sr => sr.DateTimeOptionId == dateTimeOptionId);
                var usersVoted = c.SurveyResponses
                                    .Where(sr => sr.DateTimeOptionId == dateTimeOptionId)
                                    .Select(sr => sr.User.Username)
                                    .ToList();

                // Sonuçları döndür
                return Json(new
                {
                    success = true,
                    voteCount = voteCount,
                    usersVoted = usersVoted
                });
            }
        }

        //düzenleme 
        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            var currentUserEmail = User.Identity.Name;
            using (var c = new Context())
            {
                var currentUser = c.Users.FirstOrDefault(x => x.Email == currentUserEmail);
                if (currentUser == null) return RedirectToAction("Index", "Login");

                var event_ = em.GetByID_DTO(id);
                if (event_ == null || event_.CreatedById != currentUser.Id)
                    return RedirectToAction("EventList");

                return View(event_);
            }
        }

        [HttpPost]
        public IActionResult EditEvent(Event event_)
        {
            var currentUserEmail = User.Identity.Name;
            using (var context = new Context())
            {
                var currentUser = context.Users.FirstOrDefault(x => x.Email == currentUserEmail);
                if (currentUser == null) return RedirectToAction("Index", "Login");

                var existingEvent = context.Events
                    .Include(e => e.DateTimeOptions)
                        .ThenInclude(dto => dto.SurveyResponses)
                    .FirstOrDefault(e => e.Id == event_.Id);

                if (existingEvent == null || existingEvent.CreatedById != currentUser.Id)
                    return RedirectToAction("EventList");

                foreach (var option in existingEvent.DateTimeOptions)
                {
                    context.SurveyResponses.RemoveRange(option.SurveyResponses);
                }
                context.DateTimeOptions.RemoveRange(existingEvent.DateTimeOptions);

                existingEvent.Title = event_.Title;
                existingEvent.Description = event_.Description;
                existingEvent.Location = event_.Location;
                existingEvent.DateTimeOptions = event_.DateTimeOptions;

                context.SaveChanges();
                return RedirectToAction("EventDetails", new { id = existingEvent.Id });
            }
        }

        //silme
        public IActionResult DeleteEvent(int id)
        {
            var currentUserEmail = User.Identity.Name;
            using (var c = new Context())
            {
                var currentUser = c.Users.FirstOrDefault(x => x.Email == currentUserEmail);
                if (currentUser == null) return RedirectToAction("Index", "Login");

                var event_ = c.Events
                    .Include(e => e.DateTimeOptions)
                        .ThenInclude(dto => dto.SurveyResponses)
                    .FirstOrDefault(e => e.Id == id);

                if (event_ != null && event_.CreatedById == currentUser.Id)
                {
                    foreach (var option in event_.DateTimeOptions)
                    {
                        c.SurveyResponses.RemoveRange(option.SurveyResponses);
                    }
                    c.DateTimeOptions.RemoveRange(event_.DateTimeOptions);
                    c.Events.Remove(event_);
                    c.SaveChanges();
                }
            }
            return RedirectToAction("EventList");
        }


        //çıkış
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // Giriş sayfasına yönlendirme
        }
    }
}
