﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;

namespace GameStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация - создание имитированного хранилища
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
        new Game { GameID = 1, Name = "Игра1", Category="Симулятор"},
        new Game { GameID = 2, Name = "Игра2", Category="Симулятор"},
        new Game { GameID = 3, Name = "Игра3", Category="Шутер"},
        new Game { GameID = 4, Name = "Игра4", Category="RPG"},
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "RPG");
            Assert.AreEqual(results[1], "Симулятор");
            Assert.AreEqual(results[2], "Шутер");
        }
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new Game[] {
        new Game { GameID = 1, Name = "Игра1", Category="Симулятор"},
        new Game { GameID = 2, Name = "Игра2", Category="Шутер"}
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Шутер";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }
        [TestMethod]
        public void Generate_Category_Specific_Game_Count()
        {
            /// Организация (arrange)
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
    {
        new Game { GameID = 1, Name = "Игра1", Category="Cat1"},
        new Game { GameID = 2, Name = "Игра2", Category="Cat2"},
        new Game { GameID = 3, Name = "Игра3", Category="Cat1"},
        new Game { GameID = 4, Name = "Игра4", Category="Cat2"},
        new Game { GameID = 5, Name = "Игра5", Category="Cat3"}
    });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((GameListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((GameListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((GameListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((GameListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}