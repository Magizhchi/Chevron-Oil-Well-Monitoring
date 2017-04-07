using System.Collections.Generic;
using System.Linq;
using CoreNg2.Models;
using Microsoft.AspNetCore.Mvc;


namespace CoreNg2.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        DataSimulatorContext _context = new DataSimulatorContext();

        [HttpGet("[action]")]
        public List<CurrentValues> GetReport()
        {
             return _context.CurrentValues.ToList();
        }
    }
}
