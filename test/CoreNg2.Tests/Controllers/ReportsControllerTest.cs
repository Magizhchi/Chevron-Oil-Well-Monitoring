using System.Collections.Generic;
using CoreNg2.Controllers;
using CoreNg2.Models;
using Xunit;
using System.Linq;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace CoreNg2.Tests.Controllers
{
    public class ReportsControllerTest
    {
        [Fact]
        public void GetReportTestApi()
        {
            var controller = new ReportsController();

            Assert.IsType<List<CurrentValues>>(controller.GetReport());
        }

        [Fact]
        public void GetEventsTestApi()
        {
            var controller = new ReportsController();

            Assert.IsType<List<dynamic>>(controller.GetEvents());
        }

        [Fact]
        public void GetDetailedEventsTestApi()
        {
            var controller = new ReportsController();

            Assert.IsType<System.Collections.Generic.List<dynamic>>(controller.GetDetailedEvents(0));
                        
        }

        //should look into this. this is more akin to a negative test
        [Fact]
        public void GetDetailsEventsTestApi()
        {
            var controller = new ReportsController();
            var results = controller.GetDetailsEvents(1);


            Assert.Null(results);
        }


        [Fact]
        public void DrillDownEventTestApi()
        {
            var controller = new ReportsControllerMock();
            var results = controller.DrillDownEvent(1);

            Assert.IsType<System.Collections.Generic.List<dynamic>>(results);

        }

        [Fact]
        public void DrillDownEventNegativeTestApi()
        {
            var controller = new ReportsController();
            var results = controller.DrillDownEvent(-1);

            Assert.IsType<List<dynamic>>(results);
            Assert.Equal(0, results.Count);
        }

    }

    public class ReportsControllerMock : ReportsController
    {

        public override AssetsDBContext GetAssetContext()
        {

            var evt_data = new List<WEvents>()
            {
                new WEvents()
            }.AsQueryable();

            evt_data.ElementAt(0).Id = 1;
            evt_data.ElementAt(0).RuleId = 1;
            evt_data.ElementAt(0).StartTime = System.DateTime.Now;

            var evt_mockSet = new Mock<DbSet<WEvents>>();
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.Provider).Returns(evt_data.Provider);
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.Expression).Returns(evt_data.Expression);
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.ElementType).Returns(evt_data.ElementType);
            evt_mockSet.As<IQueryable<WEvents>>().Setup(c => c.GetEnumerator()).Returns(evt_data.GetEnumerator());


            var mockContent = new Mock<AssetsDBContext>();
            mockContent.Setup(h => h.WEvents).Returns(evt_mockSet.Object);

            return mockContent.Object;
        }

    }
}
