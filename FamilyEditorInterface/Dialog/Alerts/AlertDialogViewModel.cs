﻿using System.Collections.Generic;
using System.Windows.Input;
using Dialog.Service;
using FamilyEditorInterface.WPF;

namespace Dialog.Alerts
{
    public class AlertDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand OKCommand { get; private set; }

        public AlertDialogViewModel(string title, List<Message> note) : this(title, string.Empty, note) { }
        public AlertDialogViewModel(string title, string message, List<Message> note) : base(title, message, note)
        {
            OKCommand = new RelayCommand(OK);
        }

        private void OK(object window)
        {
            CloseDialogWithResult(window as IDialogWindow, DialogResults.Undefined);
        }
    }
}
