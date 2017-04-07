using System.Collections.Generic;
using System.Linq;
using CoreNg2.Models;
using Microsoft.AspNetCore.Mvc;

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
                        MeasurementGreaterThan = measurement.GreaterThan,
                        MeasurementGreaterThanActive = measurement.GreaterThanActive,
                        MeasurementFkWellsId = measurement.FkWellsId
                    };

                return allMeasurements.ToList();
            }
        }

        [HttpGet("{id}", Name = "getMeasurementsFromWell")]
        public List<MeasurementObject> GetMeasurementsForWell(int id)
        {
            using (var context = GetContext())
            {
                var resultMeasurements = from measurement in context.Measurements
                    where measurement.FkWellsId == id
                    select new MeasurementObject
                    {
                        MeasurementId = measurement.Id,
                        MeasurementName = measurement.Name,
                        MeasurementTagName = measurement.TagName,
                        MeasurementGreaterThan = measurement.GreaterThan,
                        MeasurementGreaterThanActive = measurement.GreaterThanActive,
                        MeasurementFkWellsId = measurement.FkWellsId
                    };

                return resultMeasurements.ToList();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Measurements item)
        {
            using (var context = GetContext())
            {
                if (item == null)
                {
                    return BadRequest();
                }
                context.Measurements.Add(item);
                context.SaveChanges();
            }
            return CreatedAtRoute("getMeasurementsFromWell", new {id = item.FkWellsId}, item);
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
                measurementItem.GreaterThan = item.GreaterThan;
                measurementItem.GreaterThanActive = item.GreaterThanActive;

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
}