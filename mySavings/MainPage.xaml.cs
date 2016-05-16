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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace mySavings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public MainPage(Frame f)
        {
            this.InitializeComponent();
            this.navPane.Content = f;
        }

        private void Click_OpenPane(object sender, RoutedEventArgs e)
        {
            navPane.IsPaneOpen = !navPane.IsPaneOpen;
        }

        private void menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (menu_Contas.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Contas));
                //UpdateContas();
            }
            else if (menu_Transacoes.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Transacoes));
                //UpdateTransacoes();
            }
            else if (menu_Categorias.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Categorias));
                //UpdateCategorias();
            }
            else if (menu_Planejamentos.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Planejamentos));
                //UpdatePlanejamentos();
            }
            else if (menu_Tendencias.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Tendencias));
                //UpdateTendencias();
            }
            else if (menu_Moedas.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Moedas));
                //UpdateMoedas();
            }
            else if (menu_Calculadora.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Calculadora));
                //UpdateCalculadora();
            }
            else if (menu_Ajuda.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Ajuda));
                //UpdateAjuda();
            }
            else if (menu_Configurações.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Configuracoes));
                //UpdateConfiguracoes();
            }
            else if (menu_Sobre.IsSelected)
            {
                (navPane.Content as Frame).Navigate(typeof(Sobre));
                //UpdateSobre();
            }
            navPane.IsPaneOpen = false;
        }
    }
}
