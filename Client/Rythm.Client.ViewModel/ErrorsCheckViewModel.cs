// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    using Prism.Mvvm;

    public abstract class ErrorsCheckViewModel : BindableBase, INotifyDataErrorInfo
    {
        #region Fields

        protected readonly ErrorsContainer<string> _errorsContainer;

        #endregion

        #region Properties

        public bool HasErrors => _errorsContainer.HasErrors;

        #endregion

        #region Events

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion

        #region Constructors

        protected ErrorsCheckViewModel()
        {
            _errorsContainer = new ErrorsContainer<string>(RaiseErrorsChanged);
        }

        #endregion

        #region Methods

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsContainer.GetErrors(propertyName);
        }

        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}
