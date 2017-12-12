using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Models
{
    public class DataBucket<T>
    {
        public string Nome { get; set; }
        public T Data { get; set; }
    }
}
