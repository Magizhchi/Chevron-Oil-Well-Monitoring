using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreNg2.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreNg2.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public virtual DataSimulatorContext GetContext()
        {
            return new DataSimulatorContext();
        }

        [HttpGet]
        public List<string> GetTagsList()
        {
            using (var context = GetContext())
            {
                var tagNames = from values in context.CurrentValues
                               select values.Tag;
                return tagNames.Distinct().ToList();
            }
            
        }   
    }
}
