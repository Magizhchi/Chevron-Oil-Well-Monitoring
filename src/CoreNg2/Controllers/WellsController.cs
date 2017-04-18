using System.Collections.Generic;
using System.Linq;
using CoreNg2.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.v3;

namespace CoreNg2.Controllers
{
    [Route("api/[controller]")]
    public class WellsController : Controller
    {
        public virtual AssetsDBContext GetContext()
        {
            return new AssetsDBContext();
        }

        [HttpGet]
        public List<WellObject> Get()
        {
            using (var context = GetContext())
            {
                var allWells = from well in context.Wells
                    select new WellObject
                    {
                        WellId = well.Id,
                        WellName = well.Name,
                        WellFkFieldsId = well.FkFieldsId
                    };

                return allWells.ToList();
            }
        }

        [HttpGet("{id}", Name = "getWellsFromField")]
        public List<WellObject> GetWellsForField(int id)
        {
            using (var context = GetContext())
            {
                var resultWell = from well in context.Wells
                    where well.FkFieldsId == id
                    select new WellObject
                    {
                        WellId = well.Id,
                        WellName = well.Name,
                        WellFkFieldsId = well.FkFieldsId
                    };

                return resultWell.ToList();
            }
        }

        [HttpGet("breadcrumb/{id}", Name = "getBreadCrumbForWell")]
        public string GetBreadCrumb(int id)
        {
            using (var context = GetContext())
            {
                var result = from asset in context.Assets
                             join field in context.Fields on asset.Id equals field.FkAssetId
                             where field.Id == id
                             select new
                             {
                                 AssetName = asset.Name,
                                 AssetId = asset.Id,
                                 FieldName = field.Name,
                                 FieldId = field.Id
                             };

                return result.ToJson();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Wells item)
        {
            using (var context = GetContext())
            {
                if (item == null)
                {
                    return BadRequest();
                }
                context.Wells.Add(item);
                context.SaveChanges();
            }
            return CreatedAtRoute("getWellsFromField", new {id = item.FkFieldsId}, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Wells item)
        {
            using (var context = GetContext())
            {
                var wellItem = (from well in context.Wells
                    where well.Id == id
                    select well).First();
                wellItem.Name = item.Name;
                wellItem.FkFieldsId = item.FkFieldsId;
                context.Wells.Update(wellItem);
                context.SaveChanges();
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = GetContext())
            {
                var well = context.Wells.FirstOrDefault(t => t.Id == id);
                if (well == null)
                {
                    return NotFound();
                }

                context.Wells.Remove(well);
                context.SaveChanges();
            }
            return new NoContentResult();
        }
    }


    public class WellObject
    {
        public int WellId { get; set; }
        public string WellName { get; set; }
        public int WellFkFieldsId { get; set; }
    }
}