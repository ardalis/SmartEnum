using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.UnitTests;
using FluentAssertions;
using Xunit;

namespace Ardalis.SmartFlagEnum.UnitTests
{
    public class SmartFlagEnumFromName
    {
        [Fact]
        public void IgnoreCaseReturnsIEnumerableWithSameValues()
        {
            var result = SmartFlagTestEnum.FromName("ONE, TWO", true).ToList();

            Assert.Equal("One", result[0].Name);
            Assert.Equal("Two", result[1].Name);
        }

        [Fact]
        public void ThrowsExceptionGivenOutOfRangeValue()
        {
            Assert.Throws<SmartEnumNotFoundException>(() => SmartFlagTestEnum.FromName("Invalid, Value"));
        }

        [Fact] 
        public void CaseSensitiveReturnsIEnumerableWithSameValues()
        {
            var result = SmartFlagTestEnum.FromName("One, Two", false).ToList();

            Assert.Equal("One", result[0].Name);
            Assert.Equal("Two", result[1].Name);
        }

        [Fact]
        public void ReturnsIEnumerableWithSameValues()
        {
            var result = SmartFlagTestEnum.FromName("Five, Four, Three, One, Two, Zero, Other, Values, Ignored", false).ToList();

            Assert.Equal("Zero", result[0].Name);
            Assert.Equal("One", result[1].Name);
            Assert.Equal("Two", result[2].Name);
            Assert.Equal("Three", result[3].Name);
            Assert.Equal("Four", result[4].Name);
            Assert.Equal("Five", result[5].Name);
        }

        [Fact]
        public void ReturnsIEnumerableWithSameValuesKeyboardBashTest()
        {
            var result = SmartFlagTestEnum.FromName("Five, Four, Three, One, Two, Zero, Other, Values, Ignored, asd, sdf, dfg ,dfg,asd,dfg,sadf,gh,dfgh,afd,asd,asd,af,asf,asf,,asffgfdg,,sdfg,sdg,sd,g,sdf,xcv,cvb,xzcv,asdf,ghdfcg,xc,asdf,asd,dfg,xcvb,asdf,asd,sfg,dfg,xcvb,szxdf,as,fdxc,v,sdf,sdf,sdf,sdf,xdc,sfdg,cvb,dfg,vbcndfg,sdf,das,vxc,gfds,asdf,afds,dgs", false).ToList();

            Assert.Equal("Zero", result[0].Name);
            Assert.Equal("One", result[1].Name);
            Assert.Equal("Two", result[2].Name);
            Assert.Equal("Three", result[3].Name);
            Assert.Equal("Four", result[4].Name);
            Assert.Equal("Five", result[5].Name);
        }

        [Fact]
        public void ReturnsIEnumerableWithSameValuesZeroTest()
        {
            var result = SmartFlagTestEnum.FromName("Zero, One").ToList();

            Assert.Equal("Zero", result[0].Name);
            Assert.Equal("One", result[1].Name);
        }

        [Fact]
        public void ReturnsIEnumerableWithSameValuesTryFromName()
        {
            var boolResult = SmartFlagTestEnum.TryFromName("One", out var result);

            Assert.True(boolResult);
            Assert.Equal(SmartFlagTestEnum.One, result.ToList()[0]);
        }

        [Fact]
        public void ReturnsNullWhenGivenInvalidValue()
        {
            var boolResult = SmartFlagTestEnum.TryFromName("RandomString", out var result);

            Assert.False(boolResult);
            Assert.Null(result);
        }
    }
}
