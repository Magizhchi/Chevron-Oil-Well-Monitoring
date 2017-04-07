using System.Collections.Generic;
using System.Linq;
using CoreNg2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreNg2.Controllers
{
    [Route("api/[controller]")]
    public class AssetsController : Controller
    {
        public virtual AssetsDBContext GetContext()
        {
            return new AssetsDBContext();
        }

        [HttpGet]
        public List<AssetObject> Get()
        {
            using (var context = GetContext())
            {
                var allAssets = from asset in context.Assets
                    select new AssetObject
                    {
                        AssetId = asset.Id,
                        AssetName = asset.Name
                    };

                return allAssets.ToList();
            }
        }

        [HttpGet("{id}", Name = "getAsset")]
        public AssetObject Get(int id)
        {
            using (var context = GetContext())
            {
                var resultAsset = from asset in context.Assets
                    where asset.Id == id
                    select new AssetObject
                    {
                        AssetId = asset.Id,
                        AssetName = asset.Name
                    };

                return resultAsset.SingleOrDefault();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Assets item)
        {
            using (var context = GetContext())
            {
                if (item == null)
                {
                    return BadRequest();
                }
                context.Assets.Add(item);
                context.SaveChanges();
            }
            return CreatedAtRoute("getAsset", new {id = item.Id}, item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = GetContext())
            {
                var asset = context.Assets.FirstOrDefault(t => t.Id == id);
                if (asset == null)
                {
                    return NotFound();
                }

                context.Assets.Remove(asset);
                context.SaveChanges();
            }
            return new NoContentResult();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Assets item)
        {
            using (var context = GetContext())
            {
                var assetItem = (from asset in context.Assets
                    where asset.Id == id
                    select asset).First();
                assetItem.Name = item.Name;
                context.Assets.Update(assetItem);
                context.SaveChanges();
            }
            return new NoContentResult();
        }
    }

    public class AssetObject
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
    }
}