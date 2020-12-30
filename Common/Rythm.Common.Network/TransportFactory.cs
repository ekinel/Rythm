
namespace Rythm.Common.Network
{
    using Enums;
	using Rythm.Common.Network.Interfaces;
	using System;
    public static class TransportFactory
    {
        public static ITransport Create(TransportType type)
        {
            switch (type)
            {
                case TransportType.WebSocket:
                    return new WsClient();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
