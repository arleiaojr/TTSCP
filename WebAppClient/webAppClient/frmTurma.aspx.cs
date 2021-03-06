﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppClient
{
    public partial class frmTurma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            carregaTurmas();
        }

        protected void BCriaTurma_Click(object sender, EventArgs e)
        {
            WSAppTTSCP.WSAppTTSCPSoapClient cliente = new WSAppTTSCP.WSAppTTSCPSoapClient();
            
            if (TBTurma.Text.Length<5)
            {
                LResultado.Text = "Resultado: A descrição da turma deve ter pelo menos 5 caracteres!";
                return;
            }
            
            LResultado.Text = "Resultado: " + cliente.criarTurma(TBTurma.Text);
        }

        protected void btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GlobalVar.TurmaAAssociar = btn.CommandArgument;
            Server.Transfer("frmAssociaMembroTurma.aspx", true);
        }

        protected void btnPesq_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GlobalVar.TurmaAAssociar = btn.CommandArgument;
            Server.Transfer("frmAssociaPesquisaTurma.aspx", true);
        }

        protected void btnExcluiTurma_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GlobalVar.TurmaAAssociar = btn.CommandArgument;
            Server.Transfer("frmTurmaDelete.aspx", true);
        }

        protected void btnChamada_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GlobalVar.TurmaAAssociar = btn.CommandArgument;
            Server.Transfer("frmChamada.aspx", true);
        }

        protected void btnBoletimFaltas_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GlobalVar.TurmaAAssociar = btn.CommandArgument;
            Server.Transfer("frmBoletimFaltas.aspx", true);
        }

        protected void carregaTurmas()
        {
            WSAppTTSCP.WSAppTTSCPSoapClient cliente = new WSAppTTSCP.WSAppTTSCPSoapClient();
            string turmas = cliente.listaTurmas();

            if (turmas.Length > 3)
            {
                string[] t = turmas.Split(new Char[] { '|' });

                TTurmas.BorderStyle = BorderStyle.Solid;
                TTurmas.BorderWidth = 1;

                TableRow tRow = new TableRow();
                tRow.BorderStyle = BorderStyle.Solid;
                tRow.BorderWidth = 1;
                tRow.BackColor = Color.FromArgb(16, 148, 171);
                TTurmas.Rows.Add(tRow);

                TableCell tCell = new TableCell();
                tCell.Text = "Turma";
                //tCell.BorderStyle = BorderStyle.Solid;
                //tCell.BorderWidth = 1;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Membros";
                //tCell.BorderStyle = BorderStyle.Solid;
                //tCell.BorderWidth = 1;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Pesquisas";
                //tCell.BorderStyle = BorderStyle.None;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Excluir Turma";
                //tCell.BorderStyle = BorderStyle.None;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Chamada";
                //tCell.BorderStyle = BorderStyle.None;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Frequência";
                //tCell.BorderStyle = BorderStyle.None;
                tRow.Cells.Add(tCell);

                for (int i = 0; i < t.Length; i++)
                {
                    //a estrutra da linha é sempre Nome|senha|email|tipo
                    if ((!String.IsNullOrEmpty(t[i])) & (t[i].CompareTo("\0") != 0))
                    {
                        if (String.IsNullOrEmpty(tb_filtro.Text))
                        {
                            goto Inclui;
                        }
                        else if (t[i].IndexOf(tb_filtro.Text) > -1)
                        {
                            goto Inclui;
                        }
                        else goto Proximo;
                        
                        Inclui:
                            tRow = new TableRow();
                            tRow.BorderStyle = BorderStyle.Solid;
                            tRow.BorderColor = Color.Black;
                            tRow.BackColor = (i%2 == 0 ? Color.White : Color.FromArgb(100, 196, 210));
                            tRow.BorderWidth = 1;
                            TTurmas.Rows.Add(tRow);

                            //label da turma
                            tCell = new TableCell();
                            //tCell.BorderStyle = BorderStyle.None;
                            Label l = new Label { ID = "L" + t[i], Text = t[i] };
                            tCell.Controls.Add(l);
                            tRow.Cells.Add(tCell);

                            //Botões de associar membro com a turma
                            tCell = new TableCell();
                            //tCell.BorderStyle = BorderStyle.None;
                            Button b = new Button { ID = "AM" + t[i], Text = "Membros", CommandArgument = t[i] };
                            b.Click += new EventHandler(btn_click);
                            tCell.Controls.Add(b);
                            tRow.Cells.Add(tCell);

                            //Botôes de associar pesquisa com a turma
                            tCell = new TableCell();
                            //tCell.BorderStyle = BorderStyle.None;
                            Button bpesq = new Button { ID = "AP" + t[i], Text = "Pesquisas", CommandArgument = t[i] };
                            bpesq.Click += new EventHandler(btnPesq_click);
                            tCell.Controls.Add(bpesq);
                            tRow.Cells.Add(tCell);

                            //Botôes de exlcuir turma
                            tCell = new TableCell();
                            //tCell.BorderStyle = BorderStyle.None;
                            Button bexlcuiTurma = new Button { ID = "ExcluirTurma" + t[i], Text = "Excluir Turma", CommandArgument = t[i]};
                            bexlcuiTurma.Click += new EventHandler(btnExcluiTurma_click);
                            tCell.Controls.Add(bexlcuiTurma);
                            tRow.Cells.Add(tCell);

                            //Botôes de chamada
                            tCell = new TableCell();
                            //tCell.BorderStyle = BorderStyle.None;
                            Button bchamada = new Button { ID = "Chamada" + t[i], Text = "Chamada", CommandArgument = t[i] };
                            bchamada.Click += new EventHandler(btnChamada_click);
                            tCell.Controls.Add(bchamada);
                            tRow.Cells.Add(tCell);

                            //Botôes de chamada
                            tCell = new TableCell();
                            //tCell.BorderStyle = BorderStyle.None;
                            Button bboletim = new Button { ID = "Boletim" + t[i], Text = "Frequência", CommandArgument = t[i] };
                            bboletim.Click += new EventHandler(btnBoletimFaltas_click);
                            tCell.Controls.Add(bboletim);
                            tRow.Cells.Add(tCell);
                        Proximo: ;
                    }
                }
            }
        }

        protected void BVerTurmas_Click(object sender, EventArgs e)
        {

        }
    }
}