// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
	using System;

	using Interfaces;

	public class ToolPanelDisplayController : IToolPanelDisplayController
	{
		#region Fields

		private string _login;

		#endregion

		#region Properties

		public string Login
		{
			get => _login;
			set
			{
				_login = value ?? throw new ArgumentNullException(nameof(value));
				NewUserLoginEvent?.Invoke(_login);
			}
		}

		#endregion

		#region Events

		public event Action<string> NewUserLoginEvent;

		#endregion
	}
}
