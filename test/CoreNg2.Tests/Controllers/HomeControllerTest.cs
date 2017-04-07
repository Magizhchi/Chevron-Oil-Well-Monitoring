using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreNg2.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace CoreNg2.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexReturnsAResult()
        {
            var controller = new HomeController();

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ErrorReturnsAResult()
        {
            var controller = new HomeController();

            var result = controller.Error();

            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
