using Backend.Data;
using Backend.Requests;
using Backend.Storage;
using Backend.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface.TCP
{
    class TCPInterface : ServerInterface
    {
        TCPConnectionManager _connectionManager;
        TCPConnectionListener _connectionListener;


        public TCPInterface(RequestManager requestManager)
            : base(requestManager)
        {
            _connectionManager = new TCPConnectionManager(requestManager);
            _connectionListener = new TCPConnectionListener(IPAddress.IPv6Any, 8576, OnConnectionReceived); // Listen on ipv6 and ipv4
        }


        internal override void Run()
        {
            _connectionListener.Start();
        }

        internal override void Stop()
        {
            _connectionListener.Stop();
            _connectionManager.Clear();
        }



        private void OnConnectionReceived(Socket connectionSocket)
        {
            _connectionManager.HandleConnection(connectionSocket);
        }
    }
}
