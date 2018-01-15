using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Services
{
    public class testClass
    {
        public testClass()
        {
            
        }

        private sealed class TestClassEqualityComparer : IEqualityComparer<testClass>
        {
            public bool Equals(testClass x, testClass y)
            {
                throw new NotImplementedException();
            }

            public int GetHashCode(testClass obj)
            {
                throw new NotImplementedException();
            }
        }

        public static IEqualityComparer<testClass> TestClassComparer { get; } = new TestClassEqualityComparer();


        protected bool Equals(testClass other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((testClass) obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
