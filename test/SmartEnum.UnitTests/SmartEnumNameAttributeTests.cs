using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Ardalis.SmartEnum.UnitTests
{
    public class SmartEnumNameAttributeTests
    {
        [Fact]
        public void ThrowsWhenCtorGetsNullType()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action ctorCall = () => new SmartEnumNameAttribute(null);

            ctorCall.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ThrowsWhenCtorGetsNullPropertyName()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action ctorCall = () => new SmartEnumNameAttribute(typeof(TestSmartEnum), propertyName: null, errorMessage: "Some Error Message");

            ctorCall.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ThrowsWhenCtorGetsNullErrorMessage()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action ctorCall = () => new SmartEnumNameAttribute(typeof(TestSmartEnum), errorMessage: null);

            ctorCall.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ThrowsWhenCtorGetsNonSmartEnumType()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action ctorCall = () => new SmartEnumNameAttribute(typeof(SmartEnumNameAttributeTests));

            ctorCall.Should().ThrowExactly<InvalidOperationException>();
        }

        [Fact]
        public void DoesNotThrowWhenCtorForSmartEnumWithDifferentKeyType()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action ctorCall = () => new SmartEnumNameAttribute(typeof(TestSmartEnumString));

            ctorCall.Should().NotThrow();
        }

        [Fact]
        public void ReturnsErrorMessageContainingPropertyNameAndAllPossibleSmartEnumValues()
        {
            var model = new TestValidationModel { SomeProp = "foo" };
            var validationContext = new ValidationContext(model, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            using (new AssertionScope())
            {
                validationResults.Should().HaveCount(1);
                validationResults.Single().ErrorMessage.Should().Contain(nameof(TestValidationModel.SomeProp));
                foreach (var name in TestSmartEnum.List.Select(cqo => cqo.Name))
                    validationResults.Single().ErrorMessage.Should().Contain(name);
            }
        }

        [Fact]
        public void ValidatesGivenNonString()
        {
            var attribute = new SmartEnumAttribute(typeof(TestSmartEnum));
            object nonString = new { };

            var isValid = attribute.IsValid(nonString);

            isValid.Should().BeTrue();
        }

        [Fact]
        public void ValidatesGivenNullString()
        {
            var attribute = new SmartEnumAttribute(typeof(TestSmartEnum));

            var isValid = attribute.IsValid(null);

            isValid.Should().BeTrue();
        }

        [Fact]
        public void ValidatesForEachMemberOfAGivenSmartEnum()
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum));
            using (new AssertionScope())
            {
                foreach (var addressTypeName in TestSmartEnum.List.Select(at => at.Name))
                {
                    var isValid = attribute.IsValid(addressTypeName);
                    isValid.Should().BeTrue();
                }
            }
        }

        [Fact]
        public void ValidatesForCaseInsensitiveStringWhenCaseInsensitiveMatchingEnabled()
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum), allowCaseInsensitiveMatch: true);
            var caseInsensitiveSource = TestSmartEnum.TestFoo.Name.ToLower();

            var isValid = attribute.IsValid(caseInsensitiveSource);

            isValid.Should().BeTrue();
        }

        [Fact]
        public void DoesNotValidateForCaseInsensitiveStringWhenCaseInsensitiveMatchingDisabled()
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum));
            var caseInsensitiveSource = TestSmartEnum.TestFoo.Name.ToLower();

            var isValid = attribute.IsValid(caseInsensitiveSource);

            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("Some Wrong Value")]
        [InlineData("25")]
        public void DoesNotValidateGivenNonSmartEnumNames(string invalidName)
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum));

            var isValid = attribute.IsValid(invalidName);

            isValid.Should().BeFalse();
        }

        private class TestValidationModel
        {
            [SmartEnumName(typeof(TestSmartEnum))]
            public string SomeProp { get; set; }
        }

        private class TestSmartEnum : SmartEnum<TestSmartEnum>
        {
            public static readonly TestSmartEnum TestFoo = new TestSmartEnum(nameof(TestFoo), 2);

            private TestSmartEnum(string name, int value) : base(name, value) { }
        }

        private class TestSmartEnumString : SmartEnum<TestSmartEnumString, string>
        {
            private TestSmartEnumString(string name, string value) : base(name, value) { }
        }
    }
}
