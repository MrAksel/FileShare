using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    /// <summary>
    /// Marks a point in the program that never should have been reached.
    /// </summary>
    class ThisShouldNeverHappenException : Exception
    {
        public ThisShouldNeverHappenException(string message) : base(message)
        {
        }
    }
}
