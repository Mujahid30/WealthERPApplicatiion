using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoCommon
{
   public class CommonProgrammingBo
    {
       public bool IsNumeric(string s)
       {
           double Result;
           return double.TryParse(s, out Result);
       }  
    }
}
