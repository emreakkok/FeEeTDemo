using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class EventManager : IEventService
    {
        IEventDal _eventDal;

        public EventManager(IEventDal eventDal)
        {
            _eventDal = eventDal;
        }

        public Event TGetById(int id)
        {
            return _eventDal.GetByID(id);
        }

        public List<Event> GetList()
        {
            return _eventDal.GetListAll();
        }

        public void TAdd(Event t)
        {
            _eventDal.Insert(t);
        }

        public void TDelete(Event t)
        {
            _eventDal.Delete(t);
        }

        public void TUpdate(Event t)
        {
            _eventDal.Update(t);
        }

        //DTO DAHİL GETİRME
        public List<Event> GetListAll_DTO()
        {
            using (var c = new Context())
            {

                return c.Events.Include(x => x.DateTimeOptions).ToList();
            }
        }

        public Event GetByID_DTO(int id)
        {
            using (var c = new Context())
            {
                return c.Events.Include(x => x.DateTimeOptions).FirstOrDefault(e => e.Id == id);
            }
        }

        public List<Event> GetEventsByUserId(int userId)
        {
            using (var c = new Context())
            {
                return c.Events
                    .Include(x => x.DateTimeOptions)
                    .Include(x => x.Participants)
                    .Include(x => x.CreatedBy)
                    .Where(e => e.CreatedById == userId || e.Participants.Any(p => p.Id == userId))
                    .OrderByDescending(e => e.CreatedAt)
                    .ToList();
            }
        }

        public Event GetEventDetailsById(int id)
        {
            using (var c = new Context())
            {
                return c.Events
                    .Include(x => x.DateTimeOptions)
                        .ThenInclude(x => x.SurveyResponses)
                            .ThenInclude(x => x.User)
                    .Include(x => x.CreatedBy)
                    .Include(x => x.Participants)
                    .FirstOrDefault(e => e.Id == id);
            }
        }
    }
}
