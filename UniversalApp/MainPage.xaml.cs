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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UniversalApp
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

        //----------------------UPDATES--------------------

        public void UpdateContas()
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

        public void UpdateTransacoes()
        {
            int contPassado = 0, contRetrasado = 0, contAtual = 0, contSeguinte = 0;
            mesesFlip.Items.Clear();

            //MES RETRASADO
            FlipViewItem mesRetrasado = new FlipViewItem();
            ScrollViewer scrollMesRetrasado = new ScrollViewer();
            StackPanel painelMesRetrasado = new StackPanel();
            TextBlock headerMesRetrasado = new TextBlock();
            scrollMesRetrasado.Margin = new Thickness(0, 0, 0, 50);
            headerMesRetrasado.FontSize = 20;
            headerMesRetrasado.VerticalAlignment = VerticalAlignment.Top;
            headerMesRetrasado.HorizontalAlignment = HorizontalAlignment.Center;
            headerMesRetrasado.Text = DateTime.Today.AddMonths(-2).ToString("MMMM");
            painelMesRetrasado.Children.Add(headerMesRetrasado);

            foreach (var item in App.transacoes)
            {
                if (item.data.Month == DateTime.Today.AddMonths(-2).Month)
                    contRetrasado++;
                else if (item.data.Month == DateTime.Today.AddMonths(-1).Month)
                    contPassado++;
                else if (item.data.Month == DateTime.Today.Month)
                    contAtual++;
                else if (item.data.Month == DateTime.Today.AddMonths(1).Month)
                    contSeguinte++;
            }

            if (contRetrasado == 0)
            {
                TextBlock semTransacoes = new TextBlock();
                semTransacoes.Text = "Sem transações.";
                semTransacoes.VerticalAlignment = VerticalAlignment.Center;
                semTransacoes.HorizontalAlignment = HorizontalAlignment.Center;
                semTransacoes.FontSize = 30;
                painelMesRetrasado.Children.Add(semTransacoes);
            }
            else
            {
                //Transações
                foreach (var item in App.transacoes)
                {
                    if (item.data.Month == DateTime.Today.AddMonths(-2).Month)
                    {
                        StackPanel painel = new StackPanel();
                        TextBlock texto1 = new TextBlock();
                        TextBlock texto2 = new TextBlock();
                        TextBlock texto3 = new TextBlock();
                        TextBlock texto4 = new TextBlock();

                        painel.Width = 300;
                        painel.Height = 100;
                        painel.VerticalAlignment = VerticalAlignment.Top;
                        painel.Margin = new Thickness(0, 20, 0, 0);

                        texto1.Text = "Categoria: " + item.categoria.nome;
                        texto1.FontSize = 25;
                        painel.Children.Add(texto1);

                        if (item.categoria.tipo == "Receita")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
                        else if (item.categoria.tipo == "Despesa")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                        else if (item.categoria.tipo == "Transferência")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);

                        texto2.Text = "Valor: R$ " + item.valor;
                        texto2.FontSize = 20;
                        painel.Children.Add(texto2);

                        texto3.Text = "Detalhes: " + item.detalhes; ;
                        texto3.FontSize = 17;
                        painel.Children.Add(texto3);

                        texto4.Text = "Data: " + item.data.ToString("dd/MMMM/yyyy"); ;
                        texto4.FontSize = 10;
                        painel.Children.Add(texto4);

                        painelMesRetrasado.Children.Add(painel);
                    }
                }
            }

            scrollMesRetrasado.Content = painelMesRetrasado;
            mesRetrasado.Content = scrollMesRetrasado;
            mesesFlip.Items.Add(mesRetrasado);

            //MES PASSADO
            FlipViewItem mesPassado = new FlipViewItem();
            ScrollViewer scrollMesPassado = new ScrollViewer();
            StackPanel painelMesPassado = new StackPanel();
            TextBlock headerMesPassado = new TextBlock();
            scrollMesPassado.Margin = new Thickness(0, 0, 0, 50);
            headerMesPassado.FontSize = 20;
            headerMesPassado.VerticalAlignment = VerticalAlignment.Top;
            headerMesPassado.HorizontalAlignment = HorizontalAlignment.Center;
            headerMesPassado.Text = DateTime.Today.AddMonths(-1).ToString("MMMM");
            painelMesPassado.Children.Add(headerMesPassado);

            if (contPassado == 0)
            {
                TextBlock semTransacoes = new TextBlock();
                semTransacoes.Text = "Sem transações.";
                semTransacoes.VerticalAlignment = VerticalAlignment.Center;
                semTransacoes.HorizontalAlignment = HorizontalAlignment.Center;
                semTransacoes.FontSize = 30;
                painelMesPassado.Children.Add(semTransacoes);
            }
            else
            {
                //Transações
                foreach (var item in App.transacoes)
                {
                    if (item.data.Month == DateTime.Today.AddMonths(-1).Month)
                    {
                        StackPanel painel = new StackPanel();
                        TextBlock texto1 = new TextBlock();
                        TextBlock texto2 = new TextBlock();
                        TextBlock texto3 = new TextBlock();
                        TextBlock texto4 = new TextBlock();

                        painel.Width = 300;
                        painel.Height = 100;
                        painel.VerticalAlignment = VerticalAlignment.Top;
                        painel.Margin = new Thickness(0, 20, 0, 0);

                        texto1.Text = "Categoria: " + item.categoria.nome;
                        texto1.FontSize = 25;
                        painel.Children.Add(texto1);

                        if (item.categoria.tipo == "Receita")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
                        else if (item.categoria.tipo == "Despesa")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                        else if (item.categoria.tipo == "Transferência")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);

                        texto2.Text = "Valor: R$ " + item.valor;
                        texto2.FontSize = 20;
                        painel.Children.Add(texto2);

                        texto3.Text = "Detalhes: " + item.detalhes; ;
                        texto3.FontSize = 17;
                        painel.Children.Add(texto3);

                        texto4.Text = "Data: " + item.data.ToString("dd/MMMM/yyyy"); ;
                        texto4.FontSize = 10;
                        painel.Children.Add(texto4);

                        painelMesPassado.Children.Add(painel);
                    }
                }
            }

            scrollMesPassado.Content = painelMesPassado;
            mesPassado.Content = scrollMesPassado;
            mesesFlip.Items.Add(mesPassado);

            //MES ATUAL
            FlipViewItem mesAtual = new FlipViewItem();
            ScrollViewer scrollMesAtual = new ScrollViewer();
            StackPanel painelMesAtual = new StackPanel();
            TextBlock headerMesAtual = new TextBlock();
            scrollMesAtual.Margin = new Thickness(0, 0, 0, 50);
            headerMesAtual.FontSize = 20;
            headerMesAtual.VerticalAlignment = VerticalAlignment.Top;
            headerMesAtual.HorizontalAlignment = HorizontalAlignment.Center;
            headerMesAtual.Text = DateTime.Today.ToString("MMMM");
            painelMesAtual.Children.Add(headerMesAtual);

            if (contAtual == 0)
            {
                TextBlock semTransacoes = new TextBlock();
                semTransacoes.Text = "Sem transações.";
                semTransacoes.VerticalAlignment = VerticalAlignment.Center;
                semTransacoes.HorizontalAlignment = HorizontalAlignment.Center;
                semTransacoes.FontSize = 30;
                painelMesAtual.Children.Add(semTransacoes);
            }
            else
            {
                //Transações
                foreach (var item in App.transacoes)
                {
                    if (item.data.Month == DateTime.Today.Month)
                    {
                        StackPanel painel = new StackPanel();
                        TextBlock texto1 = new TextBlock();
                        TextBlock texto2 = new TextBlock();
                        TextBlock texto3 = new TextBlock();
                        TextBlock texto4 = new TextBlock();

                        painel.Width = 300;
                        painel.Height = 100;
                        painel.VerticalAlignment = VerticalAlignment.Top;
                        painel.Margin = new Thickness(0, 20, 0, 0);

                        texto1.Text = "Categoria: " + item.categoria.nome;
                        texto1.FontSize = 25;
                        painel.Children.Add(texto1);

                        if (item.categoria.tipo == "Receita")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
                        else if (item.categoria.tipo == "Despesa")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                        else if (item.categoria.tipo == "Transferência")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);

                        texto2.Text = "Valor: R$ " + item.valor;
                        texto2.FontSize = 20;
                        painel.Children.Add(texto2);

                        texto3.Text = "Detalhes: " + item.detalhes; ;
                        texto3.FontSize = 17;
                        painel.Children.Add(texto3);

                        texto4.Text = "Data: " + item.data.ToString("dd/MMMM/yyyy"); ;
                        texto4.FontSize = 10;
                        painel.Children.Add(texto4);

                        painelMesAtual.Children.Add(painel);
                    }
                }
            }
            scrollMesAtual.Content = painelMesAtual;
            mesAtual.Content = scrollMesAtual;
            mesesFlip.Items.Add(mesAtual);


            //MES SEGUINTE
            FlipViewItem mesSeguinte = new FlipViewItem();
            ScrollViewer scrollMesSeguinte = new ScrollViewer();
            StackPanel painelMesSeguinte = new StackPanel();
            TextBlock headerMesSeguinte = new TextBlock();
            scrollMesSeguinte.Margin = new Thickness(0, 0, 0, 50);
            headerMesSeguinte.FontSize = 20;
            headerMesSeguinte.VerticalAlignment = VerticalAlignment.Top;
            headerMesSeguinte.HorizontalAlignment = HorizontalAlignment.Center;
            headerMesSeguinte.Text = DateTime.Today.AddMonths(1).ToString("MMMM");
            painelMesSeguinte.Children.Add(headerMesSeguinte);

            if (contSeguinte == 0)
            {
                TextBlock semTransacoes = new TextBlock();
                semTransacoes.Text = "Sem transações.";
                semTransacoes.VerticalAlignment = VerticalAlignment.Center;
                semTransacoes.HorizontalAlignment = HorizontalAlignment.Center;
                semTransacoes.FontSize = 30;
                painelMesSeguinte.Children.Add(semTransacoes);
            }
            else
            {
                //Transações
                foreach (var item in App.transacoes)
                {
                    if (item.data.Month == DateTime.Today.AddMonths(1).Month)
                    {
                        StackPanel painel = new StackPanel();
                        TextBlock texto1 = new TextBlock();
                        TextBlock texto2 = new TextBlock();
                        TextBlock texto3 = new TextBlock();
                        TextBlock texto4 = new TextBlock();

                        painel.Width = 300;
                        painel.Height = 100;
                        painel.VerticalAlignment = VerticalAlignment.Top;
                        painel.Margin = new Thickness(0, 20, 0, 0);

                        texto1.Text = "Categoria: " + item.categoria.nome;
                        texto1.FontSize = 25;
                        painel.Children.Add(texto1);

                        if (item.categoria.tipo == "Receita")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
                        else if (item.categoria.tipo == "Despesa")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                        else if (item.categoria.tipo == "Transferência")
                            texto2.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);

                        texto2.Text = "Valor: R$ " + item.valor;
                        texto2.FontSize = 20;
                        painel.Children.Add(texto2);

                        texto3.Text = "Detalhes: " + item.detalhes; ;
                        texto3.FontSize = 17;
                        painel.Children.Add(texto3);

                        texto4.Text = "Data: " + item.data.ToString("dd/MMMM/yyyy"); ;
                        texto4.FontSize = 10;
                        painel.Children.Add(texto4);

                        painelMesSeguinte.Children.Add(painel);
                    }
                }
            }

            scrollMesSeguinte.Content = painelMesSeguinte;
            mesSeguinte.Content = scrollMesSeguinte;
            mesesFlip.Items.Add(mesSeguinte);

            mesesFlip.SelectedIndex = 1;
            mesesFlip.SelectedIndex = 2;
        }

        private void UpdateCategorias()
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

        private void UpdatePlanejamentos()
        {

        }

        private void UpdateTendencias()
        {

        }

        private void UpdateMoedas()
        {

        }

        private void UpdateCalculadora()
        {

        }

        private void UpdateAjuda()
        {

        }

        private void UpdateConfiguracoes()
        {

        }

        private void UpdateSobre()
        {

        }


        //---------------------MENU---------------------------

        private void Click_OpenPane(object sender, RoutedEventArgs e)
        {
            navPane.IsPaneOpen = !navPane.IsPaneOpen;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Contas
            if (App.paginaAtual == 0)
            {
                menu.SelectedIndex = 0;
                page_Contas.Visibility = Visibility.Visible;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateContas();
            }
            //Transações
            else if (App.paginaAtual == 1)
            {
                menu.SelectedIndex = 1;
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Visible;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateTransacoes();
            }
            //Categorias
            else if (App.paginaAtual == 2)
            {
                menu.SelectedIndex = 2;
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Visible;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateCategorias();
            }
            //Planejamentos
            else if (App.paginaAtual == 3)
            {
                menu.SelectedIndex = 3;
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Visible;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdatePlanejamentos();
            }
            //Tendências
            else if (App.paginaAtual == 4)
            {
                menu.SelectedIndex = 4;
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Visible;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateTendencias();
            }
            //Moedas
            else if (App.paginaAtual == 5)
            {
                menu.SelectedIndex = 5;
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Visible;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateMoedas();
            }
            //Calculadora
            else if (App.paginaAtual == 6)
            {
                menu.SelectedIndex = 6;
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Visible;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateCalculadora();
            }
            //Ajuda
            else if (App.paginaAtual == 7)
            {
                menu.SelectedIndex = 7;
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Visible;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateAjuda();
            }
            //Configurações
            else if (App.paginaAtual == 8)
            {
                menu.SelectedIndex = 8;
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Visible;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateConfiguracoes();
            }
            //Sobre
            else if (App.paginaAtual == 9)
            {
                menu.SelectedIndex = 9;
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Visible;
                UpdateSobre();
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Contas
            if (menu_Contas.IsSelected)
            {
                page_Contas.Visibility = Visibility.Visible;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateContas();
            }
            //Transações
            else if (menu_Transacoes.IsSelected)
            {
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Visible;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateTransacoes();
            }
            //Categorias
            else if (menu_Categorias.IsSelected)
            {
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Visible;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateCategorias();
            }
            //Planejamentos
            else if (menu_Planejamentos.IsSelected)
            {
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Visible;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdatePlanejamentos();
            }
            //Tendências
            else if (menu_Tendencias.IsSelected)
            {
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Visible;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateTendencias();
            }
            //Moedas
            else if (menu_Moedas.IsSelected)
            {
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Visible;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateMoedas();
            }
            //Calculadora
            else if (menu_Calculadora.IsSelected)
            {
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Visible;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateCalculadora();
            }
            //Ajuda
            else if (menu_Ajuda.IsSelected)
            {
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Visible;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateAjuda();
            }
            //Configurações
            else if (menu_Configurações.IsSelected)
            {
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Visible;
                page_Sobre.Visibility = Visibility.Collapsed;
                UpdateConfiguracoes();
            }
            //Sobre
            else if (menu_Sobre.IsSelected)
            {
                page_Contas.Visibility = Visibility.Collapsed;
                page_Transacoes.Visibility = Visibility.Collapsed;
                page_Categorias.Visibility = Visibility.Collapsed;
                page_Planejamentos.Visibility = Visibility.Collapsed;
                page_Tendencias.Visibility = Visibility.Collapsed;
                page_Moedas.Visibility = Visibility.Collapsed;
                page_Calculadora.Visibility = Visibility.Collapsed;
                page_Ajuda.Visibility = Visibility.Collapsed;
                page_Configuracoes.Visibility = Visibility.Collapsed;
                page_Sobre.Visibility = Visibility.Visible;
                UpdateSobre();
            }
            navPane.IsPaneOpen = false;
        }


        //-----------------BOTÕES DE ADIÇÃO-------------------------
        private async void addTransacao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.contas.Count == 0)
                    throw new Exception("Adicione uma conta antes de fazer uma transação.");
                if (App.categorias.Count == 0)
                    throw new Exception("Adicione uma categoria antes de fazer uma transação.");                
                Frame.Navigate(typeof(NovaTransacao));
                App.paginaAtual = 1;
            }
            catch (Exception ex)
            {
                MessageDialog message = new MessageDialog(ex.Message);
                await message.ShowAsync();
            }
        }

        private void addConta_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NovaConta));
            App.paginaAtual = 0;
        }

        private void addCategoria_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NovaCategoria));
            App.paginaAtual = 2;
        }
    }
}
