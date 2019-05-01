using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeWork.Controllers;
using HomeWork.Models;
using HomeWork;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
namespace HomeWork.Tests.Controllers
{
    [TestClass]
    public class VLTeaTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new TraSuaEntities();
            var controller = new VanLangTeaController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as List<BubleTea>;
            Assert.IsInstanceOfType(result.Model, typeof(List<BubleTea>));
            Assert.AreEqual(db.BubleTeas.Count(), (model.Count));
        }
        [TestMethod]
        public void TestCreate()
        {
            var controller = new VanLangTeaController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestEdit()
        {
            var controller = new VanLangTeaController();
            {
                var db = new TraSuaEntities();
                var ketqua = controller.Edit(0);
                Assert.IsInstanceOfType(ketqua, typeof(HttpNotFoundResult));
                var item = db.BubleTeas.First();
                var kq = controller.Edit(item.id) as ViewResult;
                Assert.IsNotNull(kq);
                var model = kq.Model as BubleTea;
                Assert.AreEqual(item.id, model.id);
            }
        }
         [TestMethod]
        public void TestDetails()
        {
            var controller = new VanLangTeaController();
            var db = new TraSuaEntities();
            var item = db.BubleTeas.First();

            var result = controller.Details(item.id);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            var model = view.Model as BubleTea;
            Assert.IsNotNull(model);
            Assert.AreEqual(item.id, model.id);

            var result0 = controller.Details(0);
            Assert.IsInstanceOfType(result0, typeof(HttpNotFoundResult));
        }
         [TestMethod]
         public void TestDelete()
         {
             var controller = new VanLangTeaController();
             var db = new TraSuaEntities();
             var item = db.BubleTeas.First();

             var result = controller.Delete(item.id);
             var view = result as ViewResult;
             Assert.IsNotNull(view);
             var model = view.Model as BubleTea;
             Assert.IsNotNull(model);
             Assert.AreEqual(item.id, model.id);

             var result0 = controller.Delete(0);
             Assert.IsInstanceOfType(result0, typeof(HttpNotFoundResult));
         }
         [TestMethod]
         public void TestCreated()
         {
             var db = new TraSuaEntities();
             var model = new BubleTea
             {
                 Name = "Tra sua VL",
                 Price = 25000,
                 Topping = "Tran chau trang"
             };
             var controller = new VanLangTeaController();
             var result = controller.Create(model);
             var redirect = result as RedirectToRouteResult;
             Assert.IsNotNull(redirect);
             Assert.AreEqual("Index", redirect.RouteValues["action"]);
             var item = db.BubleTeas.Find(model.id);
             Assert.IsNotNull(item);
             Assert.AreEqual(model.Name, item.Name);
             Assert.AreEqual(model.Price, item.Price);
             Assert.AreEqual(model.Topping, item.Topping);
         }
    }
}
