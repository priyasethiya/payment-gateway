using System;

namespace PaymentGateway.Model
{
    public interface IPayment
    {
        string Uid { get; set; }
        string CardNumber { get; set; }
        string CardExpiry { get; set; }
        string CardCvv { get; set; }
        double Amount { get; set; }
        string Currency { get; set; }
        DateTime PaymentDate { get; set; }
        bool Success { get; set; }
    }
}
