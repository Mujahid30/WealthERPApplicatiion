namespace WERP_NAVLIB
{
    using System;
    using System.Net;

    internal class WerpCredentials
    {
        internal static NetworkCredential GetDefaultCredentials()
        {
            string uName = "principal";
            return new NetworkCredential(uName, "Pr1$0212");
        }
    }
}

