namespace Rythm.Client.BusinessLogic
{
    using Rythm.Common.Network;

    public interface IConnectionServiceController
    {
        ITransport CurrentTransport { get; }
        void DataSending(string address, string port, string login);

        string Login { get; set; }

    }
}
