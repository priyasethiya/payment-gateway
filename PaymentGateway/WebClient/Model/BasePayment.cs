using System;

namespace WebClient.Model
{
    public class BasePayment : IPayment
    {
        public BasePayment()
        {

        }
        public string Uid { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiry { get; set; }
        public string CardCvv { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public DateTime PaymentDate { get; }
        public bool Success { get; set; }

        void IPayment.Print()
        {
            throw new NotImplementedException();
        }

        void IPayment.TakeInput()
        {
            Console.Write("Enter Uid :");
            Uid = Console.ReadLine();

            Console.WriteLine("Enter Your 16 Digit Card Number Without Spaces :");
            CardNumber = Console.ReadLine();

            int mm;
            int yyyy;
            Console.WriteLine("Enter Card Expiry Month :");
            mm = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Enter Card Expiry Year in YYYY format :");
            yyyy = Int32.Parse(Console.ReadLine());

            CardExpiry = yyyy + "-" + mm;

            Console.WriteLine("Enter Card Cvv :");
            CardCvv = Console.ReadLine();

            Console.WriteLine("Enter Amount :");
            Amount = Double.Parse(Console.ReadLine());

            Console.WriteLine("Enter Currency in 3 letter format :");
            Currency = Console.ReadLine();
        }
    }
}
