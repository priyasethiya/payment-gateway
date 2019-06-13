using System;

namespace WebClient.Model
{
    public interface IPayment
    {
        string Uid { get; set; }
        string CardNumber { get; set; }
        string CardExpiry { get; set; }
        string CardCvv { get; set; }
        double Amount { get; set; }
        string Currency { get; set; }
        DateTime PaymentDate { get; }
        bool Success { get; set; }
        void Print();
        void TakeInput();
    }    
}
