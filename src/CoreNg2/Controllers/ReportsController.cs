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

        public virtual AssetsDBContext GetAssetContext()
        {
            return new AssetsDBContext();
        }

        [HttpGet("[action]")]
        public List<CurrentValues> GetReport()
        {
             return _dataSimulatorContext.CurrentValues.ToList();
        }

        [HttpGet("events/")]
        public List<dynamic> GetEvents()
        {
            using(var context = GetAssetContext())
            {
            var events = from evt in context.WEvents
                         join rule in context.Rules on evt.RuleId equals rule.Id
                         join measurement in context.Measurements on rule.FkMeasurementsId equals measurement.Id
                         join well in context.Wells on measurement.FkWellsId equals well.Id
                         join field in context.Fields on well.FkFieldsId equals field.Id
                         join asset in context.Assets on field.FkAssetId equals asset.Id
                         select new
                         {
                            evt.Id,
                            evt.RuleId,
                            evt.Tag,
                            evt.StartTime,
                            evt.EndTime,
                            evt.MaxValue,
                            MeasurementName = measurement.Name,
                            AssetName = asset.Name
                         };

            return events.ToList<dynamic>();
            }
        }

        [HttpGet("events/{id}")]
        public dynamic GetDetailsEvents(int id)
        {
            using (var context = GetAssetContext())
            {
                var wEvent = from evt in context.WEvents
                         join rule in context.Rules on evt.RuleId equals rule.Id
                         join ruletype in context.RuleType on rule.FkRuleTypeId equals ruletype.RuleTypeId
                         join measurement in context.Measurements on rule.FkMeasurementsId equals measurement.Id
                         join well in context.Wells on measurement.FkWellsId equals well.Id
                         join field in context.Fields on well.FkFieldsId equals field.Id
                         join asset in context.Assets on field.FkAssetId equals asset.Id
                         where evt.Id == id
                         select new
                         {
                             evt.Id,
                             evt.StartTime,
                             evt.EndTime,
                             evt.MaxValue,
                             evt.Tag,
                             RuleValue = rule.Value,
                             ruletype.RuleDescription,
                             MeasurementName = measurement.Name,
                             MeasurementDesc = measurement.Description,
                             WellName = well.Name,
                             FieldName = field.Name,
                             AssetName = asset.Name
                         };

                return wEvent.FirstOrDefault();
            }
        }
        public List<dynamic> GetDetailedEvents(int id)
        {
            using(var context = GetAssetContext())
            { 
                var events = from evt in context.WEvents
                             join rule in context.Rules on evt.RuleId equals rule.Id
                             join ruletype in context.RuleType on rule.FkRuleTypeId equals ruletype.RuleTypeId
                             join measurement in context.Measurements on rule.FkMeasurementsId equals measurement.Id
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
        }

        [HttpGet("events/drill/{id}", Name = "DrillDown")]
        public List<dynamic> DrillDownEvent(int id)
        {
            using(var context = GetAssetContext())
            {
                var eventResult = from evt in context.WEvents
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
                    && hist.Time >= selectedEvent.time.AddHours(-.5) && hist.Time <= selectedEvent.time.AddHours(.5)
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

}
