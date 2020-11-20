using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  //лишние юзинги

namespace Rythm.Client.BusinessLogic
{
    using Rythm.Common.Network;
    public interface ISettingConnectionController
    {
        event Action<string> MessageRecievedEvent;//опечатка

        void MessageSend(string CurrentMessage);//метод не нужен, параметр с маленькой буквы

        //void MessageReceived(MessageReceivedEventArgs e);//параметр должен быть говорящий, одна буква не пойдёт
    }
}
