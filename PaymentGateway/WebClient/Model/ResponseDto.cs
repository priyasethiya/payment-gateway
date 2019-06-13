using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebClient.Model
{
    [DataContract(Name = "ResponseDto")]
    public class ResponseDto
    {
        #region Properties
        [DataMember(Name = "resultCode")]
        public int ResultCode { get; set; }
        [DataMember(Name = "resultText")]
        public string ResultText { get; set; }

        [DataMember(Name = "paymentDetails")]
        public List<Payment> PaymentDetails{ get; set; }
        #endregion
    }
}
