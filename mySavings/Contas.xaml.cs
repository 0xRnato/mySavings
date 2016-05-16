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
    public sealed partial class Contas : Page
    {
        public Contas()
        {
            this.InitializeComponent();
        }

        private void addConta_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NovaConta));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            contasGrid.Children.Clear();

            ScrollViewer scrollContas = new ScrollViewer();
            StackPanel painelContas = new StackPanel();
            scrollContas.Margin = new Thickness(0, 0, 0, 50);

            if (App.contas.Count == 0)
            {
                TextBlock semContas = new TextBlock();
                semContas.Text = "Sem contas.";
                semContas.VerticalAlignment = VerticalAlignment.Center;
                semContas.HorizontalAlignment = HorizontalAlignment.Center;
                semContas.FontSize = 30;
                painelContas.Children.Add(semContas);
            }
            else
            {
                foreach (var item in App.contas)
                {
                    StackPanel painel = new StackPanel();
                    TextBlock texto1 = new TextBlock();
                    TextBlock texto2 = new TextBlock();

                    painel.Width = 300;
                    painel.Height = 100;
                    painel.VerticalAlignment = VerticalAlignment.Top;
                    painel.Margin = new Thickness(0, 20, 0, 0);

                    texto1.Text = "Nome: " + item.nome;
                    texto1.FontSize = 25;
                    painel.Children.Add(texto1);

                    texto2.Text = "Saldo: R$ " + item.saldo;
                    texto2.FontSize = 20;
                    painel.Children.Add(texto2);

                    painelContas.Children.Add(painel);
                }
            }
            scrollContas.Content = painelContas;
            contasGrid.Children.Add(scrollContas);
        }
    }
}
