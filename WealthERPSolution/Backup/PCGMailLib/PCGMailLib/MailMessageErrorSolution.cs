using System;
using System.IO;
namespace PCGMailLib
{
    /// <summary>
    /// Further more error handling and action can be defined if we need any thing in alert engine or something but its fine for most of the things
    /// </summary>
    public enum MailMessageErrorSolution
    {
        Delete,
        Ignore
    }
}