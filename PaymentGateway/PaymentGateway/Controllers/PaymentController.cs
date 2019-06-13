using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Services;
using PaymentGateway.Model;
using PaymentGateway.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace PaymentGateway.Controllers
{
    // This is the controller which handles calls to all payments processing for the api
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {        
        #region Properties
        private IPaymentService _paymentService;
        private ILogger<DebugLoggerProvider> loggerDebug;

        #endregion

        #region Constructors
        public PaymentController(IPaymentService paymentService, ILogger<DebugLoggerProvider> loggerDebug)
        {
            _paymentService = paymentService;
            this.loggerDebug = loggerDebug;
            ValidationHelper.loggerDebug = this.loggerDebug;
        }
        #endregion

        #region Methods
        // GET api/payment
        // This returns details of all payments made through the api
        [HttpGet]
       public IActionResult Get()
        {            
            loggerDebug.LogInformation("Get All payments method called");
            Dictionary<string, IPayment> payData = _paymentService.GetPayments();
            ResponseData response = new ResponseData();
            int count = payData.Count;
            if (count > 0)
            {
                List<IPayment> listPayments = new List<IPayment>(payData.Values);
                response.PaymentDetails = listPayments;
            }                
            response.ResultCode = 1;
            response.ResultText = $"{count} payments retrieved";

            loggerDebug.Log(LogLevel.Information, "Retrieved {0} payments", count);

            return Ok(response);
            
           // return Ok("Test");
        }

        // GET api/payment/uid
        // This returns details of a specific payment identified by its uid
        [HttpGet("{uid}")]
        public ActionResult<string> Get(string uid)
        {
            loggerDebug.Log(LogLevel.Information, "Get a specific payment method called with uid {0}",uid);
            IPayment payment = _paymentService.GetPayment(uid);
            ResponseData response = new ResponseData();
            if (payment != null)
            {
                response.PaymentDetails.Add(payment);
                response.ResultCode = 1;
                response.ResultText = "Found  payment details";
                loggerDebug.Log(LogLevel.Information, "Retrieved payment successfully");
            }
            else
            { 
                response.ResultCode = 0;
                response.ResultText = "Payment details not found";
                loggerDebug.Log(LogLevel.Information, "Payment with uid {0} not found", uid);
            }
            return Ok(response);
        }

        // POST api/payment
        //This method processes a payment
        [HttpPost]
        public ActionResult<string> Post([FromBody] Payment pymt)
        {
            IPayment payment = ValidationHelper.Parse(pymt);
            string msg;
            bool valid = ValidationHelper.IsValidRequest(payment, out msg);
            ResponseData response = new ResponseData();
            if (!valid)
            {
                response.ResultCode = 0;
                response.ResultText = "Failed Data Validation. Message = " + msg;
                loggerDebug.Log(LogLevel.Error, "Failed Data Validation. Message = " + msg);
                return Ok(response);
            }
            bool exists = _paymentService.ExistsPayment(payment.Uid);
            if(exists)
            {
                response.ResultCode = 1;
                response.ResultText = "Payment already processed";
                loggerDebug.Log(LogLevel.Error, "Payment already processed");
                return Ok(response);
            }
            bool result = _paymentService.AddPayment(payment);
            if (result)
            {
                response.ResultCode = 1;
                response.ResultText = "Processed Payment successfully";
                loggerDebug.Log(LogLevel.Information, "Payment with uid {0} processed successfully", pymt.Uid);
            }
            else
            {
                response.ResultCode = 0;
                response.ResultText = "Payment Failed";
                loggerDebug.Log(LogLevel.Information, "Payment with uid {0} failed", pymt.Uid);
            }
            return Ok(response);
        }
        #endregion
    }
}
