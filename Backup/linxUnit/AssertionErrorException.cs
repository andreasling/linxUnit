using System;
using System.Collections.Generic;
using System.Text;

namespace linxUnit
{
    public class AssertionErrorException : Exception
    {
        public AssertionErrorException()
            : base()
        {
        }

        public AssertionErrorException(string message)
            : base(message)
        {
        }
    }

}
