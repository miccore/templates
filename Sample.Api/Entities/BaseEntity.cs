using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Miccore.Net.webapi_template.Sample.Api.Entities
{
    public abstract class BaseEntity
    {
        public int? Id { get; set; }
        
        public int? CreatedAt { get; set;}
        
        public int? UpdatedAt { get; set;}
        
        public int? DeletedAt { get; set;}
    }
}
