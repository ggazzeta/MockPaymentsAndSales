using Microsoft.AspNetCore.Mvc;
using MockPaymentsAndSales.Gateways;
using MockPaymentsAndSales.Gateways.Interfaces;
using MockPaymentsAndSales.ReturnObjects;
using System.ComponentModel.DataAnnotations;

namespace MockPaymentsAndSales.Controllers
{
    [ApiController]
    [Route("sales")]
    public class SalesController : ControllerBase
    {

        private readonly ISalesGateway _salesGateway;

        public SalesController(ISalesGateway salesGateway)
        {
            _salesGateway = salesGateway;
        }

        [Route("get_sales")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ReturnSale>), 200)]
        public async Task<List<ReturnSale>> GetSales([Required(ErrorMessage = "This field is required."), Range(1, 5, ErrorMessage = "Min size is {0}, Max size is {1}")] int salesAmount,
                                                        DateTime startDate, DateTime endDate)
        {
            return await _salesGateway.ReturnAllSales(salesAmount, startDate, endDate);
        }
    }
}
