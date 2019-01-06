namespace Cedar.API.Test.Unit.Registration
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Xunit;

    public class RegistrationControllerTests
    {
        private class Arrangements
        {
            public RegistrationRequest Request { get; }
            
            public RegistrationController SUT { get; }
            
            public Arrangements(RegistrationRequest request)
            {
                Request = request;

                SUT = new RegistrationController
                {
                    ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()}
                };

            }
        }

        private class ArrangementsBuilder
        {
            private RegistrationRequest request;

            public ArrangementsBuilder()
            {
                request = new RegistrationRequest();
            }

            public ArrangementsBuilder WithNullRequest()
            {
                request = null;
                return this;
            }

            public ArrangementsBuilder WithRequest(string name, string email)
            {
                
                request.Name = name;
                request.Email = email;
                return this;
            }

            public Arrangements Build()
            {
                return new Arrangements(request);
            }
        }

        [Fact]
        public void Register_WithNullRequest_ReturnsBadRequest()
        {
            // Arrange 
            var arrangements = new ArrangementsBuilder()
                .WithNullRequest()
                .Build();
            
            // Act 

            var response = arrangements.SUT.Register(arrangements.Request);
            
            // Assert

            response.Should().NotBeNull();
            response.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult) response).Value.Should().Be("No data provided");
        }

        [Fact]
        public void Register_WithNoName_ReturnsValidationError()
        {
            // Arrange
            var arrangements = new ArrangementsBuilder()
                .WithRequest(name: null, email: "abc@gmail.com")
                .Build();
            
            // Act 

            var context = new ValidationContext(arrangements.Request);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(arrangements.Request, context, results, true);
            
            // Assert
            isValid.Should().BeFalse();
            results.First().ErrorMessage.Should().Be("Name is required.");
        }
        
        [Fact]
        public void Register_WithNoEmail_ReturnsValidationError()
        {
            // Arrange
            var arrangements = new ArrangementsBuilder()
                .WithRequest(name: "Sagar", email: null)
                .Build();
            
            // Act 

            var context = new ValidationContext(arrangements.Request);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(arrangements.Request, context, results, true);
            
            // Assert
            isValid.Should().BeFalse();
            results.First().ErrorMessage.Should().Be("Email is required.");
        }

        [Fact]
        public void Register_WithValidRequest_ReturnOkResult()
        {
            // Arrange 
            var arrangements = new ArrangementsBuilder()
                .WithRequest(name: "Sagar", email: "abc@gmail.com")
                .Build();
            
            // Act
            var response = arrangements.SUT.Register(arrangements.Request);
            
            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
        }
    }
}