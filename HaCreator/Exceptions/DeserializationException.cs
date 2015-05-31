using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.Exceptions
{
    public class DeserializationException : Exception
    {
        public DeserializationException() : base() { }
        public DeserializationException(string message) : base(message) { }
    }
}
