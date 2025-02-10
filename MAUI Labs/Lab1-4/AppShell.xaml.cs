namespace Lab1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Page1", typeof(Page1));
            Routing.RegisterRoute("Page2", typeof(Page2));
        }
    }
}
