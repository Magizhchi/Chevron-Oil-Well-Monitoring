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
    public class ValuesControllerTest
    {
        [Fact]
        public void getContextTest()
        {
            var controller = new ValuesController();
            var context = controller.GetContext();

            Assert.IsType<DataSimulatorContext>(context);
        }

        [Fact]
        public void getTagsListTestApi()
        {
            var controller = new ValuesControllerMock();
            var results = controller.GetTagsList();

            Assert.IsType<List<string>>(results);
        }

        [Fact]
        public void getTagsListTest()
        {
            var controller = new ValuesControllerMock();
            var results = controller.GetTagsList();

            int count = results.Count;

            Assert.Equal(2, count);
        }
    }


    public class ValuesControllerMock : ValuesController
    {
        public override DataSimulatorContext GetContext()
        {
            var data = new List<CurrentValues>
            {
                new CurrentValues(),
                new CurrentValues()
              
            }.AsQueryable();

            data.ElementAt(0).Tag = "mock";
            data.ElementAt(1).Tag = "mock2";

            var mockSet = new Mock<DbSet<CurrentValues>>();
            mockSet.As<IQueryable<CurrentValues>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CurrentValues>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CurrentValues>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CurrentValues>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContent = new Mock<DataSimulatorContext>();
            mockContent.Setup(c => c.CurrentValues).Returns(mockSet.Object);

            return mockContent.Object;

        }
    }
}
