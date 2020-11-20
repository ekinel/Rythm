using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Client.BusinessLogic
{
    using Rythm.Common.Network;
    public interface ISettingConnectionController
    {
        event Action<string> MessageRecievedEvent;

        void MessageSend(string CurrentMessage);

        void MessageReceived(MessageReceivedEventArgs e);
    }
}
