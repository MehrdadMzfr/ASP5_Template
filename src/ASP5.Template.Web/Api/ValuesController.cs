using ASP5.Template.Core;
using ASP5.Template.Core.Models;
using ASP5.Template.Web.Models;
using Microsoft.AspNet.Mvc;

namespace ASP5.Template.Web.Api
{
    public class ValuesController : Controller
    {
        private readonly IBusinessService _businessLayer;
        private readonly EfBusinessLayer _testLayer = new EfBusinessLayer();
        [FromServices]
        public ContextConfiguration Config { get; set; }

        public ValuesController(IBusinessService businessLayer)
        {
            _businessLayer = businessLayer;
        }

        [HttpGet]
        public ActionResult GetManufacturers()
        {
            var manufacturers = _testLayer.GetData();
            return Ok(manufacturers);
        }
    }
}
