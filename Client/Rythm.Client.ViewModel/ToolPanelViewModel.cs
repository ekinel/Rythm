// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System;
	using System.Collections.Generic;
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

			List<string> _styles = new List<string>
			{
				IsLightTheme ? "CustomWindowChromeLight" : "CustomWindowChromeDark",
				IsLightTheme ? "ButtonStyleLight" : "ButtonStyleDark",
				IsLightTheme ? "CommonBackgroundStyleLight" : "CommonBackgroundStyleDark",
				IsLightTheme ? "ChatListBoxItemStyleLight" : "ChatListBoxItemStyleDark",
				IsLightTheme ? "ClientsListBoxItemStyleLight" : "ClientsListBoxItemStyleDark",
				IsLightTheme ? "ClientsListBoxBackgroundStyleLight" : "ClientsListBoxBackgroundStyleDark",
				IsLightTheme ? "ChatListBoxBackgroundStyleLight" : "ChatListBoxBackgroundStyleDark",
				IsLightTheme ? "TextBoxStyleLight" : "TextBoxStyleDark",
				IsLightTheme ? "TextBlockStyleLight" : "TextBlockStyleDark"
		};

			Application.Current.Resources.Clear();

			foreach (var element in _styles)
			{
				var uri = new Uri(@"../../Resources/" + element + ".xaml", UriKind.Relative);
				ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
				Application.Current.Resources.MergedDictionaries.Add(resourceDict);
			}
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
