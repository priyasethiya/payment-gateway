using System;
using System.Runtime.Serialization;

namespace WebClient.Model
{
    [DataContract(Name = "paymentDetails")]
    public class Payment : IPayment
    {
        #region Properties
        [DataMember(Name = "uid")]
        public string Uid { get; set; }

        [DataMember(Name = "cardNumber")]
        public string CardNumber { get; set; }

        [DataMember(Name = "cardExpiry")]
        public string CardExpiry { get; set; }

        [DataMember(Name = "cardCvv")]
        public string CardCvv { get; set; }

        [DataMember(Name = "amount")]
        public double Amount { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "paymentDate")]
        private string JsonPaymentDate { get; set; }

        [IgnoreDataMember]
        public DateTime PaymentDate
        {
            get
            {
                DateTime dt;
                if (DateTime.TryParse(JsonPaymentDate, out dt))
                    return dt;
                return DateTime.Now;
            }
            set { }
        }

        [DataMember(Name = "success")]
        public bool Success { get; set; }
        #endregion

        #region Methods
        public void Print()
        {
            Console.WriteLine("Uid : " + Uid);
            Console.WriteLine("CardNumber : " + CardNumber);
            Console.WriteLine("CardExpiry : " + CardExpiry);
            Console.WriteLine("CardCvv : " + CardCvv);
            Console.WriteLine("Amount : " + Amount);
            Console.WriteLine("Currency : " + Currency);
            Console.WriteLine("PaymentDate : " + PaymentDate);
            Console.WriteLine("Success : " + Success);
            Console.WriteLine();
        }

        public void TakeInput()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

