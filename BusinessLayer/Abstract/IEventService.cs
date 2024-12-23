using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IEventService : IGenericService<Event>
    {
        List<Event> GetListAll_DTO();

        Event GetByID_DTO(int id);

        List<Event> GetEventsByUserId(int id);
        Event GetEventDetailsById(int id);
    }
}