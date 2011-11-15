using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUser
{
    /// <summary>
    /// Class Containing the User Details of a particular User.
    /// </summary>
    public class UserVo
    {
        private int m_UserId;
        private string m_LoginId;
        private string m_OriginalPassword;
        private string m_Password;
        private string m_PwdSaltValue;
        private string m_FirstName;
        private string m_MiddleName;
        private string m_LastName;
        private string m_Email;
        private string m_UserType;
        private int m_IsTempPassword;

        public string[] RoleList { get; set; } 

        public int IsTempPassword
        {
            get { return m_IsTempPassword; }
            set { m_IsTempPassword = value; }
        }

        public int UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }

        public string LoginId
        {
            get { return m_LoginId; }
            set { m_LoginId = value; }
        }
        

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        public string OriginalPassword
        {
            get { return m_OriginalPassword; }
            set { m_OriginalPassword = value; }
        }
        public string PasswordSaltValue
        {
            get { return m_PwdSaltValue; }
            set { m_PwdSaltValue = value; }
        }
        
        public string FirstName
        {
            get { return m_FirstName; }
            set { m_FirstName = value; }
        }

        public string MiddleName
        {
            get { return m_MiddleName; }
            set { m_MiddleName = value; }
        }

        public string LastName
        {
            get { return m_LastName; }
            set { m_LastName = value; }
        }


        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }


        public string UserType
        {
            get { return m_UserType; }
            set { m_UserType = value; }
        }

        public string theme { get; set; }
    }
}
