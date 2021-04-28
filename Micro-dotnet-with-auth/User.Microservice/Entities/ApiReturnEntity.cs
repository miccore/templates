using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Microservice.Entities
{
    public class ApiReturnEntity
    {
        public string status { get; set; }
        public Object data {get; set;}
    }
}
