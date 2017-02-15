namespace Microsoft.VisualStudio.Docker.Shared.UI.Scaffolding
{
    /// <summary>
    /// Interaction logic for ScaffoldingDialog.xaml
    /// </summary>
    public partial class ScaffoldingDialog
    {

        public ScaffoldingDialog(ScaffoldingViewModel viewModel)
        {
            InitializeComponent();


            DataContext = viewModel;

        }
    }
}
