using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Backend.Interface.TCP
{
    internal class TCPConnectionListener
    {
        Socket _listener;
        Action<Socket> _callback;

        CancellationTokenSource _tokenSource;

        internal TCPConnectionListener(IPAddress bindAddress, int bindPort, Action<Socket> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");


            IPEndPoint listeningEndPoint = new IPEndPoint(bindAddress, bindPort);

            // Create a IPv6 and IPv4 agnostic socket for listening and bind it to any address
            _listener = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            _listener.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, 0);
            _listener.Bind(listeningEndPoint);

            _callback = callback;
        }


        internal void Start()
        {
            _listener.BeginAccept(AcceptCallback, _tokenSource.Token);
        }

        internal void Stop()
        {
            _tokenSource.Cancel();
            _listener.Close();
        }


        private void AcceptCallback(IAsyncResult result)
        {
            CancellationToken token = (CancellationToken)result.AsyncState;
            bool closed = token.IsCancellationRequested;
            try
            {
                // We already got the socket, so even if we're closing up we pass this one up through the callback
                Socket client = _listener.EndAccept(result);
                _callback(client);
            }
            catch (SocketException)
            {
                // TODO Log this exception
            }
            catch (ObjectDisposedException)
            {
                // We closed the socket, nothing more to do
                closed = true;
            }
            finally
            {
                // Listen again if we're still open
                if (!closed)
                    _listener.BeginAccept(AcceptCallback, token);
            }
        }

    }
}