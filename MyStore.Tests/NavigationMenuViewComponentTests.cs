using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MyStore.Components;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //Arrange
            Mock<ICategoryRepository> mock = new();

            mock.Setup(m => m.Categories).Returns((new Category[] {
                new Category { Name = "One"},
                new Category { Name = "Two"},
                new Category { Name = "Three"}
            }).AsQueryable());

            NavigationMenuViewComponent navigationMenu = new(mock.Object)
            {
                ViewComponentContext = new()
                {
                    ViewContext = { RouteData = new Microsoft.AspNetCore.Routing.RouteData() }
                }
            };

            //Action
            string[] results = ((navigationMenu.Invoke() as ViewViewComponentResult)?.ViewData?.Model as IEnumerable<string> ?? Enumerable.Empty<string>()).ToArray();


            //Assert
            Assert.Equal(new string[] { "One", "Two", "Three"}, results);
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            //Arrange
            string selectedCategory = "Two";
            Mock<ICategoryRepository> mock = new();

            mock.Setup(m => m.Categories).Returns((new Category[] {
                new Category { Name = "One"},
                new Category { Name = "Two"},
                new Category { Name = "Three"}

            }).AsQueryable());

            NavigationMenuViewComponent navigationMenu = new(mock.Object)
            {
                ViewComponentContext = new()
                {
                    ViewContext = { RouteData = new Microsoft.AspNetCore.Routing.RouteData() }
                }
            };

            navigationMenu.RouteData.Values["category"] = selectedCategory;

            //Action
            string? result = (navigationMenu.Invoke() as ViewViewComponentResult)?.ViewData?["selectedCategory"] as string;

            //Assert
            Assert.Equal(selectedCategory, result);
        }
    }
}
