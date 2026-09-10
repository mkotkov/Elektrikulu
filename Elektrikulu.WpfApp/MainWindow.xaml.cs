using Elektrikulu.ClassLibrary;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Elektrikulu.WpfApp
{
    public partial class MainWindow : Window
    {
        private static readonly Brush NormalBorder = Brushes.Gray;
        private static readonly Brush ErrorBorder = Brushes.Red;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Input_Changed(object sender, RoutedEventArgs e)
        {
            Recalculate();
        }

        private void Recalculate()
        {
            bool tarbimineOk = ValidateField(tarbimine, tarbimineError,
                "Sisesta korrektne kogus", out decimal tarbimine_dec);

            bool borsihindOk = ValidateField(borsihind, borsihindError,
                "Sisesta korrektne börsihind", out decimal borsihind_dec);

            bool kaibemaksOk = ValidateField(kaibemaks, kaibemaksError,
                "Sisesta korrektne käibemaks", out decimal kaibemaks_dec);

            if (!tarbimineOk || !borsihindOk || !kaibemaksOk)
            {
                if (arve_kokku != null)
                {
                    arve_kokku.Content = "0.00 €";
                }
                return;
            }

            bool kaibemaksChecked = kaibemaks_check?.IsChecked ?? false;
            decimal tulemus = Arve.Arve_lugemine(
                tarbimine_dec, borsihind_dec, kaibemaks_dec,
                kaibemaksChecked);

            if (arve_kokku != null)
            {
                arve_kokku.Content = $"{tulemus} €";
            }
        }

        private bool ValidateField(System.Windows.Controls.TextBox box,
            System.Windows.Controls.TextBlock errorText,
            string errorMessage, out decimal value)
        {
           

            if (box == null) { value = 0; return false; }
            string normalized = box.Text.Replace(",", ".").Replace(" ", "");
            bool ok = decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out value) && value >= 0;

            if (ok)
            {
                box.BorderBrush = NormalBorder;
                if (errorText != null)
                {
                    errorText.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                box.BorderBrush = ErrorBorder;
                if (errorText != null)
                {
                    errorText.Text = errorMessage;
                    errorText.Visibility = Visibility.Visible;
                }
            }

            return ok;
        }

        private void Tühsta_Click(object sender, RoutedEventArgs e)
        {
            tarbimine.Text = "0";
            borsihind.Text = "0";
            kaibemaks.Text = "24";
            kaibemaks_check.IsChecked = false;

            tarbimine.BorderBrush = NormalBorder;
            borsihind.BorderBrush = NormalBorder;
            kaibemaks.BorderBrush = NormalBorder;
            tarbimineError.Visibility = Visibility.Collapsed;
            borsihindError.Visibility = Visibility.Collapsed;
            kaibemaksError.Visibility = Visibility.Collapsed;

            arve_kokku.Content = "0.00 €";
        }
    }
}