using System.Net;

namespace WERP_NAVLIB
{
    //Thsi class will handle all the credialtials
    class WerpCredentials
    {

        internal static NetworkCredential GetDefaultCredentials()
        { 
            //User Name and pwd will be encricted and will be coming from App.COnfig file
            string uName="principal";
            string Pwd="Pr1$0212";
            NetworkCredential nc = new NetworkCredential(uName, Pwd);
            return nc;
        }
    }
}
