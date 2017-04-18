using System;
using System.Collections.Generic;
using System.Linq;
using CoreNg2.Models;
using Microsoft.AspNetCore.Mvc;


namespace CoreNg2.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        readonly DataSimulatorContext _dataSimulatorContext = new DataSimulatorContext();
        AssetsDBContext _assetsDbContext = new AssetsDBContext();

        [HttpGet("[action]")]
        public List<CurrentValues> GetReport()
        {
             return _dataSimulatorContext.CurrentValues.ToList();
        }

        //[HttpGet("rule/{id}", Name = "filterMeasurements")]
        //public List<EventObject> FilterMeasurements()
        //{
        //        var allAssets = from evt in _dataSimulatorContext.History
        //                        //where evt.Value > 100 TODO: implement rule filter
        //                        select new EventObject()
        //                        {
        //                            Tag = evt.Tag,
        //                            Time = evt.Time,
        //                            Value = evt.Value
        //                        };
        //        return allAssets.ToList();
            
        //}
    }

    public class EventObject
    {
        public string Tag { get; set; }
        public DateTime Time { get; set; }
        public double Value { get; set; }
    }


}
