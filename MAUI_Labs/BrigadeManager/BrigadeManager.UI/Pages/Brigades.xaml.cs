using System.Diagnostics;

namespace BrigadeManager.UI.Pages;

public partial class Brigades : ContentPage
{
    public Brigades(ViewModels.BrigadesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}