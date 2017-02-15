//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.VisualStudio.PlatformUI;
using System.Windows;

namespace Microsoft.VisualStudio.Docker.Shared.UI
{
    /// <summary>
    /// CloseableDialogWindow<T> assumes a view model of type T to be set as DataContext.
    /// </summary>
    public class CloseableDialogWindow<T> : DialogWindow, ICloseableDialogWindow
        where T : CloseableDialogViewModel
    {
        public CloseableDialogWindow()
        {
            DataContextChanged += OnDataContextChanged;
        }

        public new T DataContext
        {
            get
            {
                return (T)base.DataContext;
            }
            set
            {
                base.DataContext = value;
            }
        }

        public void Close(bool result)
        {
            DialogResult = result;
            Close();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                ((T)e.NewValue).DialogWindow = this;
            }

            if (e.OldValue != null)
            {
                ((T)e.OldValue).DialogWindow = null;
            }
        }
    }
}
