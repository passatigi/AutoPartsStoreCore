using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore
{
    class ReviewExistsException : Exception
    {
        public ReviewExistsException(string message)
        : base(message)
        { }
    }
}
