using System;

namespace Backend.Interface.TCP
{
    /// <summary>
    /// Describes the current state of the TCPConnectionHandler
    /// </summary>
    class TCPConnectionStatus
    {
        private ConnectionStatus _currentStatus;
        private Action<ConnectionStatus> _onClosedDelegate;


        internal TCPConnectionStatus(Action<ConnectionStatus> onClosedDelegate)
        {
            _currentStatus = ConnectionStatus.Open;
            _onClosedDelegate = onClosedDelegate;
        }

        
        internal void SetStatus(ConnectionStatus status)
        {
            _currentStatus = status;
            OnStatusUpdate(_currentStatus);
        }

        private void OnStatusUpdate(ConnectionStatus status)
        {
            switch (status)
            {
                case ConnectionStatus.ClientSentAck2:
                    _currentStatus = ConnectionStatus.Closed;
                    OnConnectionClosed(ConnectionStatus.ClientSentAck2);
                    break;
                case ConnectionStatus.HostSentAck2:
                    _currentStatus = ConnectionStatus.Closed;
                    OnConnectionClosed(ConnectionStatus.HostSentAck2);
                    break;
            }
        }

        private void OnConnectionClosed(ConnectionStatus cause)
        {
            if (_onClosedDelegate != null)
            {
                _onClosedDelegate(cause);
            }
        }
    }
}