using System;
using System.Collections.Generic;
using System.Linq;
using CoreNg2.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.v3;

namespace CoreNg2.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementsController : Controller
    {
        public virtual AssetsDBContext GetContext()
        {
            return new AssetsDBContext();
        }

        [HttpGet]
        public List<MeasurementObject> Get()
        {
            using (var context = GetContext())
            {
                var allMeasurements = from measurement in context.Measurements
                                        select new MeasurementObject
                                        {
                                            MeasurementId = measurement.Id,
                                            MeasurementName = measurement.Name,
                                            MeasurementTagName = measurement.TagName,
                                            MeasurementFkWellsId = measurement.FkWellsId,
                                        };

                return allMeasurements.ToList();
            }
        }

        [HttpGet("breadcrumb/{id}", Name = "getBreadCrumbForMeasurement")]
        public string GetBreadCrumb(int id)
        {
            using (var context = GetContext())
            {
                var result = from asset in context.Assets
                             join field in context.Fields on asset.Id equals field.FkAssetId
                             join well in context.Wells on field.Id equals well.FkFieldsId
                             where well.Id == id
                             select new
                             {
                                 AssetName = asset.Name,
                                 AssetId = asset.Id,
                                 FieldName = field.Name,
                                 FieldId = field.Id,
                                 WellName = well.Name,
                                 WellId = well.Id
                             };

                return result.ToJson();
            }
        }

        [HttpGet("RuleTypes")]
        public string GetRuleTypes()
        {
            using (var context = GetContext())
            {
                var result = from ruleType in context.RuleType
                             select ruleType;

                return result.ToJson();
            }
            
        }

        [HttpGet("{id}", Name = "getMeasurementsFromWell")]
        public string GetMeasurementsForWell(int id)
        {
            using (var context = GetContext())
            {
                var resultMeasurements = from measurement in context.Measurements
                                         join rule in context.Rules on measurement.Id equals rule.FkMeasurementsId
                                         join ruleType in context.RuleType on rule.FkRuleTypeId equals ruleType.RuleTypeId
                                         where measurement.FkWellsId == id
                                        select new
                                        {
                                            MeasurementId = measurement.Id,
                                            MeasurementName = measurement.Name,
                                            MeasurementTagName = measurement.TagName,
                                            MeasurementFkWellsId = measurement.FkWellsId,
                                            rule.Value,
                                            ruleType.RuleDescription
                                        };

                return resultMeasurements.ToJson();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] MeasurementImporter item)
        {
            using (var context = GetContext())
            {
                if (item == null)
                {
                    return BadRequest();
                }

                Measurements measurementItem = new Measurements();
                measurementItem.Name = item.MeasurementName;
                measurementItem.TagName = item.MeasurementTagName;
                measurementItem.FkWellsId = item.FkWellId;
                measurementItem.Description = item.MeasurementDescription;


                context.Measurements.Add(measurementItem);
                context.SaveChanges();

                Rules rule = new Rules();
                rule.FkMeasurementsId = measurementItem.Id;
                rule.FkRuleTypeId = item.RuleTypeId;
                rule.Value = item.Value;
                rule.IsActive = true;

                context.Rules.Add(rule);
                context.SaveChanges();

                return CreatedAtRoute("getMeasurementsFromWell", new { id = measurementItem.FkWellsId }, item);

            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Measurements item)
        {
            using (var context = GetContext())
            {
                var measurementItem = (from measurement in context.Measurements
                    where measurement.Id == id
                    select measurement).First();
                measurementItem.Name = item.Name;
                measurementItem.FkWellsId = item.FkWellsId;

                context.Measurements.Update(measurementItem);
                context.SaveChanges();
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = GetContext())
            {
                var measurement = context.Measurements.FirstOrDefault(t => t.Id == id);
                if (measurement == null)
                {
                    return NotFound();
                }

                context.Measurements.Remove(measurement);
                context.SaveChanges();
            }
            return new NoContentResult();
        }
    }

    public class MeasurementObject
    {
        public string MeasurementName { get; set; }
        public string MeasurementTagName { get; set; }
        public int MeasurementId { get; set; }
        public int MeasurementGreaterThan { get; set; }
        public bool MeasurementGreaterThanActive { get; set; }
        public int MeasurementFkWellsId { get; set; }
    }

    public class MeasurementImporter
    {
        public string MeasurementName { get; set; }
        public string MeasurementTagName { get; set; }
        public string MeasurementDescription { get; set; }
        public int RuleTypeId { get; set; }
        public int Value { get; set; }
        public int FkWellId { get; set; }

    }
}