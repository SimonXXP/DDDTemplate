using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Models
{
    public class EntityProperty
    {

        public string Name { get; set; }

        public Type PropertyType { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool Nullable { get; set; }

        

    }
}