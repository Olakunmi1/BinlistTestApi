using BinlistTestApi.Binlist.Data;
using BinlistTestApi.Controllers;
using BinlistTestApi.Helpers;
using BinlistTestApi.ReadDTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace TestProject
{
   public class CardTest
    {
        const int CardNum = 5789123;
        const int count1 = 2;
        const int CardNum2 = 54689022;
        const int count2 = 4;
        private static List<HitCountsDTO_GetAll> GetAllCardHits()
        {
            List<HitCountsDTO_GetAll> cardHits = new List<HitCountsDTO_GetAll>
            {
                new HitCountsDTO_GetAll
                {
                    Size = 7,
                    Response = CardNum.ToString() + ":" + " " + count1.ToString()
                },

                 new HitCountsDTO_GetAll
                {
                    Size = 8,
                    Response = CardNum2.ToString() + ":" + " " + count2.ToString()
                },
            };

            return cardHits;
        }

        private static MyRootClass GetCarddetails()
        {
            MyRootClass cardDetails = new MyRootClass
            {
               
            };

            return cardDetails;
        }

        [Fact]
        public void GetListOfCardHits_Action_method_Should_Return_ListOfCardHits()
        {
            // arrange ---intializing the classes needed, and setup up mock
            var mockcardservice = new Mock<ICardService>();
            ILogger<CardController> logger = new Logger<CardController>(new NullLoggerFactory()); //mock for ilogger 
            mockcardservice.Setup(c => c.getAllCardHits()).Returns(GetAllCardHits());

            var controller = new CardController(logger, mockcardservice.Object);

            //act  --- calling on the method to be tested 

            var result = controller.GetAllCardHits();

            //assert  --- i.e we need to start testing d outcome
            var viewresult = result.Should().BeOfType<OkObjectResult>();
            var model = viewresult.Subject.Value.Should().BeAssignableTo<ApiGenericResponseDTO<HitCountsDTO_GetAll>>();
            model.Subject.Response.Count().Should().Be(2);
        }

        [Fact]
        public void GetCardDetails_3rdPartyApi()
        {
            // arrange ---intializing the classes needed, and setup up mock
            var mockcardservice = new Mock<ICardService>();
            ILogger<CardController> logger = new Logger<CardController>(new NullLoggerFactory()); //mock for ilogger 
            mockcardservice.Setup(c => c.GetcardDetails(CardNum)).Returns(GetAllCardHits());

            var controller = new CardController(logger, mockcardservice.Object);

            //act  --- calling on the method to be tested 

            var result = controller.GetCardDetails();

            //assert  --- i.e we need to start testing d outcome
            var viewresult = result.Should().BeOfType<OkObjectResult>();
            var model = viewresult.Subject.Value.Should().BeAssignableTo<ApiGenericResponseDTO<HitCountsDTO_GetAll>>();
            model.Subject.Response.Count().Should().Be(2);
        }
    }
}
