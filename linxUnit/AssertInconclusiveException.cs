using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linxUnit
{
    public class AssertInconclusiveException : Exception
    {
        public AssertInconclusiveException()
        {

        }

        public AssertInconclusiveException(string message)
            : base(message)
        {

        }
    }
}
