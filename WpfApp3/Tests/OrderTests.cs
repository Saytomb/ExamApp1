
namespace WpfApp3.Tests
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using WpfApp3.Entities;
    using NUnit.Framework;
    using NUnit.Framework.Legacy;

    [TestFixture]
    public class OrderTests
    {

        [Test]
        public void DeleteOrder_ShouldRemoveOrderFromDatabase()
        {
            // Arrange
            int orderId;

            using (var context = new wpfapp1Entities2())
            {
                var order = new Order
                {
                    ProductId = 1,
                    PartnerId = 1,
                    Quantity = 10,
                    OrderDate = DateTime.Now
                };
                context.Orders.Add(order);
                context.SaveChanges();
                orderId = order.OrderId;
            }

            using (var context = new wpfapp1Entities2())
            {
                var order = context.Orders.Find(orderId);

                // Act
                context.Orders.Remove(order);
                context.SaveChanges();
            }

            // Assert
            using (var context = new wpfapp1Entities2())
            {
                var deletedOrder = context.Orders.Find(orderId);
                ClassicAssert.IsNull(deletedOrder);
            }
        }
    }

}
