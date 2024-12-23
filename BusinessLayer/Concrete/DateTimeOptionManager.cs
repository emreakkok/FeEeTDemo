using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class DateTimeOptionManager : IDateTimeOptionService
    {
        IDateTimeOptionDal _dateTimeOptionDal;

        public DateTimeOptionManager(IDateTimeOptionDal dateTimeOptionDal)
        {
            _dateTimeOptionDal = dateTimeOptionDal;
        }

        public List<DateTimeOption> GetList()
        {
            return _dateTimeOptionDal.GetListAll();
        }

        public void TAdd(DateTimeOption t)
        {
            _dateTimeOptionDal.Insert(t);
        }

        public void TDelete(DateTimeOption t)
        {
            _dateTimeOptionDal.Delete(t);
        }

        public DateTimeOption TGetById(int id)
        {
            return _dateTimeOptionDal.GetByID(id);
        }

        public void TUpdate(DateTimeOption t)
        {
            _dateTimeOptionDal.Update(t);
        }
    }
}
