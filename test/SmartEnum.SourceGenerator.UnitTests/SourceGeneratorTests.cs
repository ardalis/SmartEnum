using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnum.SourceGenerator.UnitTests
{
    public class SourceGeneratorTests
    {
        [Fact]
        public void Subscriptions_CheckNameAndValue()
        {
            var freeSubscriptions = Subscriptions.Free;
            Assert.Equal(nameof(Subscriptions.Free), freeSubscriptions.Name);
            Assert.Equal(1, freeSubscriptions.Value);
        }
        
        [Fact]
        public void Permissions_CheckValueAndName()
        {
            var permissions = Permissions.UserManagement;
            Assert.Equal(nameof(Permissions.UserManagement), permissions.Name);
            Assert.Equal(2, permissions.Value);
        }
        
        
    }
}
