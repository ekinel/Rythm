namespace Rythm.Client.ViewModel
{
    using Prism.Mvvm;

    public class SendMessage : BindableBase
    {
        private string _name;
        private string _text;

        public string Name { get => _name; }

        public string Text { get => _text; }

        public SendMessage(string name, string text)
        {
            _name = name;
            _text = text;
        }
    }
}
