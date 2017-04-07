using System.Collections.Generic;
using System.Linq;
using CoreNg2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreNg2.Controllers
{
    [Route("api/[controller]")]
    public class FieldsController : Controller
    {
        public virtual AssetsDBContext GetContext()
        {
            return new AssetsDBContext();
        }

        [HttpGet]
        public List<FieldObject> Get()
        {
            using (var context = GetContext())
            {
                var allFields = from field in context.Fields
                    select new FieldObject
                    {
                        FieldId = field.Id,
                        FieldName = field.Name,
                        FieldFkAssetId = field.FkAssetId
                    };

                return allFields.ToList();
            }
        }

        [HttpGet("{id}", Name = "getFieldsFromAsset")]
        public List<FieldObject> GetFieldsForAsset(int id)
        {
            using (var context = GetContext())
            {
                var resultFields = from field in context.Fields
                    where field.FkAssetId == id
                    select new FieldObject
                    {
                        FieldId = field.Id,
                        FieldName = field.Name,
                        FieldFkAssetId = field.FkAssetId
                    };

                return resultFields.ToList();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Fields item)
        {
            using (var context = GetContext())
            {
                if (item == null)
                {
                    return BadRequest();
                }
                context.Fields.Add(item);
                context.SaveChanges();
            }
            return CreatedAtRoute("getFieldsFromAsset", new {id = item.FkAssetId}, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Fields item)
        {
            using (var context = GetContext())
            {
                var fieldItem = (from field in context.Fields
                    where field.Id == id
                    select field).First();
                fieldItem.Name = item.Name;
                fieldItem.FkAssetId = item.FkAssetId;
                context.Fields.Update(fieldItem);
                context.SaveChanges();
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = GetContext())
            {
                var field = context.Fields.FirstOrDefault(t => t.Id == id);
                if (field == null)
                {
                    return NotFound();
                }

                context.Fields.Remove(field);
                context.SaveChanges();
            }
            return new NoContentResult();
        }
    }

    public class FieldObject
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public int FieldFkAssetId { get; set; }
    }
}
