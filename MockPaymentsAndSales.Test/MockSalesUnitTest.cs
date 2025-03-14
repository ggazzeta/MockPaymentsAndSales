using Moq;
using MockPaymentsAndSales.Controllers;
using MockPaymentsAndSales.Gateways.Interfaces;
using MockPaymentsAndSales.ReturnObjects;
using Microsoft.AspNetCore.Mvc;
using MockPaymentsAndSales.ValueObjects;

namespace MockPaymentsAndSales.Test
{
    public class MockSalesUnitTest
    {
        [Fact]
        public async Task GetSales_ShouldReturnMockSalesData()
        {
            var mockSalesGateway = new Mock<ISalesGateway>();

            var mockSalesData = new List<ReturnSale>
            {
                new ReturnSale(
                    new Sale
                    {
                        id = "S001",
                        total_price = 150.00m,
                        shipping_price = 10.00m,
                        customer_id = "C001",
                        status = 1,
                        sale_date = new DateTime(2024, 3, 1)
                    },
                    new Customer
                    {
                        id = "C001",
                        customer_name = "John Doe",
                        social_number = "123-45-6789",
                        address_line_1 = "123 Main St",
                        address_line_2 = "Apt 4B",
                        street_number = "123",
                        city = "New York",
                        state = "NY",
                        country = "USA",
                        postal_code = "10001"
                    },
                    new List<Payments>
                    {
                        new Payments
                        {
                            id = "P001",
                            sales_id = "S001",
                            description = "Credit Card Payment",
                            payment_method = new PaymentMethod
                            {
                                type = "CreditCard",
                                description = "Visa Payment",
                                card_network = "Visa",
                                card_last_numbers = "1234"
                            },
                            price = 150.00m,
                            paid_price = 150.00m,
                            payment_date = new DateTime(2024, 3, 2)
                        }
                    }
                ),
                new ReturnSale(
                    new Sale
                    {
                        id = "S002",
                        total_price = 300.00m,
                        shipping_price = 20.00m,
                        customer_id = "C002",
                        status = 2,
                        sale_date = new DateTime(2024, 3, 5)
                    },
                    new Customer
                    {
                        id = "C002",
                        customer_name = "Jane Doe",
                        social_number = "987-65-4321",
                        address_line_1 = "456 Elm St",
                        address_line_2 = null,
                        street_number = "456",
                        city = "Los Angeles",
                        state = "CA",
                        country = "USA",
                        postal_code = "90001"
                    },
                    new List<Payments>
                    {
                        new Payments
                        {
                            id = "P002",
                            sales_id = "S002",
                            description = "PayPal Payment",
                            payment_method = new PaymentMethod
                            {
                                type = "PayPal",
                                description = "Online Payment"
                            },
                            price = 300.00m,
                            paid_price = 300.00m,
                            payment_date = new DateTime(2024, 3, 6)
                        }
                    }
                )
            };

            mockSalesGateway
                .Setup(sg => sg.ReturnAllSales(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(mockSalesData);

            var controller = new SalesController(mockSalesGateway.Object);

            var result = await controller.GetSales(2, new DateTime(2024, 3, 1), new DateTime(2024, 3, 31));

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}