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
    public sealed partial class Categorias : Page
    {
        public Categorias()
        {
            this.InitializeComponent();
        }

        private void addCategoria_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NovaCategoria));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            categoriasFlip.Items.Clear();
            FlipViewItem categoriaFlipItem = new FlipViewItem();
            ScrollViewer scrollcategoria = new ScrollViewer();
            StackPanel painelcategoria = new StackPanel();
            scrollcategoria.Margin = new Thickness(0, 0, 0, 50);

            if (App.categorias.Count == 0)
            {
                TextBlock semCategorias = new TextBlock();
                semCategorias.Text = "Sem Categorias.";
                semCategorias.VerticalAlignment = VerticalAlignment.Center;
                semCategorias.HorizontalAlignment = HorizontalAlignment.Center;
                semCategorias.FontSize = 30;
                painelcategoria.Children.Add(semCategorias);
            }
            else
            {
                foreach (var item in App.categorias)
                {
                    StackPanel painel = new StackPanel();
                    TextBlock texto1 = new TextBlock();
                    TextBlock texto2 = new TextBlock();

                    painel.Width = 300;
                    painel.Height = 100;
                    painel.VerticalAlignment = VerticalAlignment.Top;
                    painel.Margin = new Thickness(0, 20, 0, 0);

                    texto1.Text = "Tipo: " + item.tipo;
                    texto1.FontSize = 25;
                    painel.Children.Add(texto1);

                    texto2.Text = "Nome: " + item.nome;
                    texto2.FontSize = 20;
                    painel.Children.Add(texto2);

                    painelcategoria.Children.Add(painel);
                }
            }

            scrollcategoria.Content = painelcategoria;
            categoriaFlipItem.Content = scrollcategoria;
            categoriasFlip.Items.Add(categoriaFlipItem);
        }
    }
}
