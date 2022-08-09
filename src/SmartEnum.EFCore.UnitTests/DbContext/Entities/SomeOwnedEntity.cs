using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnum.EFCore.IntegrationTests.DbContext.Entities
{
    [Owned]
    public class SomeOwnedEntity
    {
        public int Value { get; set; }

        public Weekday Weekday { get; set; }
    }
}
