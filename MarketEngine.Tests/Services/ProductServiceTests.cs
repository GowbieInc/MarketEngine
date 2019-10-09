using AutoFixture;
using FluentAssertions;
using MarketEngine.Domain.Service.Interfaces;
using MarketEngine.Domain.Service.Services;
using MarketEngine.Model.Models;
using MarketEngine.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace MarketEngine.Tests.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IProductService service;
        private Mock<IProductRepository> productRepositoryMock;

        private Fixture fixture;

        [OneTimeSetUp]
        public void Init()
        {
            fixture = new Fixture();
        }
        
        [SetUp]
        public void SetUp()
        {
            productRepositoryMock = new Mock<IProductRepository>();
            service = new ProductService(productRepositoryMock.Object);
        }

        [Test]
        public void ShouldCreateAProduct()
        {
            var product = fixture.Create<Product>();
            productRepositoryMock.Setup(x => x.Create(product)).Returns(product);

            var response = service.Create(product);

            response.Should().BeOfType<Product>();
            productRepositoryMock.Verify(x => x.Create(product), Times.Once);
        }
    }
}
