using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment.Registration.App.DTOs;
using Payment.Registration.App.Services;

namespace Payment.Registration.API.Controllers
{
    [Route("paymentForm")]
    public class PaymentFormController : Controller
    {
        private readonly PaymentFormAppService paymentFormAppService;

        public PaymentFormController(PaymentFormAppService paymentFormAppService)
        {
            this.paymentFormAppService = paymentFormAppService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<PaymentFormDto>> Get()
        {
            return await paymentFormAppService.Get();
        }

        [HttpPost]
        [Route("")]
        public async Task<Guid> Add([FromBody] PaymentFormSaveDto paymentFormSaveDto)
        {
            return await paymentFormAppService.Add(paymentFormSaveDto);
        }
    }
}