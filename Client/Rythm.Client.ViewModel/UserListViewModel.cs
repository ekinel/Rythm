// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System.Collections.ObjectModel;

    using Prism.Events;
    using Prism.Mvvm;

    public class UserListViewModel : BindableBase
    {
        #region Properties

        public ObservableCollection<UsersLoginsViewModel> UserList { get; set; }

        #endregion

        #region Constructors

        public UserListViewModel(IEventAggregator eventAggregator)
        {
            UserList = new ObservableCollection<UsersLoginsViewModel>
            {
                new UsersLoginsViewModel(eventAggregator, "Никита"),
                new UsersLoginsViewModel(eventAggregator, "Катя"),
                new UsersLoginsViewModel(eventAggregator, "Паша")
            };
        }

        #endregion
    }
}
