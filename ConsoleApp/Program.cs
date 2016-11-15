using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySavings_ConsoleApp
{
    struct Conta
    {
        public float bruto;
        public float corrente;
        public float poupanca;
        public float credito;
        public float carteira;
        public float receita;
        public float despesa;
        public float liquido;
    }

    struct Transacao
    {
        public string tipo;
        public float valor;
        public string contaO;
        public string contaD;
        public DateTime data;
    }

    struct Planejamento
    {
        public string nome;
        public string dataInicio;
        public float quantiaAtual;
        public float meta;
        public string dataPrevista;
    }

    class Program
    {
        //Variáveis globais
        public static Conta conta;
        public static bool transacoesActive = true;
        public static List<Transacao> historico = new List<Transacao>();
        public static string ultimaTrans = "";

        //Carregar os dados
        static void Load()
        {
            //Dados da conta
            if (System.IO.File.Exists(@"dataC.rnt"))
            {
                string dadosConta1 = System.IO.File.ReadAllText(@"dataC.rnt");
                dadosConta1 = dadosConta1.Replace("\r", "").Replace("=", " ");
                string[] dadosConta2 = dadosConta1.Split();
                conta.bruto = float.Parse(dadosConta2[1]);
                conta.corrente = float.Parse(dadosConta2[3]);
                conta.poupanca = float.Parse(dadosConta2[5]);
                conta.credito = float.Parse(dadosConta2[7]);
                conta.carteira = float.Parse(dadosConta2[9]);
                conta.receita = float.Parse(dadosConta2[11]);
                conta.despesa = float.Parse(dadosConta2[13]);
                conta.liquido = float.Parse(dadosConta2[15]);
            }
            else
            {
                conta.bruto = 0;
                conta.corrente = 0;
                conta.poupanca = 0;
                conta.credito = 0;
                conta.receita = 0;
                conta.despesa = 0;
                conta.liquido = 0;
            }

            //Dados do historico
            if (System.IO.File.Exists(@"dataH.rnt"))
            {
                string data;
                string dadosHistorico1 = System.IO.File.ReadAllText(@"dataH.rnt");
                dadosHistorico1 = dadosHistorico1.Replace("\r", "").Replace("=", " ").Replace(",", "\n");
                string[] dadosHistorico2 = dadosHistorico1.Split();
                Transacao[] dadosHistorico3 = new Transacao[int.Parse(dadosHistorico2[1])];
                int count = 2;

                for (int i = 0; i < dadosHistorico3.Length; i++)
                {
                    dadosHistorico3[i].tipo = dadosHistorico2[count + 1];
                    dadosHistorico3[i].valor = float.Parse(dadosHistorico2[count + 2]);
                    dadosHistorico3[i].contaO = dadosHistorico2[count + 3];
                    dadosHistorico3[i].contaD = dadosHistorico2[count + 4];
                    data = dadosHistorico2[count + 5] + " " + dadosHistorico2[count + 6] + " " + dadosHistorico2[count + 7];
                    dadosHistorico3[i].data = DateTime.Parse(data);
                    count += 8;
                }
                historico = dadosHistorico3.ToList();
            }

        }

        //Salvar os dados
        static void Save()
        {
            int count = 0;
            string dadosHistorico = "Historico.Count=" + historico.Count + "\r\n";
            string dadosConta = "bruto=" + conta.bruto + "\r\ncorrente=" + conta.corrente + "\r\npoupanca=" + conta.poupanca + "\r\ncredito=" + conta.credito + "\r\ncarteira=" + conta.carteira + "\r\nreceita=" + conta.receita + "\r\ndespesa=" + conta.despesa + "\r\nliquido=" + conta.liquido;

            foreach (var item in historico)
            {
                count++;
                dadosHistorico += "Transacao" + count + "=" + item.tipo + "," + item.valor + "," + item.contaO + "," + item.contaD + "," + item.data + "\r\n";
            }

            System.IO.File.WriteAllText(@"dataC.rnt", dadosConta);
            System.IO.File.WriteAllText(@"dataH.rnt", dadosHistorico);
        }

        //Menu de navegação
        static void Menu()
        {
            int opcao = 0;
            string telaAtual = "TelaInicial";
            ConsoleKeyInfo input;

            do
            {
                if (opcao == 0)
                {
                    telaAtual = "TelaInicial";
                }
                else if (opcao == 1)
                {
                    telaAtual = "Transacoes";
                    transacoesActive = true;
                }
                else if (opcao == 2)
                {
                    telaAtual = "Grafico";
                }
                else if (opcao == 3)
                {
                    telaAtual = "Planejamentos";
                }

                do
                {
                    if (telaAtual == "TelaInicial")
                        TelaInicial();
                    else if (telaAtual == "Transacoes")
                        Transacoes();
                    else if (telaAtual == "Grafico")
                        Grafico();
                    else if (telaAtual == "Planejamentos")
                        Planejamentos();

                    if (opcao == 0)
                    {
                        Console.WriteLine("\n\n\n\nUse as setas para navegar no menu: ");
                        Console.WriteLine("» Tela inicial «");
                        Console.WriteLine("  Transações");
                        Console.WriteLine("  Gráficos de movimentações");
                        Console.WriteLine("  Planejamentos");
                        Console.WriteLine("  Salvar e sair");
                    }
                    else if (opcao == 1)
                    {
                        Console.WriteLine("\n\n\n\nUse as setas para navegar no menu: ");
                        Console.WriteLine("  Tela inicial");
                        Console.WriteLine("» Transações «");
                        Console.WriteLine("  Gráficos de movimentações");
                        Console.WriteLine("  Planejamentos");
                        Console.WriteLine("  Salvar e sair");
                    }
                    else if (opcao == 2)
                    {
                        Console.WriteLine("\n\n\n\nUse as setas para navegar no menu: ");
                        Console.WriteLine("  Tela inicial");
                        Console.WriteLine("  Transações");
                        Console.WriteLine("» Gráficos de movimentações «");
                        Console.WriteLine("  Planejamentos");
                        Console.WriteLine("  Salvar e sair");
                    }
                    else if (opcao == 3)
                    {
                        Console.WriteLine("\n\n\n\nUse as setas para navegar no menu: ");
                        Console.WriteLine("  Tela inicial");
                        Console.WriteLine("  Transações");
                        Console.WriteLine("  Gráficos de movimentações");
                        Console.WriteLine("» Planejamentos «");
                        Console.WriteLine("  Salvar e sair");
                    }
                    else if (opcao == 4)
                    {
                        Console.WriteLine("\n\n\n\nUse as setas para navegar no menu: ");
                        Console.WriteLine("  Tela inicial");
                        Console.WriteLine("  Transações");
                        Console.WriteLine("  Gráficos de movimentações");
                        Console.WriteLine("  Planejamentos");
                        Console.WriteLine("» Salvar e sair «");
                    }
                    input = Console.ReadKey();

                    if (input.Key == ConsoleKey.UpArrow)
                        opcao--;
                    else if (input.Key == ConsoleKey.DownArrow)
                        opcao++;

                    if (opcao < 0)
                        opcao = 0;
                    else if (opcao > 4)
                        opcao = 4;

                } while (input.Key != ConsoleKey.Enter);
            } while (opcao != 4);
        }

        //Tela Inicial
        static void TelaInicial()
        {
            conta.bruto = conta.corrente + conta.poupanca + conta.carteira + conta.credito;
            conta.receita = 0;
            conta.despesa = 0;
            foreach (var item in historico)
            {
                if (item.data.Month == DateTime.Now.Month)
                {
                    if (item.tipo == "Receita")
                        conta.receita += item.valor;
                    else if (item.tipo == "Despesa")
                        conta.despesa += item.valor;
                }
            }
            conta.liquido = conta.receita - conta.despesa;

            Console.Clear();
            Console.WriteLine("Olá sr.Renato\n\n");
            Console.WriteLine("           Tela Inicial");
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("Saldo Bruto: " + conta.bruto);
            Console.WriteLine("   Conta corrente: " + conta.corrente);
            Console.WriteLine("   Conta poupança: " + conta.poupanca);
            Console.WriteLine("   Carteira: " + conta.carteira);
            Console.WriteLine("   Crédito: " + conta.credito);
            Console.WriteLine("Receita mensal: " + conta.receita);
            Console.WriteLine("Despesa mensal: " + conta.despesa);
            Console.WriteLine("Saldo líquido: " + conta.liquido);
        }

        //Menu de Transações
        static void Transacoes()
        {
            int opcao = 0;
            int opcaoTrans = 0;
            ConsoleKeyInfo inputTrans;
            ConsoleKeyInfo input;
            Transacao transacao;
            transacao.tipo = "";
            transacao.contaD = "";
            transacao.contaO = "";

            if (System.IO.File.Exists(@"dataH.rnt"))
            {
                ultimaTrans = System.IO.File.ReadAllText(@"dataH.rnt");
                if (ultimaTrans.Length > 19)
                {
                    ultimaTrans = "Tipo:      " + historico.Last().tipo + "\nValor:  R$ " + historico.Last().valor + "\nOrigem:    " + historico.Last().contaO + "\nDestino:   " + historico.Last().contaD + "\nData:   " + historico.Last().data;
                }
                else
                    ultimaTrans = "Sem regístros rescentes...";
            }


            Console.Clear();
            Console.WriteLine("Olá sr.Renato\n\n");
            Console.WriteLine("          Menu de Transações");
            Console.WriteLine("─────────────────────────────────────");
            if (ultimaTrans != "")
            {
                Console.WriteLine("Última transação cadastrada\n");
                Console.WriteLine(ultimaTrans);
            }
            else
                Console.WriteLine("Sem regístros rescentes...");

            while (transacoesActive)
            {
                //Mini menu
                do
                {
                    Console.Clear();
                    Console.WriteLine("Olá sr.Renato\n\n");
                    Console.WriteLine("          Menu de Transações");
                    Console.WriteLine("─────────────────────────────────────");
                    if (ultimaTrans != "")
                    {
                        Console.WriteLine("Última transação cadastrada\n");
                        Console.WriteLine(ultimaTrans);
                    }
                    else
                        Console.WriteLine("Sem regístros rescentes...");

                    if (opcao == 0)
                    {
                        Console.WriteLine("\nNova transação? ");
                        Console.WriteLine("» Sim «");
                        Console.WriteLine("  Histórico");
                        Console.WriteLine("  Voltar");
                    }
                    else if (opcao == 1)
                    {
                        Console.WriteLine("\nNova transação? ");
                        Console.WriteLine("  Sim");
                        Console.WriteLine("» Histórico «");
                        Console.WriteLine("  Voltar");
                    }
                    else if (opcao == 2)
                    {
                        Console.WriteLine("\nNova transação? ");
                        Console.WriteLine("  Sim");
                        Console.WriteLine("  Histórico");
                        Console.WriteLine("» Voltar «");
                    }

                    input = Console.ReadKey();

                    if (input.Key == ConsoleKey.UpArrow)
                        opcao--;
                    else if (input.Key == ConsoleKey.DownArrow)
                        opcao++;

                    if (opcao < 0)
                        opcao = 0;
                    else if (opcao > 2)
                        opcao = 2;

                } while (input.Key != ConsoleKey.Enter);

                //SIM
                if (opcao == 0)
                {
                    //TIPO
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Olá sr.Renato\n\n");
                        Console.WriteLine("          Menu de Transações");
                        Console.WriteLine("─────────────────────────────────────");
                        Console.WriteLine("Cadastro de Transação\n");

                        if (opcaoTrans == 0)
                        {
                            Console.WriteLine("Tipo:    » Receita «");
                        }
                        else if (opcaoTrans == 1)
                        {
                            Console.WriteLine("Tipo:    » Despesa «");
                        }
                        else if (opcaoTrans == 2)
                        {
                            Console.WriteLine("Tipo:    » Transferência «");
                        }

                        inputTrans = Console.ReadKey();

                        if (inputTrans.Key == ConsoleKey.UpArrow)
                            opcaoTrans--;
                        else if (inputTrans.Key == ConsoleKey.DownArrow)
                            opcaoTrans++;

                        if (opcaoTrans < 0)
                            opcaoTrans = 2;
                        else if (opcaoTrans > 2)
                            opcaoTrans = 0;

                    } while (inputTrans.Key != ConsoleKey.Enter);

                    if (opcaoTrans == 0)
                        transacao.tipo = "Receita";
                    else if (opcaoTrans == 1)
                        transacao.tipo = "Despesa";
                    else if (opcaoTrans == 2)
                        transacao.tipo = "Transferência";

                    //VALOR
                    Console.Clear();
                    Console.WriteLine("Olá sr.Renato\n\n");
                    Console.WriteLine("          Menu de Transações");
                    Console.WriteLine("─────────────────────────────────────");
                    Console.WriteLine("Cadastro de Transação\n");
                    Console.WriteLine("Tipo:      " + transacao.tipo);
                    Console.Write("Valor:  R$ ");
                    transacao.valor = float.Parse(Console.ReadLine());

                    opcaoTrans = 0;
                    //CONTAS
                    //RECEITA
                    if (transacao.tipo == "Receita")
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Olá sr.Renato\n\n");
                            Console.WriteLine("          Menu de Transações");
                            Console.WriteLine("─────────────────────────────────────");
                            Console.WriteLine("Cadastro de Transação\n");
                            Console.WriteLine("Tipo:      " + transacao.tipo);
                            Console.WriteLine("Valor:  R$ " + transacao.valor);

                            if (opcaoTrans == 0)
                            {
                                Console.WriteLine("Destino: » Corrente «");
                            }

                            else if (opcaoTrans == 1)
                            {
                                Console.WriteLine("Destino: » Poupança «");
                            }

                            else if (opcaoTrans == 2)
                            {
                                Console.WriteLine("Destino: » Carteira «");
                            }

                            inputTrans = Console.ReadKey();

                            if (inputTrans.Key == ConsoleKey.UpArrow)
                                opcaoTrans--;
                            else if (inputTrans.Key == ConsoleKey.DownArrow)
                                opcaoTrans++;

                            if (opcaoTrans < 0)
                                opcaoTrans = 2;
                            else if (opcaoTrans > 2)
                                opcaoTrans = 0;

                        } while (inputTrans.Key != ConsoleKey.Enter);

                        if (opcaoTrans == 0)
                            transacao.contaD = "Corrente";
                        else if (opcaoTrans == 1)
                            transacao.contaD = "Poupança";
                        else if (opcaoTrans == 2)
                            transacao.contaD = "Carteira";

                        Console.Clear();
                        Console.WriteLine("Olá sr.Renato\n\n");
                        Console.WriteLine("          Menu de Transações");
                        Console.WriteLine("─────────────────────────────────────");
                        Console.WriteLine("Cadastro de Transação\n");
                        Console.WriteLine("Tipo:      " + transacao.tipo);
                        Console.WriteLine("Valor:  R$ " + transacao.valor);
                        Console.WriteLine("Destino:   " + transacao.contaD);

                        transacao.data = DateTime.Now;
                        historico.Add(transacao);

                        //Alteração dos dados
                        if (transacao.contaD == "Corrente")
                            conta.corrente += transacao.valor;
                        else if (transacao.contaD == "Poupança")
                            conta.poupanca += transacao.valor;
                        else if (transacao.contaD == "Carteira")
                            conta.carteira += transacao.valor;
                    }
                    //DESPESA
                    else if (transacao.tipo == "Despesa")
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Olá sr.Renato\n\n");
                            Console.WriteLine("          Menu de Transações");
                            Console.WriteLine("─────────────────────────────────────");
                            Console.WriteLine("Cadastro de Transação\n");
                            Console.WriteLine("Tipo:      " + transacao.tipo);
                            Console.WriteLine("Valor:  R$ " + transacao.valor);

                            if (opcaoTrans == 0)
                            {
                                Console.WriteLine("Origem: » Corrente «");
                            }

                            else if (opcaoTrans == 1)
                            {
                                Console.WriteLine("Origem: » Poupança «");
                            }

                            else if (opcaoTrans == 2)
                            {
                                Console.WriteLine("Origem: » Carteira «");
                            }
                            else if (opcaoTrans == 3)
                            {
                                Console.WriteLine("Origem: » Crédito «");
                            }

                            inputTrans = Console.ReadKey();

                            if (inputTrans.Key == ConsoleKey.UpArrow)
                                opcaoTrans--;
                            else if (inputTrans.Key == ConsoleKey.DownArrow)
                                opcaoTrans++;

                            if (opcaoTrans < 0)
                                opcaoTrans = 3;
                            else if (opcaoTrans > 3)
                                opcaoTrans = 0;

                        } while (inputTrans.Key != ConsoleKey.Enter);

                        if (opcaoTrans == 0)
                            transacao.contaO = "Corrente";
                        else if (opcaoTrans == 1)
                            transacao.contaO = "Poupança";
                        else if (opcaoTrans == 2)
                            transacao.contaO = "Carteira";
                        else if (opcaoTrans == 3)
                            transacao.contaO = "Crédito";

                        Console.Clear();
                        Console.WriteLine("Olá sr.Renato\n\n");
                        Console.WriteLine("          Menu de Transações");
                        Console.WriteLine("─────────────────────────────────────");
                        Console.WriteLine("Cadastro de Transação\n");
                        Console.WriteLine("Tipo:      " + transacao.tipo);
                        Console.WriteLine("Valor:  R$ " + transacao.valor);
                        Console.WriteLine("Origem:   " + transacao.contaO);

                        transacao.data = DateTime.Now;
                        historico.Add(transacao);

                        //Alteração dos dados
                        if (transacao.contaO == "Corrente")
                            conta.corrente -= transacao.valor;
                        else if (transacao.contaO == "Poupança")
                            conta.poupanca -= transacao.valor;
                        else if (transacao.contaO == "Carteira")
                            conta.carteira -= transacao.valor;
                        else if (transacao.contaO == "Crédito")
                            conta.credito -= transacao.valor;
                    }
                    //TRANSFERÊNCIA
                    else if (transacao.tipo == "Transferência")
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Olá sr.Renato\n\n");
                            Console.WriteLine("          Menu de Transações");
                            Console.WriteLine("─────────────────────────────────────");
                            Console.WriteLine("Cadastro de Transação\n");
                            Console.WriteLine("Tipo:      " + transacao.tipo);
                            Console.WriteLine("Valor:  R$ " + transacao.valor);

                            if (opcaoTrans == 0)
                            {
                                Console.WriteLine("Origem:  » Corrente «");
                            }

                            else if (opcaoTrans == 1)
                            {
                                Console.WriteLine("Origem:  » Poupança «");
                            }

                            else if (opcaoTrans == 2)
                            {
                                Console.WriteLine("Origem:  » Carteira «");
                            }

                            inputTrans = Console.ReadKey();

                            if (inputTrans.Key == ConsoleKey.UpArrow)
                                opcaoTrans--;
                            else if (inputTrans.Key == ConsoleKey.DownArrow)
                                opcaoTrans++;

                            if (opcaoTrans < 0)
                                opcaoTrans = 2;
                            else if (opcaoTrans > 2)
                                opcaoTrans = 0;

                        } while (inputTrans.Key != ConsoleKey.Enter);

                        if (opcaoTrans == 0)
                            transacao.contaO = "Corrente";
                        else if (opcaoTrans == 1)
                            transacao.contaO = "Poupança";
                        else if (opcaoTrans == 2)
                            transacao.contaO = "Carteira";

                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Olá sr.Renato\n\n");
                            Console.WriteLine("          Menu de Transações");
                            Console.WriteLine("─────────────────────────────────────");
                            Console.WriteLine("Cadastro de Transação\n");
                            Console.WriteLine("Tipo:      " + transacao.tipo);
                            Console.WriteLine("Valor:  R$ " + transacao.valor);
                            Console.WriteLine("Origem:    " + transacao.contaO);

                            if (opcaoTrans == 0)
                            {
                                Console.WriteLine("Destino: » Corrente «");
                            }

                            else if (opcaoTrans == 1)
                            {
                                Console.WriteLine("Destino: » Poupança «");
                            }

                            else if (opcaoTrans == 2)
                            {
                                Console.WriteLine("Destino: » Carteira «");
                            }

                            inputTrans = Console.ReadKey();

                            if (inputTrans.Key == ConsoleKey.UpArrow)
                                opcaoTrans--;
                            else if (inputTrans.Key == ConsoleKey.DownArrow)
                                opcaoTrans++;

                            if (opcaoTrans < 0)
                                opcaoTrans = 2;
                            else if (opcaoTrans > 2)
                                opcaoTrans = 0;

                        } while (inputTrans.Key != ConsoleKey.Enter);

                        if (opcaoTrans == 0)
                            transacao.contaD = "Corrente";
                        else if (opcaoTrans == 1)
                            transacao.contaD = "Poupança";
                        else if (opcaoTrans == 2)
                            transacao.contaD = "Carteira";

                        Console.Clear();
                        Console.WriteLine("Olá sr.Renato\n\n");
                        Console.WriteLine("          Menu de Transações");
                        Console.WriteLine("─────────────────────────────────────");
                        Console.WriteLine("Cadastro de Transação\n");
                        Console.WriteLine("Tipo:      " + transacao.tipo);
                        Console.WriteLine("Valor:  R$ " + transacao.valor);
                        Console.WriteLine("Origem:    " + transacao.contaO);
                        Console.WriteLine("Destino:   " + transacao.contaD);

                        transacao.data = DateTime.Now;
                        historico.Add(transacao);

                        //Alteração dos dados
                        if (transacao.contaO == "Corrente")
                            conta.corrente -= transacao.valor;
                        else if (transacao.contaO == "Poupança")
                            conta.poupanca -= transacao.valor;
                        else if (transacao.contaO == "Carteira")
                            conta.carteira -= transacao.valor;

                        if (transacao.contaD == "Corrente")
                            conta.corrente += transacao.valor;
                        else if (transacao.contaD == "Poupança")
                            conta.poupanca += transacao.valor;
                        else if (transacao.contaD == "Carteira")
                            conta.carteira += transacao.valor;
                    }

                    Console.WriteLine("\nTransação concluída.");
                    transacoesActive = false;
                }
                //HISTÓRICO
                else if (opcao == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Olá sr.Renato\n\n");
                    Console.WriteLine("          Menu de Transações");
                    Console.WriteLine("─────────────────────────────────────");

                    for (int i = historico.Count - 1; i >= 0; i--)
                    {
                        if (historico[i].tipo == "Receita")
                        {
                            Console.Write(i + 1 + "° Transação: \n");
                            Console.Write("Tipo: " + historico[i].tipo + "\n");
                            Console.Write("Valor: " + historico[i].valor + "\n");
                            Console.Write("Destino: " + historico[i].contaD + "\n");
                            Console.Write("Data: " + historico[i].data + "\n\n");
                        }
                        else if (historico[i].tipo == "Despesa")
                        {
                            Console.Write(i + 1 + "° Transação: \n");
                            Console.Write("Tipo: " + historico[i].tipo + "\n");
                            Console.Write("Valor: " + historico[i].valor + "\n");
                            Console.Write("Origem: " + historico[i].contaO + "\n");
                            Console.Write("Data: " + historico[i].data + "\n\n");
                        }
                        else if (historico[i].tipo == "Transferência")
                        {
                            Console.Write(i + 1 + "° Transação: \n");
                            Console.Write("Tipo: " + historico[i].tipo + "\n");
                            Console.Write("Valor: " + historico[i].valor + "\n");
                            Console.Write("Origem: " + historico[i].contaO + "\n");
                            Console.Write("Destino: " + historico[i].contaD + "\n");
                            Console.Write("Data: " + historico[i].data + "\n\n");
                        }
                    }
                    transacoesActive = false;
                }
                //VOLTAR
                else if (opcao == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Olá sr.Renato\n\n");
                    Console.WriteLine("          Menu de Transações");
                    Console.WriteLine("─────────────────────────────────────");
                    if (ultimaTrans != "")
                    {
                        Console.WriteLine("Última transação cadastrada\n");
                        Console.WriteLine(ultimaTrans);
                    }
                    else
                        Console.WriteLine("Sem regístros rescentes...");
                    transacoesActive = false;
                }

                if (System.IO.File.Exists(@"dataH.rnt"))
                {
                    ultimaTrans = System.IO.File.ReadAllText(@"dataH.rnt");
                    if (ultimaTrans.Length > 19)
                    {
                        ultimaTrans = "Tipo:      " + historico.Last().tipo + "\nValor:  R$ " + historico.Last().valor + "\nOrigem:    " + historico.Last().contaO + "\nDestino:   " + historico.Last().contaD + "\nData:   " + historico.Last().data;
                    }
                    else
                        ultimaTrans = "Sem regístros rescentes...";
                }
            }
            Save();
        }

        //Gráfico de Movimentações
        static void Grafico()
        {
            Console.Clear();
            Console.WriteLine("Olá sr.Renato\n\n");
            Console.WriteLine("       Gráfico de Movimentações");
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("Em breve...");
            Save();
        }

        //Planejamentos
        //** INCLUIR LISTAS DE PLANEJAMENTOS **
        static void Planejamentos()
        {
            Console.Clear();
            Console.WriteLine("Olá sr.Renato\n\n");
            Console.WriteLine("           Planejamentos");
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("Em breve...");
            Save();
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 38;
            Load();
            Menu();
            Save();
        }
    }
}