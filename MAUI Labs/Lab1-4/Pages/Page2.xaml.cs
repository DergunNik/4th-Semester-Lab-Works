using System.ComponentModel;
using System.Threading;
using Lab1.Model;

namespace Lab1
{
    public partial class Page2 : ContentPage
    {
        private CancellationTokenSource cancellationTokenSource;
        private ProgressIndicator progressIndicator;

        public Page2(ProgressIndicator progressIndicator)
        {
            this.progressIndicator = progressIndicator;
            InitializeComponent();
            bar.BindingContext = progressIndicator;
            percentLabel.BindingContext = progressIndicator;
        }

        async private void OnStartClicked(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            infoLabel.Text = "Calculations";
            try
            {
                var result = await CalculateIntegral(cancellationTokenSource.Token);
                infoLabel.Text = $"Result: {result}";
            }
            catch (Exception)
            {}
        }

        private void OnCanselClicked(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            infoLabel.Text = "Task canseled";
        }

        async private Task<double> CalculateIntegral(CancellationToken token)
        {
            double step = 0.0002, sum = 0;
            int totalSteps = (int)(1 / step);
            for (int i = 0; i < totalSteps; i++)
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(1, token);
                var x = i * step;       
                sum += Math.Sin(x) * step;
                for (int j = 0; j < 1000; j++)
                {
                    double temp = j * j;
                }
                progressIndicator.Progress = (double)(i + 1) / totalSteps;
            }
            return sum;
        }
    }

}
