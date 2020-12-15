﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
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

	public class ToolPanelViewModel : BindableBase
	{
		#region Fields

		private string _login;
		private readonly IEventAggregator _eventAggregator;

		private bool _buttonsStackPanelVisibility;
		private Visibility _stackPanelVisibility;

		#endregion

		#region Properties

		public ICommand ChangeSettingsVisibilityCommand { get; }
		public ICommand DisplayingEventsVisibilityCommand { get; }

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

		#endregion

		#region Constructors

		public ToolPanelViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
			_eventAggregator.GetEvent<NewClientChosenViewModel>().Subscribe(HandleNewLoginSelected);
			ChangeSettingsVisibilityCommand = new DelegateCommand(ExecuteChangeSettingsVisibilityCommand);
			DisplayingEventsVisibilityCommand = new DelegateCommand(ExecuteDisplayingEventsVisibilityCommand);

			SplitterVisibility = Visibility.Collapsed;
		}

		#endregion

		#region Methods

		public void HandleNewLoginSelected(string login)
		{
			Login = login;
		}

		private void ExecuteChangeSettingsVisibilityCommand()
		{
			_eventAggregator.GetEvent<ConnectionPanelVisibilityChangedEvent>().Publish();
		}

		private void ExecuteDisplayingEventsVisibilityCommand()
		{
			_eventAggregator.GetEvent<DisplayingEventsVisibility>().Publish();

			_buttonsStackPanelVisibility = !_buttonsStackPanelVisibility;
			SplitterVisibility = _buttonsStackPanelVisibility ? Visibility.Visible : Visibility.Collapsed;
		}

		#endregion
	}
}