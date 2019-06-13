using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PaymentGateway.Model
{
    public class ResponseData
    {
        #region Constructor
        public ResponseData()
        {
            PaymentDetails = new List<IPayment>();
        }
        #endregion
        #region Properties
        [DataMember(Name = "ResultCode", IsRequired = true, EmitDefaultValue = false)]
        public int ResultCode { get; set; }
        [DataMember(Name = "ResultText", IsRequired = false, EmitDefaultValue = false)]
        public string ResultText { get; set; }
        [DataMember(Name = "PaymentDetails", IsRequired = true, EmitDefaultValue = false)]
        public List<IPayment> PaymentDetails { get; set; }
        #endregion
    }
}
