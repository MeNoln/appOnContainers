using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using DiaryApi.Models;
using DiaryApi.Infrastructure;
using System.Threading.Tasks;
using DiaryApi.Controllers;

namespace xUnitTests.DiaryApiTests
{
    public class xUnitTest
    {
        private IEnumerable<DiaryModel> GetTestDbData()
        {
            return new List<DiaryModel>
            {
                new DiaryModel{ Id = 1, Date = DateTime.Now, DayDescription = "Day One", DayMark = "One" },
                new DiaryModel{ Id = 2, Date = DateTime.Now, DayDescription = "Day Two", DayMark = "Two" },
                new DiaryModel{ Id = 3, Date = DateTime.Now, DayDescription = "Day Three", DayMark = "Three" },
                new DiaryModel{ Id = 4, Date = DateTime.Now, DayDescription = "Day Four", DayMark = "Four" }
            };
        }

        [Fact]
        public void Test()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}
