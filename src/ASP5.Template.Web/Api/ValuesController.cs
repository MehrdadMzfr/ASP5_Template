using ASP5.Template.Core;
using ASP5.Template.Core.Models;
using ASP5.Template.Web.Models;
using Microsoft.AspNet.Mvc;

namespace ASP5.Template.Web.Api
{
    public class ValuesController : Controller
    {
        private readonly IBusinessService _businessLayer;
        private readonly EfBusinessLayer _efBusinessLayer;

        public ValuesController(IBusinessService businessLayer, EfBusinessLayer efBusinessLayer)
        {
            _businessLayer = businessLayer;
            _efBusinessLayer = efBusinessLayer;
        }

        [HttpGet]
        public ActionResult GetManufacturers()
        {
            var manufacturers = _efBusinessLayer.GetData();
            return Ok(manufacturers);
        }
    }
}
