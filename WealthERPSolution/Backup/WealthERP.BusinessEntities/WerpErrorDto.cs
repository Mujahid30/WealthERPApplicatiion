using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WealthERP.BusinessEntities
{
    public class WerpErrorDto
    {
        public const int E_SUCCESS = 0;
        public const int E_GENERIC = -1;
        public const int E_INVALID_INPUT = -2;
        public const int E_DATABASE = -3;

        public static string GetAppMessage(int E_Code)
        {
            switch (E_Code)
            {
                case E_SUCCESS:
                    return "SUCCESS";
                case E_INVALID_INPUT:
                    return "Invalid input";
                case E_DATABASE:
                    return "Database error";
                default:
                    return "Generic error";
            }
        }
    }
}
