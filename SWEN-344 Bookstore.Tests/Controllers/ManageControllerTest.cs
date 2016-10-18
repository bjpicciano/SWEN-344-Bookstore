using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWEN_344_Bookstore;
using SWEN_344_Bookstore.Controllers;

namespace SWEN_344_Bookstore.Tests.Controllers
{
    [TestClass]
    public class ManageControllerTest
    {
        [TestMethod]
        public void ChangePassword()
        {
            // Arrange
            ManageController controller = new ManageController();
            // Act
            ViewResult result = controller.ChangePassword() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SetPassword()
        {
            // Arrange
            ManageController controller = new ManageController();
            // Act
            ViewResult result = controller.SetPassword() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }


    }
}
