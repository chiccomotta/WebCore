using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebCore.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public int Age { get; set; }
    }
}
