using System;

namespace Rythm.Client.BusinessLogic
{
    public interface IMessagingServiceController
    {
        event Action<string> MessageReceivedEvent;
        void MessageSend(string currentMessage);
    }
}
