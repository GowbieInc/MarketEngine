using AutoFixture;
using FluentAssertions;
using MarketEngine.Domain.Service.Interfaces;
using MarketEngine.Domain.Service.Services;
using MarketEngine.Model.DTO.Requests;
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
            var request = fixture.Create<CreateProductRequest>();
            productRepositoryMock.Setup(x => x.Create(It.IsAny<Product>())).Returns(fixture.Create<Product>());

            var response = service.Create(request, fixture.Create<string>());

            response.Should().BeOfType<Product>();
            response.Should().NotBeNull();
            response.Id.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void ShouldThrowAExceptionWhenTryingToCreateANullProduct()
        {
            service
                .Invoking(method => method.Create(null, fixture.Create<string>()))
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Cannot create a null product");

            productRepositoryMock.Verify(x => x.Create(null), Times.Never);
        }

        [Test]
        public void ShouldThrowAExceptionWhenTryingToCreateAProductWithUnidentifiedUser()
        {
            service
                .Invoking(method => method.Create(fixture.Create<CreateProductRequest>(), string.Empty))
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Cannot create a product (UNIDENTIFIED USER)");

            productRepositoryMock.Verify(x => x.Create(null), Times.Never);
        }

        //[Test]
        //public void ShouldGetAProduct()
        //{
        //    var productId = Guid.NewGuid().ToString();
        //    productRepositoryMock.Setup(x => x.GetById(productId)).Returns(fixture.Create<Product>());

        //    var response = service.GetById(productId);

        //    response.Should().BeOfType<Product>();
        //    response.Should().NotBeNull();
        //}
    }
}
