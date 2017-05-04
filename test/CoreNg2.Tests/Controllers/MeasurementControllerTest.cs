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
    public class MeasurementControllerTest
    {
        [Fact]
        public void GetMeasurementsTest()
        {
            var testController = new MeasurementsControllerMock();
            var measurements = testController.Get();

            int numRows = measurements.Count;
            Assert.Equal(numRows, 3);
        }

        [Fact]
        public void GetMeasurementsTestApi()
        {
            var testController = new MeasurementsControllerMock();
            var results = testController.Get();
            Assert.IsType<List<MeasurementObject>>(results);
        }

        [Fact]
        public void GetRuleTypesTestAPI()
        {
            var testcontroller = new MeasurementsControllerMock();
            var results = testcontroller.GetRuleTypes();

            Assert.IsType<string>(results);
        }

        [Fact]
        public void GetMeasurementsByWellIdTestApi()
        {
            var testController = new MeasurementsControllerMock();
            var results = testController.GetMeasurementsForWell(1);

            Assert.IsType<string>(results);
        }

        //[Fact]
        //public void GetMeasurementsByWellIdTest()
        //{
        //    var testController = new MeasurementsControllerMock();
        //    var measurements = testController.GetMeasurementsForWell(1);

        //    int numRows = measurements.Count;
        //    Assert.Equal(numRows, 1);
        //}

        [Fact]
        public void CreateMeasurementTestApi()
        {
            var testController = new MeasurementsControllerMock();
            var test = new MeasurementImporter()
            {
                RuleTypeId = 2,
                FkWellId = 2
            };

            var result = testController.Create(test);

            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("getMeasurementsFromWell", ((CreatedAtRouteResult) result).RouteName);
            Assert.NotNull(((CreatedAtRouteResult) result).RouteValues["id"]);
        }


        [Fact]
        public void CreateMeasurementNegativeTestApi()
        {
            var testController = new MeasurementsControllerMock();


            var result = testController.Create(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetBreadCrumbTestApi()
        {
            var testcontroller = new MeasurementsControllerMock();
            var result = testcontroller.GetBreadCrumb(1);

            Assert.IsType<string>(result);
        }

        [Fact]
        public void GetRecentEventTestApi()
        {
            var testcontroller = new MeasurementsControllerMock();
            var result = testcontroller.GetRecentEvent(1);

            var type_result = result.GetType().ToString().Contains("Anonymous");
            Assert.True(type_result);
        }

        [Fact]
        public void GetRecentEventNegativeTest()
        {
            var testcontroller = new MeasurementsControllerMock();
            var result = testcontroller.GetRecentEvent(-1);

            var type_result = result.GetType().ToString().Contains("Anonymous");
            Assert.True(type_result);
        }

        [Fact]
        public void UpdateMeasurementTestApi()
        {
            var testcontroller = new MeasurementsControllerMock();
            Measurements test = new Measurements()
            {
                Id = 2,
                FkWellsId = 2,
                Name = "test"
            };

            var result = testcontroller.Update(1, test);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMeasurementTestApi()
        {
            var testcontroller = new MeasurementsControllerMock();
            var result = testcontroller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMeasurementNegativeTestApi()
        {
            var testcontroller = new MeasurementsControllerMock();
            var result = testcontroller.Delete(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetContextTest()
        {
            var testcontroller = new MeasurementsController();
            Assert.IsType<AssetsDBContext>(testcontroller.GetContext());
        }

        [Fact]
        public void MeasurementImporterTest()
        {
            var test = new MeasurementImporter();
            test.FkWellId = 1;
            test.MeasurementDescription = "mock";
            test.MeasurementName = "name";
            test.MeasurementTagName="tag";
            test.RuleTypeId = 1;

            Assert.IsType<MeasurementImporter>(test);
            Assert.Equal(1, test.FkWellId);
            Assert.Equal("mock", test.MeasurementDescription);
            Assert.Equal("name", test.MeasurementName);
            Assert.Equal("tag", test.MeasurementTagName);
            Assert.Equal(1, test.RuleTypeId);

            
        }
    }

    public class MeasurementsControllerMock : MeasurementsController
    {
        public override AssetsDBContext GetContext()
        {
            var data = new List<Measurements>
            {
                new Measurements(),
                new Measurements(),
                new Measurements(),
            }.AsQueryable();

            data.ElementAt(0).Id = 1;
            data.ElementAt(0).FkWellsId = 1;
            data.ElementAt(1).Id = 2;
            data.ElementAt(1).FkWellsId = 2;
            data.ElementAt(2).Id = 3;
            data.ElementAt(2).FkWellsId = 1;

            var assets_data = new List<Assets>
            {
                new Assets(),
                new Assets(),
                new Assets(),
            }.AsQueryable();

            assets_data.ElementAt(0).Id = 1;
            assets_data.ElementAt(1).Id = 2;
            assets_data.ElementAt(2).Id = 3;


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

          

            var rules_data = new List<Rules>()
            {
                new Rules()
            }.AsQueryable();

            rules_data.ElementAt(0).Id = 1;
            rules_data.ElementAt(0).FkMeasurementsId = 1;
            rules_data.ElementAt(0).FkRuleTypeId = 1;

            var evt_data = new List<WEvents>()
            {
                new WEvents()
            }.AsQueryable();

            evt_data.ElementAt(0).Id = 1;
            evt_data.ElementAt(0).RuleId = 1;
            evt_data.ElementAt(0).EndTime = System.DateTime.Now;

            var rulestype_data = new List<RuleType>()
            {
                new RuleType()
            }.AsQueryable();

            rulestype_data.ElementAt(0).RuleTypeId = 1;


            var mockSet = new Mock<DbSet<Measurements>>();
            mockSet.As<IQueryable<Measurements>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Measurements>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Measurements>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Measurements>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

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

            var wells_mockSet = new Mock<DbSet<Wells>>();
            wells_mockSet.As<IQueryable<Wells>>().Setup(d => d.Provider).Returns(wells_data.Provider);
            wells_mockSet.As<IQueryable<Wells>>().Setup(d => d.Expression).Returns(wells_data.Expression);
            wells_mockSet.As<IQueryable<Wells>>().Setup(d => d.ElementType).Returns(wells_data.ElementType);
            wells_mockSet.As<IQueryable<Wells>>().Setup(d => d.GetEnumerator()).Returns(wells_data.GetEnumerator());


            var evt_mockSet = new Mock<DbSet<WEvents>>();
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.Provider).Returns(evt_data.Provider);
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.Expression).Returns(evt_data.Expression);
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.ElementType).Returns(evt_data.ElementType);
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.GetEnumerator()).Returns(evt_data.GetEnumerator());

            var rules_mockSet = new Mock<DbSet<Rules>>();
            rules_mockSet.As<IQueryable<Rules>>().Setup(b => b.Provider).Returns(rules_data.Provider);
            rules_mockSet.As<IQueryable<Rules>>().Setup(b => b.Expression).Returns(rules_data.Expression);
            rules_mockSet.As<IQueryable<Rules>>().Setup(b => b.ElementType).Returns(rules_data.ElementType);
            rules_mockSet.As<IQueryable<Rules>>().Setup(b => b.GetEnumerator()).Returns(rules_data.GetEnumerator());

            var rulestype_mockSet = new Mock<DbSet<RuleType>>();
            rulestype_mockSet.As<IQueryable<RuleType>>().Setup(b => b.Provider).Returns(rulestype_data.Provider);
            rulestype_mockSet.As<IQueryable<RuleType>>().Setup(b => b.Expression).Returns(rulestype_data.Expression);
            rulestype_mockSet.As<IQueryable<RuleType>>().Setup(b => b.ElementType).Returns(rulestype_data.ElementType);
            rulestype_mockSet.As<IQueryable<RuleType>>().Setup(b => b.GetEnumerator()).Returns(rulestype_data.GetEnumerator());

            var mockContent = new Mock<AssetsDBContext>();
            mockContent.Setup(c => c.Measurements).Returns(mockSet.Object);
            mockContent.Setup(c => c.Assets).Returns(assets_mockSet.Object);
            mockContent.Setup(c => c.Fields).Returns(fields_mockSet.Object);
            mockContent.Setup(c => c.Wells).Returns(wells_mockSet.Object);
            mockContent.Setup(c => c.Rules).Returns(rules_mockSet.Object);
            mockContent.Setup(h => h.WEvents).Returns(evt_mockSet.Object);
            mockContent.Setup(c => c.RuleType).Returns(rulestype_mockSet.Object);

            return mockContent.Object;
        }
    }
}
