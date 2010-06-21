using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoCustomerPortfolio;
namespace MFTransactionCancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            customerTransactionBo.RunMFTRansactionsCancellationJob();
        }
    }
}
