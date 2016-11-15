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
    public sealed partial class NovaTransacao : Page
    {
        public NovaTransacao()
        {
            this.InitializeComponent();
        }

        Transacao transacao;
        StackPanel painelValor;
        StackPanel painelDetalhes;
        StackPanel painelData;
        TextBlock headerValor;
        TextBlock headerDetalhes;
        TextBlock headerData;
        TextBox newValor;
        TextBox newDetalhes;
        DatePicker newData;
        InputScope scope;
        InputScopeName scopeName;
        Conta conta;
        Conta contaO;
        Conta contaD;
        ComboBox newContaD;
        ComboBox newContaO;
        ComboBox newConta;

        private void cancelTransasao_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void salvarTransacao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (newValor.Text == "")
                    throw new Exception("Você esqueceu de digitar o valor.");
                if ((transacao.categoria.tipo == "Despesa" && float.Parse(newValor.Text) > transacao.conta.saldo)||(transacao.categoria.tipo == "Transferência" && float.Parse(newValor.Text) > transacao.contaO.saldo))
                    throw new Exception("A conta selecionada não pussui saldo suficiente para realizar essa transação.");
                transacao.valor = float.Parse(newValor.Text);
                if (newDetalhes.Text == "")
                    throw new Exception("Você esqueceu de digitar os detalhes.");
                transacao.detalhes = newDetalhes.Text;
                transacao.data = newData.Date.Date;

                App.transacoes.Add(transacao);

                if (transacao.categoria.tipo == "Receita")
                {
                    for (int i = 0; i < App.contas.Count; i++)
                    {
                        if(App.contas[i].nome == transacao.conta.nome)
                        {
                            conta.nome = App.contas[i].nome;
                            conta.saldo = App.contas[i].saldo + transacao.valor;
                            App.contas.Insert(i, conta);
                            App.contas.RemoveAt(i + 1);
                        }
                    }
                }
                else if(transacao.categoria.tipo == "Despesa")
                {
                    for (int i = 0; i < App.contas.Count; i++)
                    {
                        if (App.contas[i].nome == transacao.conta.nome)
                        {
                            conta.nome = App.contas[i].nome;
                            conta.saldo = App.contas[i].saldo - transacao.valor;
                            App.contas.Insert(i, conta);
                            App.contas.RemoveAt(i + 1);
                        }
                    }
                }
                else if (transacao.categoria.tipo == "Transferência")
                {
                    for (int i = 0; i < App.contas.Count; i++)
                    {
                        if (App.contas[i].nome == transacao.contaO.nome)
                        {
                            contaO.nome = App.contas[i].nome;
                            contaO.saldo = App.contas[i].saldo - transacao.valor;
                            App.contas.Insert(i, contaO);
                            App.contas.RemoveAt(i + 1);
                        }
                        else if (App.contas[i].nome == transacao.contaD.nome)
                        {
                            contaD.nome = App.contas[i].nome;
                            contaD.saldo = App.contas[i].saldo + transacao.valor;
                            App.contas.Insert(i, contaD);
                            App.contas.RemoveAt(i + 1);
                        }
                    }
                }

                transacao = new Transacao();
                conta = new Conta();
                contaO = new Conta();
                contaD = new Conta();

                Frame.Navigate(typeof(MainPage));
            }
            catch (FormatException)
            {
                var messageDialog = new MessageDialog("O valor está em um formato incorreto.");
                await messageDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                var messageDiolago = new MessageDialog(ex.Message);
                await messageDiolago.ShowAsync();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxItem comboItem;
            newCategoria.Items.Clear();

            foreach (var item in App.categorias)
            {
                comboItem = new ComboBoxItem();
                comboItem.Content = item.nome;
                newCategoria.Items.Add(comboItem);
            }
        }

        private void newConta_DropDownClosed(object sender, object e)
        {
            if (transacao.categoria.tipo == "Receita" || transacao.categoria.tipo == "Despesa")
            {
                conta = new Conta();
                conta.nome = newConta.SelectionBoxItem.ToString();
                foreach (var item in App.contas)
                {
                    if (item.nome == conta.nome)
                        conta.saldo = item.saldo;
                }
                transacao.conta = conta;
            }
            else if (transacao.categoria.tipo == "Transferência")
            {
                contaO = new Conta();
                contaO.nome = newContaO.SelectionBoxItem.ToString();
                contaD = new Conta();
                contaD.nome = newContaD.SelectionBoxItem.ToString();

                foreach (var item in App.contas)
                {
                    if (item.nome == contaO.nome)
                        contaO.saldo = item.saldo;
                    if (item.nome == contaD.nome)
                        contaD.saldo = item.saldo;
                }
                transacao.contaO = contaO;
                transacao.contaD = contaD;
            }

            painelValor = new StackPanel();
            painelDetalhes = new StackPanel();
            painelData = new StackPanel();
            headerValor = new TextBlock();
            headerDetalhes = new TextBlock();
            headerData = new TextBlock();
            newValor = new TextBox();
            newDetalhes = new TextBox();
            newData = new DatePicker();
            scope = new InputScope();
            scopeName = new InputScopeName();

            painelValor.Orientation = Orientation.Horizontal;
            painelDetalhes.Orientation = Orientation.Horizontal;
            painelData.Orientation = Orientation.Horizontal;
            headerValor.Text = "Valor: ";
            headerValor.FontSize = 20;
            headerDetalhes.Text = "Detalhes: ";
            headerDetalhes.FontSize = 20;
            headerData.Text = "Data: ";
            headerData.FontSize = 20;
            newValor.Margin = new Thickness(61, 0, 0, 0);
            scopeName.NameValue = InputScopeNameValue.Number;
            scope.Names.Add(scopeName);
            newValor.InputScope = scope;
            newDetalhes.Margin = new Thickness(29, 0, 0, 0);
            newData.Margin = new Thickness(65, 0, 0, 0);

            painelValor.Children.Add(headerValor);
            painelValor.Children.Add(newValor);
            painel.Children.Add(painelValor);

            painelDetalhes.Children.Add(headerDetalhes);
            painelDetalhes.Children.Add(newDetalhes);
            painel.Children.Add(painelDetalhes);

            painelData.Children.Add(headerData);
            painelData.Children.Add(newData);
            painel.Children.Add(painelData);
        }

        private void newCategoria_DropDownClosed(object sender, object e)
        {
            Categoria categoria = new Categoria();

            categoria.nome = newCategoria.SelectionBoxItem.ToString();
            foreach (var item in App.categorias)
            {
                if (item.nome == categoria.nome)
                    categoria.tipo = item.tipo;
            }
            transacao.categoria = categoria;

            if (transacao.categoria.tipo == "Receita" || transacao.categoria.tipo == "Despesa")
            {
                StackPanel painelConta = new StackPanel();
                TextBlock headerConta = new TextBlock();
                newConta = new ComboBox();
                ComboBoxItem comboItemConta;

                painelConta.Orientation = Orientation.Horizontal;
                headerConta.Text = "Conta: ";
                headerConta.FontSize = 20;
                newConta.Margin = new Thickness(21, 0, 0, 0);
                newConta.Width = 300;
                newConta.DropDownClosed += newConta_DropDownClosed;

                foreach (var item in App.contas)
                {
                    comboItemConta = new ComboBoxItem();
                    comboItemConta.Content = item.nome;
                    newConta.Items.Add(comboItemConta);
                }
                painelConta.Children.Add(headerConta);
                painelConta.Children.Add(newConta);
                painel.Children.Add(painelConta);
            }
            else if (transacao.categoria.tipo == "Transferência")
            {
                StackPanel painelContaO = new StackPanel();
                TextBlock headerContaO = new TextBlock();
                newContaO = new ComboBox();
                StackPanel painelContaD = new StackPanel();
                TextBlock headerContaD = new TextBlock();
                newContaD = new ComboBox();
                ComboBoxItem comboItemContaO;
                ComboBoxItem comboItemContaD;

                painelContaD.Orientation = Orientation.Horizontal;
                painelContaO.Orientation = Orientation.Horizontal;
                headerContaO.Text = "Conta origem: ";
                headerContaO.FontSize = 20;
                newContaO.Margin = new Thickness(21, 0, 0, 0);
                newContaO.Width = 300;
                headerContaD.Text = "Conta destino: ";
                headerContaD.FontSize = 20;
                newContaD.Margin = new Thickness(21, 0, 0, 0);
                newContaD.Width = 300;
                newContaD.DropDownClosed += newConta_DropDownClosed;

                foreach (var item in App.contas)
                {
                    comboItemContaO = new ComboBoxItem();
                    comboItemContaO.Content = item.nome;
                    newContaO.Items.Add(comboItemContaO);
                }
                foreach (var item in App.contas)
                {
                    comboItemContaD = new ComboBoxItem();
                    comboItemContaD.Content = item.nome;
                    newContaD.Items.Add(comboItemContaD);
                }

                painelContaO.Children.Add(headerContaO);
                painelContaO.Children.Add(newContaO);
                painel.Children.Add(painelContaO);

                painelContaD.Children.Add(headerContaD);
                painelContaD.Children.Add(newContaD);
                painel.Children.Add(painelContaD);
            }
        }
    }
}
