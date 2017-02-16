using Microsoft.VisualStudio.Docker.Shared.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Microsoft.VisualStudio.Docker.Shared.UI.Scaffolding
{
    public enum TargetOS { Linux, NanoServer }

    public class DockerScaffoldingViewModel : CloseableDialogViewModel
    {
        private TargetOS _selectedTargetOS;
        private bool _showComposeProjectsList;
        private ObservableCollection<DockerComposeProject> _availableDockerComposeProjects;

        public DockerScaffoldingViewModel(DockerScaffoldingModel model)
        {
            SelectedTargetOS = model.DefaultTargetOS;
            _availableDockerComposeProjects = new ObservableCollection<DockerComposeProject>(model.DockerComposeProjects);

            _showComposeProjectsList = AvailableDockerComposeProjects.Count > 1;
            SaveCommand = new RelayCommand(OnSave);
        }
        public TargetOS SelectedTargetOS
        {
            get
            {
                return _selectedTargetOS;
            }
            set
            {
                _selectedTargetOS = value;
                base.RaisePropertyChanged();
            }
        }

        public ObservableCollection<DockerComposeProject> AvailableDockerComposeProjects
        {
            get
            {
                return _availableDockerComposeProjects;
            }
            set
            {
                if (value != _availableDockerComposeProjects)
                {
                    _availableDockerComposeProjects = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool ShowComposeProjectsList
        {
            get
            {
                return _showComposeProjectsList;
            }
            set
            {
                _showComposeProjectsList = value;
                base.RaisePropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public void OnSave(object paramter)
        {
            this.DialogWindow.Close(true);
        }
    }
}
