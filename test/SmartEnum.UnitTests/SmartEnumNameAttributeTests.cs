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
            Action ctorCall = () => new SmartEnumNameAttribute(null);

            ctorCall.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ThrowsWhenCtorGetsNullPropertyName()
        {
            Action ctorCall = () => new SmartEnumNameAttribute(typeof(TestSmartEnum), propertyName: null, errorMessage: "Some Error Message");

            ctorCall.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ThrowsWhenCtorGetsNullErrorMessage()
        {
            Action ctorCall = () => new SmartEnumNameAttribute(typeof(TestSmartEnum), errorMessage: null);

            ctorCall.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ThrowsWhenCtorGetsNonSmartEnumType()
        {
            Action ctorCall = () => new SmartEnumNameAttribute(typeof(SmartEnumNameAttributeTests));

            ctorCall.Should().ThrowExactly<InvalidOperationException>();
        }

        [Fact]
        public void DoesNotThrowWhenCtorForSmartEnumWithDifferentKeyType()
        {
            Action ctorCall = () => new SmartEnumNameAttribute(typeof(TestSmartEnumWithStringKeyType));

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
                string errorMessage = validationResults.Single().ErrorMessage;
                errorMessage.Should().Contain(nameof(TestValidationModel.SomeProp));
                errorMessage.Should().Contain(TestSmartEnum.TestFoo.Name);
                errorMessage.Should().Contain(TestSmartEnum.TestBar.Name);
                errorMessage.Should().Contain(TestSmartEnum.TestFizz.Name);
                errorMessage.Should().Contain(TestSmartEnum.TestBuzz.Name);
            }
        }

        [Fact]
        public void IsValidGivenNonString()
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum));
            object nonString = new { };

            bool isValid = attribute.IsValid(nonString);

            isValid.Should().BeTrue();
        }

        [Fact]
        public void IsValidGivenNullString()
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum));
            string nullString = null;

            bool isValid = attribute.IsValid(nullString);

            isValid.Should().BeTrue();
        }

        [Fact]
        public void IsValidGivenNullNonString()
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum));
            object nullObject = null;

            bool isValid = attribute.IsValid(nullObject);

            isValid.Should().BeTrue();
        }

        [Fact]
        public void IsValidForEachMemberOfAGivenSmartEnum()
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum));
            using (new AssertionScope())
            {
                foreach (var name in TestSmartEnum.List.Select(at => at.Name))
                {
                    bool isValid = attribute.IsValid(name);
                    isValid.Should().BeTrue();
                }
            }
        }

        [Fact]
        public void IsValidForCaseInsensitiveStringWhenCaseInsensitiveMatchingEnabled()
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum), allowCaseInsensitiveMatch: true);
            var caseInsensitiveSource = TestSmartEnum.TestFoo.Name.ToLower();

            bool isValid = attribute.IsValid(caseInsensitiveSource);

            isValid.Should().BeTrue();
        }

        [Fact]
        public void IsNotValidForCaseInsensitiveStringWhenCaseInsensitiveMatchingDisabled()
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum));
            var caseInsensitiveSource = TestSmartEnum.TestFoo.Name.ToLower();

            bool isValid = attribute.IsValid(caseInsensitiveSource);

            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("Some Wrong Value")]
        [InlineData("25")]
        public void IsNotValidGivenNonSmartEnumNames(string invalidName)
        {
            var attribute = new SmartEnumNameAttribute(typeof(TestSmartEnum));

            bool isValid = attribute.IsValid(invalidName);

            isValid.Should().BeFalse();
        }

        private class TestValidationModel
        {
            [SmartEnumName(typeof(TestSmartEnum))]
            public string SomeProp { get; set; }
        }

        private class TestSmartEnum : SmartEnum<TestSmartEnum>
        {
            public static readonly TestSmartEnum TestFoo = new TestSmartEnum(nameof(TestFoo), 1);
            public static readonly TestSmartEnum TestBar = new TestSmartEnum(nameof(TestBar), 2);
            public static readonly TestSmartEnum TestFizz = new TestSmartEnum(nameof(TestFizz), 3);
            public static readonly TestSmartEnum TestBuzz = new TestSmartEnum(nameof(TestBuzz), 4);

            private TestSmartEnum(string name, int value) : base(name, value) { }
        }

        private class TestSmartEnumWithStringKeyType : SmartEnum<TestSmartEnumWithStringKeyType, string>
        {
            private TestSmartEnumWithStringKeyType(string name, string value) : base(name, value) { }
        }
    }
}
