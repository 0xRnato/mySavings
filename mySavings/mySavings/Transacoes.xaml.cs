using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Transacoes : Page
    {
        public Transacoes()
        {
            this.InitializeComponent();
        }

        private async void addTransacao_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (App.contas.Count == 0)
            //        throw new Exception("Adicione uma conta antes de fazer uma transação.");
            //    if (App.categorias.Count == 0)
            //        throw new Exception("Adicione uma categoria antes de fazer uma transação.");
            //    Frame.Navigate(typeof(NovaTransacao));
            //    App.paginaAtual = 1;
            //}
            //catch (Exception ex)
            //{
            //    MessageDialog message = new MessageDialog(ex.Message);
            //    await message.ShowAsync();
            //}
        }
    }
}
