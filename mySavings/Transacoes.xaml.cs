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
    public sealed partial class Transacoes : Page
    {
        public Transacoes()
        {
            this.InitializeComponent();
        }

        private async void addTransacao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.contas.Count == 0)
                    throw new Exception("Adicione uma conta antes de fazer uma transação.");
                if (App.categorias.Count == 0)
                    throw new Exception("Adicione uma categoria antes de fazer uma transação.");
                Frame.Navigate(typeof(NovaTransacao));
            }
            catch (Exception ex)
            {
                MessageDialog message = new MessageDialog(ex.Message);
                await message.ShowAsync();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
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
    }
}
