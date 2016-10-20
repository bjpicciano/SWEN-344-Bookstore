using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWEN_344_Bookstore;
using SWEN_344_Bookstore.Controllers;
using System.Threading.Tasks;

namespace SWEN_344_Bookstore.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Login()
        {
            AccountController controller = new AccountController();

            ViewResult result = controller.Login("hi") as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("hi", result.ViewBag.returnUrl);
        }

        [TestMethod]
        public void RegisterPage()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.Register() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ConfirmEmail()
        {
            AccountController controller = new AccountController();
            var res = controller.ConfirmEmail("userId", "hi");
            
        }

        [TestMethod]
        public void ForgotPassword()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.ForgotPassword() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ForgotPasswordConfirmation()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.ForgotPasswordConfirmation() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ResetPassword()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.ResetPassword("") as ViewResult;
            // Assert
            Assert.IsNotNull(result);  
        }

        [TestMethod]
        public void ResetPasswordConfirmation()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.ResetPasswordConfirmation() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LogOff()
        {
            AccountController controller = new AccountController();
            controller.Login("");
            var result = (RedirectToRouteResult)controller.LogOff();

            result.RouteValues["action"].Equals("Index");
            result.RouteValues["controller"].Equals("Home");

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);

        }
        [TestMethod]
        public void ExternalLoginFailure()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.ExternalLoginFailure() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
