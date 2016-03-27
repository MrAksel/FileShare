using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Backend.Users;
using Backend.Requests;

namespace Backend.Interface.TCP
{
    internal class TCPConnectionManager
    {
        object l_connections;

        bool _closing;

        RequestManager _requestManager;
        List<TCPConnectionHandler> _connections;


        internal TCPConnectionManager(RequestManager requestManager)
        {
            l_connections = new object();

            _connections = new List<TCPConnectionHandler>();
            _requestManager = requestManager;
        }


        internal void HandleConnection(Socket connectionSocket)
        {
            // TODO Check race condition when we are closing
            TCPConnectionHandler handler = new TCPConnectionHandler(connectionSocket, _requestManager);

            lock (l_connections)
            {
                if (_closing)
                    handler.InitiateClose();
                else
                    _connections.Add(handler);
            }
        }

        // Close every current connection
        internal void Clear()
        {
            _closing = true;
            lock (l_connections)
            {
                foreach (TCPConnectionHandler handler in _connections)
                {
                    handler.InitiateClose();
                }
                _connections.Clear();
            }
        }
    }
}