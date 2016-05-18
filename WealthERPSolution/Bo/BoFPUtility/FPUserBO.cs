using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoFPUtility;
using VOFPUtilityUser;
using System.Data;

namespace BoFPUtility
{
    public class FPUserBO
    {
        FPUserDAO fpUserDao = new FPUserDAO();
        public static void CheckSession()
        {
            if (System.Web.HttpContext.Current.Session["UserVo"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("Default.aspx");
            }
        }
        public FPUserVo CreateAndGetFPUtilityUserDetails(FPUserVo userVo, string clientCode, bool userType)
        {
            return fpUserDao.CreateAndGetFPUtilityUserDetails(userVo, clientCode, userType);
        }
        public bool SetAnswerToQuestion(int userId, int questionId, int answerId)
        {
            return fpUserDao.SetAnswerToQuestion(userId, questionId, answerId);
        }
        public DataSet GetQuestionAndOptions(int userId)
        {
            return fpUserDao.GetQuestionAndOptions(userId);
        }
        public DataSet GetRiskClass(int userId, int adviserId)
        {
            return fpUserDao.GetRiskClass(userId, adviserId);
        }
        public DataSet GetRiskProfileQuestion(int advisorId)
        {
            return fpUserDao.GetRiskProfileQuestion(advisorId);
        }
        public DataSet GetQuestionOption(int questionId, int advisorId)
        {
            return fpUserDao.GetQuestionOption(questionId, advisorId);
        }
        public bool CheckInvestorExists(int adviserId, string panNo, string clientCode)
        {
            return fpUserDao.CheckInvestorExists(adviserId, panNo, clientCode);
        }
    }
}
