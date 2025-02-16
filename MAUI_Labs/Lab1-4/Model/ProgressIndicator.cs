using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Model
{
    public class ProgressIndicator : INotifyPropertyChanged
    {
        private double progress;
        public double Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Percent));
            }
        }

        public double Percent
        {
            get => Double.Round(progress * 100, 3);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
