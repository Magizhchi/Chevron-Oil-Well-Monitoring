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

            var mockSet = new Mock<DbSet<Fields>>();
            mockSet.As<IQueryable<Fields>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Fields>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Fields>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Fields>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContent = new Mock<AssetsDBContext>();
            mockContent.Setup(c => c.Fields).Returns(mockSet.Object);

            return mockContent.Object;
        }
    }
}
