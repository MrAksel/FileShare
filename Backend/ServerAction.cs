using Backend.Data;
using Backend.Requests;
using Backend.Storage;
using Backend.Users;
using System;

namespace Backend
{
    internal struct ServerAction
    {
        private bool _exitServer;
        private Action<RequestManager> _action;
        

        internal bool ExitServer { get { return _exitServer; } }
        internal bool IsEmpty { get { return ExitServer == false && _action == null; } }


        internal static ServerAction Empty = new ServerAction(false, null);
        internal static ServerAction Exit = new ServerAction(true, null);


        internal ServerAction(bool exitServer, Action<RequestManager> action)
        {
            _exitServer = exitServer;
            _action = action;
        }


        internal void PerformAction(RequestManager requestManager)
        {
            if (_action != null)
            {
                _action(requestManager);
            }
        }
    }
}