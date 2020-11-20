namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows.Input;

    using System.Collections.ObjectModel;

    using Prism.Mvvm;
    using Prism.Commands;

    using Rythm.Common.Network;
    using Rythm.Client.BusinessLogic;
    public class MessageViewModel : BindableBase
    {
        private readonly ISettingConnectionController _settingConnectionController;

        private string _currentMessage;
        private string _chatMessages;
        public ICommand SendCommand { get; }

        public ObservableCollection<Message> MessagesList = new ObservableCollection<Message>();
        public string CurrentMessage
        {
            get => _currentMessage;
            set => SetProperty(ref _currentMessage, value);
        }
        public string ChatMessages
        {
            get => _chatMessages;
            set => SetProperty(ref _chatMessages, value);
        }

        public void HandleNewMessageRecieved(string message)
        {
            MessagesList.Add(new Message { Name = "User1", Text = message });

        }
        public MessageViewModel(ISettingConnectionController settingConnectionController)
        {
            SendCommand = new DelegateCommand(ExecuteCommand);
            _settingConnectionController = settingConnectionController ?? throw new ArgumentNullException(nameof(settingConnectionController));
            _settingConnectionController.MessageReceivedEvent += HandleNewMessageRecieved;
        }
        private void ExecuteCommand()//лишнее удаляем
        {
            //_currentTransport?.Send("\n" + CurrentMessage);
            _settingConnectionController.MessageSend("\n" + CurrentMessage);
            CurrentMessage = string.Empty;
        }

        public void MessageReceived(MessageReceivedEventArgs e)//говорящий аргумент
        {
            //ChatMessages += ("\n" + e.Message); - my -  выводило текст на экран
        }
    }

    public class Message : BindableBase
    {
        private string _name;
        private string _text;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

}
}