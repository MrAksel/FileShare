using Backend.Data;
using Backend.Requests;
using Backend.Storage;
using Backend.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface
{
    abstract class ServerInterface
    {

        protected internal virtual RequestManager RequestManager { get; protected set; }

        internal ServerInterface(RequestManager requestManager)
        {
            RequestManager = requestManager;
        }

        internal abstract void Run();
        internal abstract void Stop();
    }
}
