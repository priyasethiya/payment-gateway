using System;

namespace PaymentGateway.Model
{
    public class Payment : IPayment
    {
        public string Uid { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiry { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CardCvv { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public bool Success { get; set; }
    }
}
