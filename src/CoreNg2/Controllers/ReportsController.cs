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
        readonly AssetsDBContext _assetsDbContext = new AssetsDBContext();

        [HttpGet("[action]")]
        public List<CurrentValues> GetReport()
        {
             return _dataSimulatorContext.CurrentValues.ToList();
        }

        [HttpGet("events/")]
        public List<dynamic> GetEvents()
        {
            var events = from evt in _assetsDbContext.WEvents
                            select new
                            {
                                evt.Id,
                                evt.RuleId,
                                evt.Tag,
                                evt.StartTime,
                                evt.EndTime,
                                evt.MaxValue
                            };

            return events.ToList<dynamic>();
        }

//        //'id' is measurement ID
//        [HttpGet("events/{id}")]
//        public List<dynamic> GetEventsForMeasurement(int id)
//        {
//            var events = from evt in _assetsDbContext.WEvents
//                         join rule in _assetsDbContext.Rules on evt.RuleId equals rule.Id
//                         join measurement in _assetsDbContext.Measurements on rule.FkMeasurementsId equals measurement.Id
//                         where measurement.Id == id
//                         select new
//                         {
//                             evt.Id,
//                             evt.RuleId,
//                             evt.Tag,
//                             evt.StartTime,
//                             evt.EndTime,
//                             evt.MaxValue
//                         };
//
//            return events.ToList<dynamic>();
//        }

        [HttpGet("events/{id}")]
        public dynamic GetDetailsEvents(int id)
        {
            var wEvent = from evt in _assetsDbContext.WEvents
                         join rule in _assetsDbContext.Rules on evt.RuleId equals rule.Id
                         join measurement in _assetsDbContext.Measurements on rule.FkMeasurementsId equals measurement.Id
                         join well in _assetsDbContext.Wells on measurement.FkWellsId equals well.Id
                         join field in _assetsDbContext.Fields on well.FkFieldsId equals field.Id
                         join asset in _assetsDbContext.Assets on field.FkAssetId equals asset.Id
                         where evt.Id == id
                         select new
                         {
                             evt.Id,
                             evt.StartTime,
                             evt.EndTime,
                             evt.MaxValue,
                             evt.Tag,
                             RuleValue = rule.Value,
                             MeasurementName = measurement.Name,
                             MeasurementDesc = measurement.Description,
                             WellName = well.Name,
                             FieldName = field.Name,
                             AssetName = asset.Name
                         };

            return wEvent.FirstOrDefault();
        }
        public List<dynamic> GetDetailedEvents(int id)
        {
            var events = from evt in _assetsDbContext.WEvents
                         join rule in _assetsDbContext.Rules on evt.RuleId equals rule.Id
                         join measurement in _assetsDbContext.Measurements on rule.FkMeasurementsId equals measurement.Id
                         where measurement.Id == id
                         select new
                         {
                             evt.Id,
                             evt.RuleId,
                             evt.Tag,
                             evt.StartTime,
                             evt.EndTime,
                             evt.MaxValue
                         };

            return events.ToList<dynamic>();
        }
        //event ID
        [HttpGet("events/drill/{id}", Name = "DrillDown")]
        public List<dynamic> DrillDownEvent(int id)
        {
            var eventResult = from evt in _assetsDbContext.WEvents
                where evt.Id == id
                select new
                {
                    tag = evt.Tag,
                    time = evt.StartTime
                };

            var selectedEvent = eventResult.FirstOrDefault();

            if (selectedEvent == null)
            {
                return Enumerable.Empty<dynamic>().ToList();
            }

            var historyValues = from hist in _dataSimulatorContext.History
                where hist.Tag == selectedEvent.tag
                && hist.Time >= selectedEvent.time.AddHours(-1) && hist.Time <= selectedEvent.time.AddHours(1)
                select new
                {
                    hist.Tag,
                    hist.Time,
                    hist.Value
                };

            return historyValues.ToList<dynamic>();
        }

    }

}
