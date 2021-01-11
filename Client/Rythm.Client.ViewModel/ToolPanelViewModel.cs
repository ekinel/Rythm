// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System;
	using System.Windows;
	using System.Windows.Input;

	using Events;

	using Prism.Commands;
	using Prism.Events;
	using Prism.Mvvm;
	using Rythm.Client.ViewModel.Properties;

	public class ToolPanelViewModel : BindableBase
	{
		#region Fields

		private string _login;
		private string _buttonContent;
		private bool _changeButtonContent;
		private readonly IEventAggregator _eventAggregator;

		private bool _buttonsStackPanelVisibility;
		private Visibility _stackPanelVisibility;
		private bool _changedLoginVisibility;
		private Visibility _loginVisibility;

		private bool _colorTheme;

		#endregion

		#region Properties

		public ICommand ChangeSettingsVisibilityCommand { get; }
		public ICommand DisplayingEventsVisibilityCommand { get; }
		public ICommand ShowClientsListCommand { get; }
		public ICommand ShowMessagesListCommand { get; }
		public ICommand ShowEventsListCommand { get; }
		public ICommand ChangeThemeColorCommand { get; }

		public string Login
		{
			get => _login;
			set => SetProperty(ref _login, value);
		}

		public Visibility SplitterVisibility
		{
			get => _stackPanelVisibility;
			set => SetProperty(ref _stackPanelVisibility, value);
		}

		public Visibility LoginVisibility
		{
			get => _loginVisibility;
			set => SetProperty(ref _loginVisibility, value);
		}

		public bool IsLightTheme
		{
			get => _colorTheme;
			set => SetProperty(ref _colorTheme, value);
		}
		public string ButtonContent
		{
			get => _buttonContent;
			set => SetProperty(ref _buttonContent, value);
		}
		#endregion

		#region Constructors

		public ToolPanelViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
			_eventAggregator.GetEvent<NewClientChosenViewModel>().Subscribe(HandleNewLoginSelected);
			ChangeSettingsVisibilityCommand = new DelegateCommand(ExecuteChangeSettingsVisibilityCommand);
			ShowClientsListCommand = new DelegateCommand(ExecuteShowClientsListCommand);
			ShowEventsListCommand = new DelegateCommand(ExecuteShowEventsListCommand);
			ShowMessagesListCommand = new DelegateCommand(ExecuteShowMessagesListCommand);
			DisplayingEventsVisibilityCommand = new DelegateCommand(ExecuteDisplayingEventsVisibilityCommand);
			ChangeThemeColorCommand = new DelegateCommand(ExecuteChangeThemeCommand);

			SplitterVisibility = Visibility.Collapsed;
			LoginVisibility = Visibility.Visible;
			_changedLoginVisibility = true;
			IsLightTheme = true;
			_changeButtonContent = true;
			ButtonContent = Resources.EventsButton;
		}

		#endregion

		#region Methods

		public void HandleNewLoginSelected(string login)
		{
			Login = login;
		}

		private void ExecuteChangeThemeCommand()
		{
			IsLightTheme = !IsLightTheme;

			string _mainWindowTheme = IsLightTheme ? "CustomWindowChromeLight" : "CustomWindowChromeDark";
			string _toolPanelTheme = IsLightTheme ? "ButtonStyleLight" : "ButtonStyleDark";
			string _connectionParametrsTheme = IsLightTheme ? "CommonBackgroundStyleLight" : "CommonBackgroundStyleDark";
			string _chatListBoxItemTheme = IsLightTheme ? "ChatListBoxItemStyleLight" : "ChatListBoxItemStyleDark";
			string _clientsListBoxItemTheme = IsLightTheme ? "ClientsListBoxItemStyleLight" : "ClientsListBoxItemStyleDark";
			string _clientsListBoxTheme = IsLightTheme ? "ClientsListBoxBackgroundStyleLight" : "ClientsListBoxBackgroundStyleDark";
			string _chatListBoxTheme = IsLightTheme ? "ChatListBoxBackgroundStyleLight" : "ChatListBoxBackgroundStyleDark";
			string _textBoxTheme = IsLightTheme ? "TextBoxStyleLight" : "TextBoxStyleDark";
			string _textBlockTheme = IsLightTheme ? "TextBlockStyleLight" : "TextBlockStyleDark";


			 var uriMainWindow = new Uri(@"../../Resources/" + _mainWindowTheme + ".xaml", UriKind.Relative);
			var uriToolPanel = new Uri(@"../../Resources/" + _toolPanelTheme + ".xaml", UriKind.Relative);
			var uriConnectionParams = new Uri(@"../../Resources/" + _connectionParametrsTheme + ".xaml", UriKind.Relative);
			var uriChatListBoxItem = new Uri(@"../../Resources/" + _chatListBoxItemTheme + ".xaml", UriKind.Relative);
			var uriClientsListBoxItem = new Uri(@"../../Resources/" + _clientsListBoxItemTheme + ".xaml", UriKind.Relative);
			var uriClientsListBox = new Uri(@"../../Resources/" + _clientsListBoxTheme + ".xaml", UriKind.Relative);
			var uriChatListBox = new Uri(@"../../Resources/" + _chatListBoxTheme + ".xaml", UriKind.Relative);
			var uriTextBoxBox = new Uri(@"../../Resources/" + _textBoxTheme + ".xaml", UriKind.Relative);
			var uriTextBlockBox = new Uri(@"../../Resources/" + _textBlockTheme + ".xaml", UriKind.Relative);

			ResourceDictionary resourceDictMainWindow = Application.LoadComponent(uriMainWindow) as ResourceDictionary;
			ResourceDictionary resourceDictToolPanel = Application.LoadComponent(uriToolPanel) as ResourceDictionary;
			ResourceDictionary resourceDictConnectionParams = Application.LoadComponent(uriConnectionParams) as ResourceDictionary;
			ResourceDictionary resourceDictChatListBoxItem = Application.LoadComponent(uriChatListBoxItem) as ResourceDictionary;
			ResourceDictionary resourceDictClientsListBoxItem = Application.LoadComponent(uriClientsListBoxItem) as ResourceDictionary;
			ResourceDictionary resourceDictClientsListBox = Application.LoadComponent(uriClientsListBox) as ResourceDictionary;
			ResourceDictionary resourceDictChatListBox = Application.LoadComponent(uriChatListBox) as ResourceDictionary;
			ResourceDictionary resourceDictTextBox = Application.LoadComponent(uriTextBoxBox) as ResourceDictionary;
			ResourceDictionary resourceDictTextBlock = Application.LoadComponent(uriTextBlockBox) as ResourceDictionary;

			Application.Current.Resources.Clear();

			Application.Current.Resources.MergedDictionaries.Add(resourceDictMainWindow);
			Application.Current.Resources.MergedDictionaries.Add(resourceDictToolPanel);
			Application.Current.Resources.MergedDictionaries.Add(resourceDictConnectionParams);
			Application.Current.Resources.MergedDictionaries.Add(resourceDictChatListBoxItem);
			Application.Current.Resources.MergedDictionaries.Add(resourceDictClientsListBoxItem);
			Application.Current.Resources.MergedDictionaries.Add(resourceDictClientsListBox);
			Application.Current.Resources.MergedDictionaries.Add(resourceDictChatListBox);
			Application.Current.Resources.MergedDictionaries.Add(resourceDictTextBox);
			Application.Current.Resources.MergedDictionaries.Add(resourceDictTextBlock);
		}

		private void ExecuteShowClientsListCommand()
		{
			_eventAggregator.GetEvent<DataBaseButtonClientsChosen>().Publish();
		}

		private void ExecuteShowMessagesListCommand()
		{
			_eventAggregator.GetEvent<DataBaseButtonMessagesChosen>().Publish();
		}

		private void ExecuteShowEventsListCommand()
		{
			_eventAggregator.GetEvent<DataBaseButtonEventsChosen>().Publish();
		}

		private void ExecuteChangeSettingsVisibilityCommand()
		{
			_eventAggregator.GetEvent<ConnectionPanelVisibilityChangedEvent>().Publish();
		}

		private void ExecuteDisplayingEventsVisibilityCommand()
		{
			_changeButtonContent = !_changeButtonContent;
			ButtonContent = _changeButtonContent ? Resources.EventsButton : Resources.ChatButton;

			_eventAggregator.GetEvent<DisplayingEventsVisibility>().Publish();

			_buttonsStackPanelVisibility = !_buttonsStackPanelVisibility;
			SplitterVisibility = _buttonsStackPanelVisibility ? Visibility.Visible : Visibility.Collapsed;

			_changedLoginVisibility = !_changedLoginVisibility;
			LoginVisibility = _changedLoginVisibility ? Visibility.Visible : Visibility.Hidden;
		}

		#endregion
	}
}
