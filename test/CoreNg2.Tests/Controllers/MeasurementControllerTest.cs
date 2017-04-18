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
        public void GetMeasurementsByWellIdTestApi()
        {
            var testController = new MeasurementsControllerMock();
            var results = testController.GetMeasurementsForWell(1);

            Assert.IsType<List<MeasurementObject>>(results);
        }

        [Fact]
        public void GetMeasurementsByWellIdTest()
        {
            var testController = new MeasurementsControllerMock();
            var measurements = testController.GetMeasurementsForWell(1);

            int numRows = measurements.Count;
            Assert.Equal(numRows, 1);
        }

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
            data.ElementAt(0).FkWellsId = 2;
            data.ElementAt(1).Id = 2;
            data.ElementAt(1).FkWellsId = 2;
            data.ElementAt(2).Id = 3;
            data.ElementAt(2).FkWellsId = 1;

            var mockSet = new Mock<DbSet<Measurements>>();
            mockSet.As<IQueryable<Measurements>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Measurements>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Measurements>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Measurements>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContent = new Mock<AssetsDBContext>();
            mockContent.Setup(c => c.Measurements).Returns(mockSet.Object);

            return mockContent.Object;
        }
    }
}
