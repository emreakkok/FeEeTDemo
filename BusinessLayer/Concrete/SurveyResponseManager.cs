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
    public class SurveyResponseManager : ISurveyResponseService
    {
        ISurveyResponseDal _surveyResponseDal;

        public SurveyResponseManager(ISurveyResponseDal surveyResponseDal)
        {
            _surveyResponseDal = surveyResponseDal;
        }

        public List<SurveyResponse> GetList()
        {
            return _surveyResponseDal.GetListAll();
        }

        public void TAdd(SurveyResponse t)
        {
            _surveyResponseDal.Insert(t);
        }

        public void TDelete(SurveyResponse t)
        {
            _surveyResponseDal.Delete(t);
        }

        public SurveyResponse TGetById(int id)
        {
            return _surveyResponseDal.GetByID(id);
        }

        public void TUpdate(SurveyResponse t)
        {
            _surveyResponseDal.Update(t);
        }
    }
}
