// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using System;

	internal class Server
	{
		#region Methods

		private static void Main(string[] args)
		{
			try
			{
				var networkWrapper = new NetworkWrapper();
				networkWrapper.Start();

				Console.ReadLine();

				networkWrapper.Stop();
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
