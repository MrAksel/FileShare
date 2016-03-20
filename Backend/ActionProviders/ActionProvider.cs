using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ActionProviders
{
    abstract class ActionProvider
    {

        internal abstract ServerAction Dequeue();
    }
}
