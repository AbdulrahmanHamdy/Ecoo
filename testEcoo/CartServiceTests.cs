using ECOO.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Session;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testEcoo
{

    public class CartServiceTests
    {
        private CartService CreateService(FakeSession session)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Features.Set<ISessionFeature>(new SessionFeature
            {
                Session = session
            });

            var accessor = new Mock<IHttpContextAccessor>();
            accessor.Setup(x => x.HttpContext).Returns(httpContext);

            return new CartService(accessor.Object);
        }

        [Fact]
        public void GetCart_ShouldReturnEmpty_WhenNoSession()
        {
            var service = CreateService(new FakeSession());

            var cart = service.GetCart();

            Assert.Empty(cart.Items);
        }

        [Fact]
        public void AddToCart_ShouldAddItem()
        {
            var session = new FakeSession();
            var service = CreateService(session);

            service.AddToCart(1, "P1", 100, "img");

            var cart = service.GetCart();

            Assert.Single(cart.Items);
        }

        [Fact]
        public void AddToCart_ShouldIncreaseQuantity()
        {
            var session = new FakeSession();
            var service = CreateService(session);

            service.AddToCart(1, "P1", 100, "img");
            service.AddToCart(1, "P1", 100, "img");

            var cart = service.GetCart();

            Assert.Equal(2, cart.Items[0].Quantity);
        }

        [Fact]
        public void UpdateQuantity_ShouldRemove_WhenZero()
        {
            var service = CreateService(new FakeSession());

            service.AddToCart(1, "P1", 100, "img");
            service.UpdateQuantity(1, 0);

            var cart = service.GetCart();

            Assert.Empty(cart.Items);
        }

        [Fact]
        public void RemoveFromCart_ShouldRemoveItem()
        {
            var service = CreateService(new FakeSession());

            service.AddToCart(1, "P1", 100, "img");
            service.RemoveFromCart(1);

            Assert.Empty(service.GetCart().Items);
        }

        [Fact]
        public void ClearCart_ShouldEmptyCart()
        {
            var service = CreateService(new FakeSession());

            service.AddToCart(1, "P1", 100, "img");
            service.ClearCart();

            Assert.Empty(service.GetCart().Items);
        }
    }
}

