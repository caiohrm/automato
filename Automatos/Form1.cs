using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Automatos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Inicializa();
        }

        
        private void Inicializa()
        {

            DefineColunas();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F5)
                Soluciona();
            if (keyData == Keys.F6)
                DefineEntrada();
            if (keyData == Keys.F7)
                DefineSaida();
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void DefineEntrada()
        {
            DefineEstado(ENTRADA);
        }

        private string ENTRADA = "ENTRADA";
        private string SAIDA = "SAIDA";
        private void DefineSaida()
        {
            DefineEstado(SAIDA);

        }

        private void DefineEstado(string estado)
        {
            if (DtPassos.SelectedRows.Count > 0)
            {
                DataGridViewRow item = DtPassos.SelectedRows[0];
                item.Tag = estado;
                string origem = item.Cells[0].Value == null ? "" : item.Cells[0].Value.ToString();
                MessageBox.Show(string.Format("Estado {0} marcado como {1}", origem,estado));
            }

        }

        private void Soluciona()
        {
            lstEstados = new List<Estado>();
            lstAlfabeto = new List<string>();
            foreach (DataGridViewRow item in DtPassos.Rows)
            {

                string origem = item.Cells[0].Value == null ? "" : item.Cells[0].Value.ToString();
                string entrada = item.Cells[1].Value == null ? "" : item.Cells[1].Value.ToString();
                string destino = item.Cells[2].Value == null ? "" : item.Cells[2].Value.ToString();
                string ehEntrada = item.Tag != null ? item.Tag.ToString() : "";


                if (string.IsNullOrEmpty(origem) || string.IsNullOrEmpty(entrada) || string.IsNullOrEmpty(destino))
                    continue;
                if (lstAlfabeto.FindIndex(x => x.Equals(entrada)) < 0)
                    lstAlfabeto.Add(entrada);
                int i = lstEstados.FindIndex(x => x.NomeEstado.Equals(origem));
                Estado estado = i >= 0 ? lstEstados[i] : new Estado(origem);

                estado.EhEntrada =estado.EhEntrada ? estado.EhEntrada : ehEntrada.Equals(ENTRADA);
                estado.EhSaida = estado.EhSaida ? estado.EhSaida : ehEntrada.Equals(SAIDA);
                Passos passos = new Passos(entrada, destino);
                estado.LstPassos.Add(passos);
                if (i < 0)
                    lstEstados.Add(estado);
                i = lstEstados.FindIndex(x => x.NomeEstado.Equals(destino));
                if (i < 0)
                {
                    estado = new Estado(destino);
                    lstEstados.Add(estado);
                }
            }
            DefineSolucao();
        }




        private void DefineSolucao()
        {
            if (!EhDeterministico() || !EhAcessivel() || !EhTotal())
                return;
            DtSolucao.Columns.Clear();
            DtSolucao.Columns.Add("", "");
            int c = lstEstados.Count ;
            for (int i = 0; i < c -1; i++)
            {
                DtSolucao.Columns.Add(lstEstados[i].NomeEstado, lstEstados[i].NomeEstado);
            }

                for (int i = 1; i < c; i++)
            {
                DtSolucao.Rows.Add();
                DataGridViewRow row = DtSolucao.Rows[i -1];
                row.Cells[0].Value = lstEstados[i].NomeEstado;
            }

            DefEquals();
            Resolve();
        }

        private bool EhTotal()
        {
            foreach (string item in lstAlfabeto)
            {
                foreach (Estado estado in lstEstados)
                {
                    if (!(estado.LstPassos.FindIndex(x => x.Entrada.Equals(item)) >= 0))
                    {
                        MessageBox.Show(String.Format("Estado {0} não possui ação para a entrada {1}",estado.NomeEstado,item));
                        return false;
                    }
                }
            }
            return true;
        }

        private void Resolve()
        {
            int c = DtSolucao.ColumnCount;
            for (int i = 0; i < c; i++)
            {
                DataGridViewColumn column = DtSolucao.Columns[i];
                string nome = column.Name;
                Estado estadoColumn = lstEstados.Find(x => x.NomeEstado.Equals(nome));
                if (estadoColumn == null)
                    continue;
                int b = DtSolucao.RowCount;
                for (int j = 0; j < b; j++)
                {
                    DataGridViewRow row = DtSolucao.Rows[j];
                    string valor = row.Cells[i].Value != null ? row.Cells[i].Value.ToString() : "";
                    if(!string.IsNullOrEmpty(valor))
                        continue;
                    nome = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : "";
                    Estado estadoRow = lstEstados.Find(x => x.NomeEstado.Equals(nome));
                    if (estadoRow == null)
                        continue;
                    bool equivalente = true;
                    foreach (string letra in lstAlfabeto)
                    {
                        Estado final1 = RetornaEstado(estadoRow, letra);
                        Estado final2 = RetornaEstado(estadoColumn, letra);
                        if (final1.EhSaida != final2.EhSaida)
                        {
                            row.Cells[i].Value = "X";
                            equivalente = false;
                        }
                    }
                    if(equivalente)
                        row.Cells[i].Value = "EQ";
                }
            }
            Finaliza();
        }


        private void Finaliza()
        {
            DtFinal.Rows.Clear();
            foreach (DataGridViewRow row in DtPassos.Rows)
            {
                int i = DtFinal.Rows.Add();
                DtFinal.Rows[i].Cells[0].Value = DtPassos.Rows[i].Cells[0].Value;
                DtFinal.Rows[i].Cells[1].Value = DtPassos.Rows[i].Cells[1].Value;
                DtFinal.Rows[i].Cells[2].Value = DtPassos.Rows[i].Cells[2].Value;
            }

            int c = DtSolucao.RowCount;
            int b = DtSolucao.ColumnCount;
            for (int d = 1; d < b; d++)
            {
                for (int i = 0; i < c; i++)
                {
                    object conteudo = DtSolucao.Rows[i].Cells[d].Value;
                    string cont = conteudo == null ? "" : conteudo.ToString();
                    if (cont.Equals("EQ"))
                    {
                        Substituir(DtSolucao.Rows[i].Cells[0].Value.ToString(),DtSolucao.Columns[d].Name);
                    }
                }
            }
        }

        private void Substituir(string linha, string coluna)
        {
            foreach (DataGridViewRow row in DtFinal.Rows)
            {
                if(row.Cells[0].Value == null)
                    continue;
                string origem = row.Cells[0].Value.ToString();
                string fim = row.Cells[2].Value.ToString();
                if (origem.Equals(linha) || origem.Equals(coluna))
                {
                    row.Cells[0].Value = linha + coluna;
                }
                if (fim.Equals(linha) || fim.Equals(coluna))
                    row.Cells[2].Value = linha + coluna;
            }
        }


        private void RemoveIgualdade()
        {
            int i = DtFinal.RowCount;
            for (int j = 0; j < i; j++)
            {
                
            }



        }

        private Estado RetornaEstado(Estado origem, string passos)
        {

            foreach (char item in passos)
            {
                int i = origem.LstPassos.FindIndex(x => x.Entrada.Equals(item.ToString()));
                if (i >= 0)
                {
                    Passos destinos = origem.LstPassos[i];
                    i = lstEstados.FindIndex(x => x.NomeEstado.Equals(destinos.Destino));
                    origem = lstEstados[i];
                    continue;
                }
                return null;
            }


            return origem;
        }



        private void DefEquals()
        {
            int c = DtSolucao.ColumnCount;
            int b = DtSolucao.RowCount;
            for (int i = 1; i < c; i++)
            {
                DataGridViewColumn column = DtSolucao.Columns[i];
                for (int j = 0; j < b; j++)
                {
                    DataGridViewRow row = DtSolucao.Rows[j];
                    string valor = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : "";
                    if(string.IsNullOrEmpty(valor))
                        continue;
                    if (valor.Equals(column.Name))
                    {
                        for (int k = i; k < c; k++)
                        {
                            row.Cells[k].Value = "X";    
                        }
                        continue;    
                    }
                    Estado coluna = lstEstados.Find(x => x.NomeEstado.Equals(column.Name));
                    Estado linha = lstEstados.Find(x => x.NomeEstado.Equals(valor));
                    if ((coluna.EhSaida != linha.EhSaida))
                    {
                        row.Cells[i].Value = "X";
                    }
                }

            }
        }

        private bool EhAcessivel()
        {
            foreach (Estado item in lstEstados)
            {
                if(item.EhEntrada)
                    continue;

                if (
                    lstEstados.FindAll(
                        x =>
                            x.LstPassos.Exists(
                                b => b.Destino.Equals(item.NomeEstado) && !x.NomeEstado.Equals(item.NomeEstado))).Count == 0)
                {

                    MessageBox.Show(string.Format("Item {0} não é acessivel", item.NomeEstado));
                    return false;
                }


            }
            return true;
        }

        private bool EhDeterministico()
        {
            foreach (Estado item in lstEstados)
            {
                if (!item.LstPassos.TrueForAll(x => item.LstPassos.FindAll(c => c.Entrada.Equals(x.Entrada)).Count < 2))
                {
                    MessageBox.Show(string.Format("Não é deterministico Estado {0} possui mais de uma possibilidade",
                        item.NomeEstado));
                    return false;
                }
            }
            return true;
        }

        List<Estado> lstEstados;
        List<string> lstAlfabeto;

        private void DefineColunas()
        {
            DtPassos.ColumnHeadersVisible = true;
            DtPassos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DtPassos.Columns.Add("origem", "Origem");
            DtPassos.Columns.Add("entrada", "Entrada");
            DtPassos.Columns.Add("saida", "Saída");

            DtFinal.ColumnHeadersVisible = true;
            DtFinal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DtFinal.Columns.Add("origem", "Origem");
            DtFinal.Columns.Add("entrada", "Entrada");
            DtFinal.Columns.Add("saida", "Saída");
        }
    }
}
