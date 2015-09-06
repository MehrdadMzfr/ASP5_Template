﻿using ASP5_Template_Web.Models;
using Microsoft.AspNet.Mvc;

namespace ASP5_Template_Web.Api
{
    public class ValuesController : Controller
    {
        private readonly IBusinessService _businessLayer;

        public ValuesController(IBusinessService businessLayer)
        {
            _businessLayer = businessLayer;
        }

        [HttpGet]
        public ActionResult GetData()
        {
            var list = new[] { "value1", "value2" };
            return Ok(list);
        }
    }
}
