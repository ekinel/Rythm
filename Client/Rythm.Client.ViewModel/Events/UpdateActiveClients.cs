// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel.Events
{
	using Prism.Events;
	using System.Collections.Generic;

	internal class UpdateActiveClients : PubSubEvent<List<string>>
	{
	}
}
