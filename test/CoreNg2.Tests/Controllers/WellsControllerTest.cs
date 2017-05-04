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
        public void GetBreadCrumbTestApi()
        {
            var testcontroller = new WellsControllerMock();
            var result = testcontroller.GetBreadCrumb(1);

            Assert.IsType<string>(result);
        }


        [Fact]
        public void GetRecentEventTestApi()
        {
            var testcontroller = new WellsControllerMock();
            object result = testcontroller.GetRecentEvent(1);

            var type_result = result.GetType().ToString().Contains("Anonymous");
            Assert.True(type_result);
        }

        [Fact]
        public void GetRecentEventNegativeTest()
        {
            var testcontroller = new WellsControllerMock();
            object result = testcontroller.GetRecentEvent(-1);

            var type_result = result.GetType().ToString().Contains("Anonymous");
            Assert.True(type_result);
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


            var assets_data = new List<Assets>
            {
                new Assets(),
               
            }.AsQueryable();

            assets_data.ElementAt(0).Id = 1;
           


            var fields_data = new List<Fields>
            {
                new Fields(),
               
            }.AsQueryable();

            fields_data.ElementAt(0).Id = 1;
            fields_data.ElementAt(0).FkAssetId = 1;
           

            var wells_data = new List<Wells>
            {
                new Wells()
            }.AsQueryable();

            wells_data.ElementAt(0).Id = 1;
            wells_data.ElementAt(0).FkFieldsId = 1;

            var measurment_data = new List<Measurements>()
            {
                new Measurements()
            }.AsQueryable();

            measurment_data.ElementAt(0).Id = 1;
            measurment_data.ElementAt(0).FkWellsId = 1;

            var rules_data = new List<Rules>()
            {
                new Rules()
            }.AsQueryable();

            rules_data.ElementAt(0).Id = 1;
            rules_data.ElementAt(0).FkMeasurementsId = 1;

            var evt_data = new List<WEvents>()
            {
                new WEvents()
            }.AsQueryable();

            evt_data.ElementAt(0).Id = 1;
            evt_data.ElementAt(0).RuleId = 1;
            evt_data.ElementAt(0).EndTime = System.DateTime.Now;



            var assets_mockSet = new Mock<DbSet<Assets>>();
            assets_mockSet.As<IQueryable<Assets>>().Setup(m => m.Provider).Returns(assets_data.Provider);
            assets_mockSet.As<IQueryable<Assets>>().Setup(m => m.Expression).Returns(assets_data.Expression);
            assets_mockSet.As<IQueryable<Assets>>().Setup(m => m.ElementType).Returns(assets_data.ElementType);
            assets_mockSet.As<IQueryable<Assets>>().Setup(m => m.GetEnumerator()).Returns(assets_data.GetEnumerator());

            var fields_mockSet = new Mock<DbSet<Fields>>();
            fields_mockSet.As<IQueryable<Fields>>().Setup(m => m.Provider).Returns(fields_data.Provider);
            fields_mockSet.As<IQueryable<Fields>>().Setup(m => m.Expression).Returns(fields_data.Expression);
            fields_mockSet.As<IQueryable<Fields>>().Setup(m => m.ElementType).Returns(fields_data.ElementType);
            fields_mockSet.As<IQueryable<Fields>>().Setup(m => m.GetEnumerator()).Returns(fields_data.GetEnumerator());

           
            var mockSet = new Mock<DbSet<Wells>>();
            mockSet.As<IQueryable<Wells>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Wells>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Wells>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Wells>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var measurements_mockSet = new Mock<DbSet<Measurements>>();
            measurements_mockSet.As<IQueryable<Measurements>>().Setup(a => a.Provider).Returns(measurment_data.Provider);
            measurements_mockSet.As<IQueryable<Measurements>>().Setup(a => a.Expression).Returns(measurment_data.Expression);
            measurements_mockSet.As<IQueryable<Measurements>>().Setup(a => a.ElementType).Returns(measurment_data.ElementType);
            measurements_mockSet.As<IQueryable<Measurements>>().Setup(a => a.GetEnumerator()).Returns(measurment_data.GetEnumerator());

            var rules_mockSet = new Mock<DbSet<Rules>>();
            rules_mockSet.As<IQueryable<Rules>>().Setup(b => b.Provider).Returns(rules_data.Provider);
            rules_mockSet.As<IQueryable<Rules>>().Setup(b => b.Expression).Returns(rules_data.Expression);
            rules_mockSet.As<IQueryable<Rules>>().Setup(b => b.ElementType).Returns(rules_data.ElementType);
            rules_mockSet.As<IQueryable<Rules>>().Setup(b => b.GetEnumerator()).Returns(rules_data.GetEnumerator());

            var evt_mockSet = new Mock<DbSet<WEvents>>();
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.Provider).Returns(evt_data.Provider);
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.Expression).Returns(evt_data.Expression);
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.ElementType).Returns(evt_data.ElementType);
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.GetEnumerator()).Returns(evt_data.GetEnumerator());


            var mockContent = new Mock<AssetsDBContext>();
            mockContent.Setup(c => c.Wells).Returns(mockSet.Object);
            mockContent.Setup(c => c.Fields).Returns(fields_mockSet.Object);
            mockContent.Setup(c => c.Assets).Returns(assets_mockSet.Object);
            mockContent.Setup(c => c.Measurements).Returns(measurements_mockSet.Object);
            mockContent.Setup(c => c.Rules).Returns(rules_mockSet.Object);
            mockContent.Setup(h => h.WEvents).Returns(evt_mockSet.Object);

            return mockContent.Object;
        }
    }
}
