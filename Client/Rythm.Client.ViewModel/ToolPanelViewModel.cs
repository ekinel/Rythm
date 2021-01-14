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

		private bool _gridWithEventsButtonVisibility;
		private bool _changedLoginVisibility;

		private bool _colorTheme;

		#endregion

		#region Properties

		public ICommand ChangeSettingsVisibilityCommand { get; }
		public ICommand DisplayingEventsVisibilityCommand { get; }
		public ICommand ShowClientsListCommand { get; }
		public ICommand ShowMessagesListCommand { get; }
		public ICommand ShowEventsListCommand { get; }
		public ICommand ChangeThemeColorCommand { get; }

		public bool IsGridWithEventsButtonVisible
		{
			get => _gridWithEventsButtonVisibility;
			set => SetProperty(ref _gridWithEventsButtonVisibility, value);

		}
		public bool IsLoginVisible
		{
			get => _changedLoginVisibility;
			set => SetProperty(ref _changedLoginVisibility, value);

		}
		public string Login
		{
			get => _login;
			set => SetProperty(ref _login, value);
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

			IsGridWithEventsButtonVisible = false;
			IsLoginVisible = true;
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
				IsLightTheme ? Resources.CustomWindowChromeLight : Resources.CustomWindowChromeDark,
				IsLightTheme ? Resources.ButtonStyleLight : Resources.ButtonStyleDark,
				IsLightTheme ? Resources.CommonBackgroundStyleLight : Resources.CommonBackgroundStyleDark,
				IsLightTheme ? Resources.ChatListBoxItemStyleLight : Resources.ChatListBoxItemStyleDark,
				IsLightTheme ? Resources.ClientsListBoxItemStyleLight : Resources.ClientsListBoxItemStyleDark,
				IsLightTheme ? Resources.ClientsListBoxBackgroundStyleLight : Resources.ClientsListBoxBackgroundStyleDark,
				IsLightTheme ? Resources.ChatListBoxBackgroundStyleLight : Resources.ChatListBoxBackgroundStyleDark,
				IsLightTheme ? Resources.TextBoxStyleLight : Resources.TextBoxStyleDark,
				IsLightTheme ? Resources.TextBlockStyleLight : Resources.TextBlockStyleDark,
				IsLightTheme ? Resources.ScrollViewerStyleLight : Resources.ScrollViewerStyleDark,
				IsLightTheme ? Resources.LoginTextBlockStyleLight : Resources.LoginTextBlockStyleDark,
		};

			Application.Current.Resources.Clear();

			foreach (var element in _styles)
			{
				var uri = new Uri(@"../../Resources/" + element, UriKind.Relative);
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

			IsGridWithEventsButtonVisible = !IsGridWithEventsButtonVisible;
			IsLoginVisible = !IsLoginVisible;

			_eventAggregator.GetEvent<DisplayingEventsVisibility>().Publish();
		}
		#endregion
	}
}
