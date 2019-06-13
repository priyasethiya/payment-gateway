using System;
using WebClient.Model;

namespace WebClient
{
    class Program
    {
        static APIService apiService;
        static void Main(string[] args)
        {
            apiService = new APIService();
           
            int n = -1;
            while(n != 4)
            {
                Console.WriteLine("\n\nMenu\n1.Make a Payment\n2.Fetch payment using UId\n3.Fetch All Payments\n4.Exit\n");
                Console.WriteLine("Enter your choice ");
                string choice = Console.ReadLine();                
                Int32.TryParse(choice, out n);
                switch(n)
                {
                    case 1:
                        IPayment pymt = new BasePayment();
                        pymt.TakeInput();
                        MakePayment(pymt);
                        break;
                    case 2:
                        Console.WriteLine("Enter uid to search");
                        string uid = Console.ReadLine();
                        GetPaymentWithUid(uid);
                        break;
                    case 3:
                        GetAllPayments();
                        break;

                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid Input . Please try agaian.");
                        break;
                }
            }            
        }

        static void GetAllPayments()
        {
            ResponseDto responseDto = apiService.GetPaymentAsync().Result;

            if (responseDto.ResultCode == 1)
            {
                if (responseDto.PaymentDetails?.Count > 0)
                {
                    foreach (IPayment payment in responseDto.PaymentDetails)
                        payment.Print();
                }
                else
                    Console.WriteLine("No payments retrieved");
            }
        }

        static void GetPaymentWithUid(string uid)
        {
            ResponseDto responseDto = apiService.GetPaymentAsync(uid).Result;
            if (responseDto.ResultCode == 1)
            {
                if(responseDto.PaymentDetails?.Count > 0)
                    responseDto.PaymentDetails[0].Print();
                 else
                    Console.WriteLine("No payments retrieved with uid " + uid);
            }
        }

        static void MakePayment(IPayment pymt)
        {          
            ResponseDto responseDto = apiService.MakePaymentAsync(pymt).Result;
            if (responseDto.ResultCode == 1)
                Console.WriteLine("Payment successful uid : " + pymt.Uid);
            else
                Console.WriteLine("Payment Failed uid : " + pymt.Uid);                
        }
    }
}
