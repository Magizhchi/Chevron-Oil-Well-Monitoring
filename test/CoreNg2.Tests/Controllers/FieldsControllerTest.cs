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
    public class FieldsControllerTest
    {
        [Fact]
        void GetFieldsTestApi()
        {
            var testController = new FieldsControllerMock();
            var result = testController.Get();
            Assert.IsType<List<FieldObject>>(result);
        }


        [Fact]
        public void GetFieldsTest()
        {
            var testController = new FieldsControllerMock();
            var fields = testController.Get();

            int numRows = fields.Count;
            Assert.Equal(numRows, 3);
        }


        [Fact]
        public void GetFieldByAssetIdTestApi()
        {
            var testController = new FieldsControllerMock();
            var result = testController.GetFieldsForAsset(2);
            Assert.IsType<List<FieldObject>>(result);
        }


        [Fact]
        public void GetFieldsByAssetIdTest()
        {
            var testController = new FieldsControllerMock();
            var fields = testController.GetFieldsForAsset(2);

            int numRows = fields.Count;
            Assert.Equal(numRows, 2);
        }

        [Fact]
        public void CreateFieldsTestApi()
        {
            var testcontroller = new FieldsControllerMock();
            Fields field = new Fields
            {
                Id = 3,
                FkAssetId = 2
            };
            var result = testcontroller.Create(field);


            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("getFieldsFromAsset", ((CreatedAtRouteResult) result).RouteName);
            Assert.NotNull(((CreatedAtRouteResult) result).RouteValues["id"]);
        }

        [Fact]
        public void GetBreadCrumbTestApi()
        {
            var testcontroller = new FieldsControllerMock();
            var result = testcontroller.GetBreadCrumb(1);

            Assert.IsType<string>(result);
        }

        [Fact]
        public void GetRecentEventTestApi()
        {
            var testcontroller = new FieldsControllerMock();
            object result = testcontroller.GetRecentEvent(1);

            var type_result = result.GetType().ToString().Contains("Anonymous");
            Assert.True(type_result);
        }

        [Fact]
        public void GetRecentEventNegativeTest()
        {
            var testcontroller = new FieldsControllerMock();
            object result = testcontroller.GetRecentEvent(-1);

            var type_result = result.GetType().ToString().Contains("Anonymous");
            Assert.True(type_result);
        }

        [Fact]
        public void CreateFieldsNegativeTestApi()
        {
            var testcontroller = new FieldsControllerMock();

            var result = testcontroller.Create(null);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void UpdateFieldsTestApi()
        {
            var testcontroller = new FieldsControllerMock();
            Fields field = new Fields
            {
                Id = 2,
                Name = "Test"
            };
            var result = testcontroller.Update(2, field);


            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public void DeleteFieldsTestApi()
        {
            var testcontroller = new FieldsControllerMock();
            var result = testcontroller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteFieldsNegativeTestApi()
        {
            var testcontroller = new FieldsControllerMock();
            var result = testcontroller.Delete(4);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetContextReturnsContext()
        {
            var controller = new FieldsController();
            var context = controller.GetContext();

            Assert.IsType<AssetsDBContext>(context);
        }

    }

    [Route("api/[controller]")]
    public class FieldsControllerMock : FieldsController
    {
        public override AssetsDBContext GetContext()
        {

            var assets_data = new List<Assets>
            {
                new Assets(),
                new Assets(),
                new Assets(),
            }.AsQueryable();

            assets_data.ElementAt(0).Id = 1;
            assets_data.ElementAt(1).Id = 2;
            assets_data.ElementAt(2).Id = 3;

            
            var data = new List<Fields>
            {
                new Fields(),
                new Fields(),
                new Fields(),
            }.AsQueryable();

            data.ElementAt(0).Id = 1;
            data.ElementAt(0).FkAssetId = 2;
            data.ElementAt(1).Id = 2;
            data.ElementAt(1).FkAssetId = 2;
            data.ElementAt(2).Id = 3;
            data.ElementAt(2).FkAssetId = 1;

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

            var mockSet = new Mock<DbSet<Fields>>();
            mockSet.As<IQueryable<Fields>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Fields>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Fields>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Fields>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var wells_mockSet = new Mock<DbSet<Wells>>();
            wells_mockSet.As<IQueryable<Wells>>().Setup(d => d.Provider).Returns(wells_data.Provider);
            wells_mockSet.As<IQueryable<Wells>>().Setup(d => d.Expression).Returns(wells_data.Expression);
            wells_mockSet.As<IQueryable<Wells>>().Setup(d => d.ElementType).Returns(wells_data.ElementType);
            wells_mockSet.As<IQueryable<Wells>>().Setup(d => d.GetEnumerator()).Returns(wells_data.GetEnumerator());

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
            mockContent.Setup(c => c.Assets).Returns(assets_mockSet.Object);
            mockContent.Setup(c => c.Fields).Returns(mockSet.Object);
            mockContent.Setup(c => c.Wells).Returns(wells_mockSet.Object);
            mockContent.Setup(c => c.Measurements).Returns(measurements_mockSet.Object);
            mockContent.Setup(c => c.Rules).Returns(rules_mockSet.Object);
            mockContent.Setup(h => h.WEvents).Returns(evt_mockSet.Object);

            return mockContent.Object;
        }
    }
}
