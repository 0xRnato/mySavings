using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace mySavings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NovaConta : Page
    {
        public NovaConta()
        {
            this.InitializeComponent();
        }

        Conta conta;

        private void cancelarConta_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void salvarConta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(newNome.Text))
                    throw new Exception("Você esqueceu de digitar o nome da conta.");
                conta.nome = newNome.Text;
                if (newSaldo.Text == "")
                    throw new Exception("Você esqueceu de digitar o Saldo.");
                conta.saldo = float.Parse(newSaldo.Text);
                App.contas.Add(conta);
                Frame.Navigate(typeof(MainPage));
            }
            catch (FormatException)
            {
                var messageDialog = new MessageDialog("O Saldo está em um formato incorreto.");
                await messageDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
