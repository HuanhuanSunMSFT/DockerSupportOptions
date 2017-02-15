namespace Microsoft.VisualStudio.Docker.Shared.UI.Scaffolding
{
    public partial class DockerScaffoldingDialog
    {

        public DockerScaffoldingDialog(DockerScaffoldingViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

        }
    }
}
