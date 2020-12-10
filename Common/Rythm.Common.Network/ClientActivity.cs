// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
    using System;
    public class ClientActivity
    {
        private string Login { get; set; }

        public string LastActivityTime { get; }

        public ClientActivity(string login)
        {
            Login = login;
            LastActivityTime = DateTime.Now.ToString();
        }
    }
}
