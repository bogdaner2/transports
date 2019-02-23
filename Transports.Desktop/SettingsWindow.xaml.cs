using System;
using System.Windows;
using System.Windows.Controls;

namespace Transports.Desktop
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void OnOk(object sender, RoutedEventArgs e)
        {
            Enum.TryParse((TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(), out StoreType storeType);
            Enum.TryParse((SerializationComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(), out SerializationProvider serializationProvider);

            StateService.SetStoreType(storeType);
            StateService.SetSerializationProvider(serializationProvider);

            MainWindow main = new MainWindow();
            main.Show();

            Close();
        }
    }
}
