// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
    using System;

    using Common.Network.Enums;

    using Dal;

    internal class Server
    {
        #region Methods

        private static void Main(string[] args)
        {
            try
            {
                var networkManager = new NetworkManager();
                networkManager.Start();

                ClientRepository clrep = new ClientRepository();
                clrep.Create(new NewClientDataBase() { Login = "d458" });
                //NewClientDataBase e = clrep.GetElement("login");

                MessageRepository msgRepository = new MessageRepository();
                msgRepository.Create(new NewMessageDataBase() { ClientFrom = "12", ClientTo = "22", MessageType = MsgType.PersonalMessage, Message = "oo2o", Date = "3425678" });

                EventRepository eventsRepository = new EventRepository();
                eventsRepository.Create(new NewEventDataBase() { Date = "1232", EventType = "121", Message = "T2ext" });

                Console.ReadLine();
                networkManager.Stop();
                networkManager.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        #endregion
    }
}
