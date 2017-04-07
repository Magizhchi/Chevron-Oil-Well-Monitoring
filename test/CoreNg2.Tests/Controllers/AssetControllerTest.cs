using System.Collections.Generic;
using System.Linq;
using Xunit;
using CoreNg2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreNg2.Models;
using Moq;

namespace CoreNg2.Tests.Controllers
{
    public class AssetControllerTest
    {

        [Fact]
        public void Canary()
        {
            Assert.True(true);
        }


        [Fact]
        public void GetAssetsTest()
        {

            var testController = new AssetControllerMock();
            var assets = testController.Get();

            int numRows = assets.Count;
            Assert.Equal(numRows, 3);
        }

        [Fact]
        public void GetAssetApiTest()
        {

            var testController = new AssetControllerMock();
            var results = testController.Get();

            Assert.IsType<List<AssetObject>>(results);
        }


        [Fact] 
        public void ReturnAssetTest()
        {
           
            var testController = new AssetControllerMock();
            var asset = testController.Get(1);

            Assert.Equal(asset.AssetId, 1);
        }

        [Fact]
        public void ReturnAssetAPI_Test()
        {
           

            var testController = new AssetControllerMock();
            var asset = testController.Get(1);

            Assert.IsType<AssetObject>(asset);
        }

        [Fact]
        public void ReturnAssetNegativeTest()
        {
           
            var testController = new AssetControllerMock();
            var asset = testController.Get(4);

            Assert.Null(asset);
        }


        [Fact]
        public void CreateAssetTestApi()
        {

         
            var testController = new AssetControllerMock();
            var item = new Assets {Id = 1};

            var result = testController.Create(item);
            Assert.IsType<CreatedAtRouteResult>(result);

         
        }

        [Fact]
        public void CreateAssetNegativeTestApi()
        {
            

            var testController = new AssetControllerMock();
         

            var result = testController.Create(null);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteAssetTestApi()
        {
            
            var testController = new AssetControllerMock();
            
            var result=testController.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteAssetNegativeTestApi()
        {
           

            var testController = new AssetControllerMock();

            var result = testController.Delete(4);
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void UpdateAssetTestApi()
        {
          
            var testController = new AssetControllerMock();
            var assets = new Assets {Name = "Test"};

            var result = testController.Update(1, assets);
            Assert.IsType<NoContentResult >(result);
        }

        [Fact]
        public void GetContextReturnsContext()
        {
            var controller = new AssetsController();
            var context = controller.GetContext();

            Assert.IsType<AssetsDBContext>(context);
        }
    }



    public class AssetControllerMock:AssetsController
    {

        public override AssetsDBContext GetContext()
        {
            var data = new List<Assets>
            {
                new Assets(),
                new Assets(),
                new Assets(),
            }.AsQueryable();

            data.ElementAt(0).Id = 1;
            data.ElementAt(1).Id = 2;
            data.ElementAt(2).Id = 3;

            var mockSet = new Mock<DbSet<Assets>>();
            mockSet.As<IQueryable<Assets>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Assets>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Assets>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Assets>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContent = new Mock<AssetsDBContext>();
            mockContent.Setup(c => c.Assets).Returns(mockSet.Object);

            return mockContent.Object;
        }

    }

}
