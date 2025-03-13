namespace BrigadeManager.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Pages.WorkDetails), typeof(Pages.WorkDetails));
            Routing.RegisterRoute(nameof(Pages.AddOrEditBrigade), typeof(Pages.AddOrEditBrigade));
            Routing.RegisterRoute(nameof(Pages.AddOrEditWork), typeof(Pages.AddOrEditWork));
        }
    }
}
