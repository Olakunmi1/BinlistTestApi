using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Xunit
{
   public class BinslistTest
    {
        //private static List<customersDTO> GetAllCustomers()
        //{
        //    List<customersDTO> customers = new List<customersDTO>
        //    {
        //        new customersDTO
        //        {
        //            Name = "John reck",
        //            Address = "30, marina street",
        //            email = "user@gmail.com",
        //            gender = "male",
        //            created_at = DateTime.Now

        //        },

        //         new customersDTO
        //        {
        //            Name = "Angel Viola",
        //            Address = "30, marina street",
        //            email = "Viola@gmail.com",
        //            gender = "female",
        //            created_at = DateTime.Now

        //        },

        //         new customersDTO
        //         {
        //            Name = "Maritn Luther",
        //             Address = "30, marina street",
        //             email = "Viola@gmail.com",
        //             gender = "female",
        //             created_at = DateTime.Now

        //         }

        //    };

        //    return customers;
        //}

        [Fact]
        public void GetListOfCardHits_Action_method_Should_Return_ListOfCardHits()
        {
            //// Arrange ---intializing the classes needed, and Setup up Mock
            //var mockCustomerservice = new Mock<ICustomer>();
            //ILogger<CustomerController> logger = new Logger<CustomerController>(new NullLoggerFactory()); //mock for Ilogger 
            //mockCustomerservice.Setup(c => c.GetListOf_AllCustomers()).Returns(GetAllCustomers());

            //var controller = new CustomerController(mockCustomerservice.Object, logger);

            ////Act  --- Calling on the method to be tested 

            //var result = controller.GetListOf_Customers();

            ////Assert  --- i.e we need to start testing d outcome
            //var viewresult = result.Should().BeOfType<OkObjectResult>();
            //var model = viewresult.Subject.Value.Should().BeAssignableTo<APIGenericResponseDTO<customersDTO>>();
            //model.Subject.Results.Count().Should().Be(3);
        }
    }
}

