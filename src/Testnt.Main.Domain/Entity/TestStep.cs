using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class TestStep: BaseEntity
    {
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
