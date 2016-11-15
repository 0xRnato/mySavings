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

namespace UniversalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NovaCategoria : Page
    {
        public NovaCategoria()
        {
            this.InitializeComponent();
        }

        Categoria categoria;

        private void cancelarCategoria_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void salvarCategoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (newTipo.SelectedItem == null)
                    throw new Exception("Você esqueceu de selecionar o tipo.");
                categoria.tipo = newTipo.SelectionBoxItem.ToString();
                if (string.IsNullOrEmpty(newNome.Text))
                    throw new Exception("Você esqueceu de digitar o nome da categoria.");
                categoria.nome = newNome.Text;
                App.categorias.Add(categoria);
                Frame.Navigate(typeof(MainPage));
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
