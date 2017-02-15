using Microsoft.VisualStudio.Docker.Shared.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Microsoft.VisualStudio.Docker.Shared.UI.Scaffolding
{
    public enum TargetOS { Linux, NanoServer }

    public class ScaffoldingViewModel : CloseableDialogViewModel, IDisposable
    {
        private TargetOS _selectedTargetOS;

        public ScaffoldingViewModel()
        {
            SelectedTargetOS = TargetOS.NanoServer;

            SaveCommand = new RelayCommand(OnOk);
        }

        /// <summary>
        /// The button selected by the user.
        /// </summary>
        public TargetOS SelectedTargetOS
        {
            get { return _selectedTargetOS; }

            set
            {
                _selectedTargetOS = value;
                base.RaisePropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public void OnOk(object paramter)
        {
            this.DialogWindow.Close(true);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
