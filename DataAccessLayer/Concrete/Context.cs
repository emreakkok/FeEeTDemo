using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-T2GF1F3\\SQLEXPRESS;database=FeEeTDemoDb; " +
                "integrated security=true;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - Event (CreatedBy)
            // Bir etkinlik, bir kullanıcı tarafından oluşturulur.
            // Bir kullanıcı birden fazla etkinlik oluşturabilir.
            modelBuilder.Entity<Event>()
                .HasOne(e => e.CreatedBy) // Event tablosundaki CreatedBy ilişkisi
                .WithMany(u => u.CreatedEvents) // User tablosundaki CreatedEvents koleksiyonu
                .HasForeignKey(e => e.CreatedById) // Event tablosundaki CreatedById sütunu
                .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silindiğinde ilişkili etkinlikler de silinir.

            // Event - DateTimeOption
            // Bir etkinlik birden fazla tarih ve saat seçeneği içerebilir.
            modelBuilder.Entity<DateTimeOption>()
                .HasOne(d => d.Event) // DateTimeOption tablosundaki Event ilişkisi
                .WithMany(e => e.DateTimeOptions) // Event tablosundaki DateTimeOptions koleksiyonu
                .HasForeignKey(d => d.EventId) // DateTimeOption tablosundaki EventId sütunu
                .OnDelete(DeleteBehavior.Restrict); // Etkinlik silindiğinde ilişkili tarih seçenekleri de silinir.


            // DateTimeOption - SurveyResponse
            // Bir tarih ve saat seçeneği birden fazla kullanıcıdan oy alabilir.
            modelBuilder.Entity<SurveyResponse>()
                .HasOne(s => s.DateTimeOption) // SurveyResponse tablosundaki DateTimeOption ilişkisi
                .WithMany(d => d.SurveyResponses) // DateTimeOption tablosundaki SurveyResponses koleksiyonu
                .HasForeignKey(s => s.DateTimeOptionId) // SurveyResponse tablosundaki DateTimeOptionId sütunu
                .OnDelete(DeleteBehavior.Restrict); // Tarih seçeneği silindiğinde ilişkili yanıtlar da silinir.

            // User - SurveyResponse
            // Bir kullanıcı birden fazla anket yanıtı verebilir.
            modelBuilder.Entity<SurveyResponse>()
                .HasOne(s => s.User) // SurveyResponse tablosundaki User ilişkisi
                .WithMany(u => u.SurveyResponses) // User tablosundaki SurveyResponses koleksiyonu
                .HasForeignKey(s => s.UserId) // SurveyResponse tablosundaki UserId sütunu
                .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silindiğinde ilişkili yanıtlar da silinir.

            // bir kullanıcının aynı seçeneğe birden fazla tıklamaması için ilişki
            modelBuilder.Entity<SurveyResponse>()
                .HasIndex(sr => new { sr.UserId, sr.DateTimeOptionId }) // UserId ve DateTimeOptionId benzersiz olacak
                .IsUnique();
        }
        
        public DbSet<Event> Events { get; set; }
        public DbSet<DateTimeOption> DateTimeOptions { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
