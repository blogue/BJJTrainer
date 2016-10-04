using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BJJTrainer.Models;
namespace BJJTrainer.Tests
{
    public class CategoryTest
    {
        [Fact]
        public void GetNameTest()
        {
            //Arrange
            var category = new Category();

            //Act
            category.Name = "Defense";
            var result = category.Name;

            //Assert
            Assert.Equal("Defense", result);
 
        }
    }
}
