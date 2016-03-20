using Backend.Data;
using Backend.Storage;
using Backend.Users;
using System;

namespace Backend
{
    internal struct ServerAction
    {
        private bool _exitServer;
        private Action<UserManager, FileManager, DataManager> _action;
        

        internal bool ExitServer { get { return _exitServer; } }
        internal bool IsEmpty { get { return ExitServer == false && PerformAction == null; } }
        internal Action<UserManager, FileManager, DataManager> PerformAction { get { return _action; } }


        internal static ServerAction Empty = new ServerAction(false, null);
        internal static ServerAction Exit = new ServerAction(true, null);


        internal ServerAction(bool exitServer, Action<UserManager, FileManager, DataManager> action)
        {
            _exitServer = exitServer;
            _action = action;
        }
    }
}