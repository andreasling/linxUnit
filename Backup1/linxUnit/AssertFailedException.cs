using System;
using System.Collections.Generic;
using System.Text;

namespace linxUnit
{
    public class AssertFailedException : Exception
    {
        public AssertFailedException()
            : base()
        {
        }

        public AssertFailedException(string message)
            : base(message)
        {
        }
    }

}
