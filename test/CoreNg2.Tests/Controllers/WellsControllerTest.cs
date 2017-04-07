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
    public class WellsControllerTest
    {
        [Fact]
        public void GetWellsTest()
        {
            
            var testController = new WellsControllerMock();
            var wells = testController.Get();

            var numRows = wells.Count;
            Assert.Equal(numRows, 3);
        }


        [Fact]
        public void GetWellsApiTest()
        {
           
            var testController = new WellsControllerMock();
            var result = testController.Get();
            Assert.IsType<List<WellObject>>(result);
        }

        [Fact]
        public void GetWellsbyFieldIdtest()
        {
           
            var testController = new WellsControllerMock();
            var well = testController.GetWellsForField(1);

            var numWells = well.Count;
            Assert.Equal(numWells, 2);
        }

        [Fact]
        public void GetWellsbyFieldIdTestApi()
        {
            
            var testController = new WellsControllerMock();
            var result = testController.GetWellsForField(1);

            Assert.IsType<List<WellObject>>(result);
        }

        [Fact]
        public void CreateWellsTestApi()
        {
            var testcontroller = new WellsControllerMock();
            Wells well = new Wells()
            {
                Id = 2,
                FkFieldsId = 1
            };

            var result = testcontroller.Create(well);
            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("getWellsFromField", ((CreatedAtRouteResult)result).RouteName);
            Assert.NotNull(((CreatedAtRouteResult)result).RouteValues["id"]);
        }

        [Fact]
        public void CreateWellsNegativeTestApi()
        {
            var testcontroller = new WellsControllerMock();
           

            var result = testcontroller.Create(null);
            Assert.IsType<BadRequestResult>(result);
            
        }

        [Fact]
        public void UpdateWellsTestApi()
        {
            var testcontroller = new WellsControllerMock();

            Wells well = new Wells()
            {
                Id = 2,
                Name = "test",
                FkFieldsId = 1
            };

            var result = testcontroller.Update(2, well);
            Assert.IsType<NoContentResult>(result);

        }


        [Fact]
        public void DeleteWellsTestApi()
        {
            var testcontroller = new WellsControllerMock();
            var result = testcontroller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteWellsNegativeTestApi()
        {
            var testcontroller = new WellsControllerMock();
            var result = testcontroller.Delete(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetContextReturnsContext()
        {
            var testController = new WellsController();
            var result = testController.GetContext();

            Assert.IsType<AssetsDBContext>(result);
        }



    }

    public class WellsControllerMock : WellsController
    {
        public override AssetsDBContext GetContext()
        {

            var data = new List<Wells>
            {
                new Wells(),
                new Wells(),
                new Wells(),
            }.AsQueryable();

            data.ElementAt(0).Id = 1;
            data.ElementAt(0).FkFieldsId = 1;
            data.ElementAt(1).Id = 2;
            data.ElementAt(1).FkFieldsId = 1;
            data.ElementAt(2).Id = 3;
            data.ElementAt(2).FkFieldsId = 2;

            var mockSet = new Mock<DbSet<Wells>>();
            mockSet.As<IQueryable<Wells>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Wells>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Wells>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Wells>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContent = new Mock<AssetsDBContext>();
            mockContent.Setup(c => c.Wells).Returns(mockSet.Object);

            return mockContent.Object;
        }
    }
}
