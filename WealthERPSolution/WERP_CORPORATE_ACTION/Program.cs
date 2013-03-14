using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WERP_CORPORATE_ACTION
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessCorporateAction();
        }

        public static void ProcessCorporateAction()
        {
            CorporateActionBo corporateActionBo = new CorporateActionBo();
            corporateActionBo.ProcessCorporateAction();
        }
    }
}
