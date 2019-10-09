using AutoFixture;
using FluentAssertions;
using MarketEngine.Domain.Service.Interfaces;
using MarketEngine.Domain.Service.Services;
using MarketEngine.Model.Models;
using MarketEngine.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System;

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

        [TearDown]
        public void TearDown()
        {
            productRepositoryMock.VerifyAll();
        }

        [Test]
        public void ShouldCreateAProduct()
        {
            var product = fixture.Create<Product>();
            productRepositoryMock.Setup(x => x.Create(product)).Returns(product);

            var response = service.Create(product);

            response.Should().BeOfType<Product>();
        }

        [Test]
        public void ShouldThrowAExceptionWhenTryingToCreateANullProduct()
        {
            service
                .Invoking(method => method.Create(null))
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Cannot create a null product");

            productRepositoryMock.Verify(x => x.Create(null), Times.Never);
        }

        [Test]
        public void ShouldGetAProduct()
        {
            var product = fixture.Create<Product>();
            productRepositoryMock.Setup(x => x.GetById(product.Id));

            var response = service.GetById(product.Id);

            response.Should().BeOfType<Product>();
            response.Should().NotBeNull();
        }
    }
}
