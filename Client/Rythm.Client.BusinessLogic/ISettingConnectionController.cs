using System;

namespace Rythm.Client.BusinessLogic
{
    public interface ISettingConnectionController
    {
        event Action<string> MessageReceivedEvent;

        void MessageSend(string currentMessage);//метод не нужен, параметр с маленькой буквы
    }
}
