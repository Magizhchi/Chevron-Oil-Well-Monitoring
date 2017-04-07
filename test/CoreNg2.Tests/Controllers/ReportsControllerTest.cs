using System.Collections.Generic;
using CoreNg2.Controllers;
using CoreNg2.Models;
using Xunit;

namespace CoreNg2.Tests.Controllers
{
    public class ReportsControllerTest
    {
        [Fact]
        public void GetReportTest()
        {
            var controller = new ReportsController();

            Assert.IsType<List<CurrentValues>>(controller.GetReport());
        }
    }
}
