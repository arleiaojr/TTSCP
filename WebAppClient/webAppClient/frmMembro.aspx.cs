﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppClient
{
    public partial class frmMembro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            verMembros();
        }

        protected void btnExcluirMembro_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GlobalVar.emailMembroAExlcuir = btn.CommandArgument;
            Server.Transfer("frmMembroDelete.aspx", true);
        }

        protected void verMembros()
        {
            WSAppTTSCP.WSAppTTSCPSoapClient cliente = new WSAppTTSCP.WSAppTTSCPSoapClient();
            string membros = cliente.dadosTodosMembros();

            if (membros.CompareTo("Não existem membros cadastrados!") != 0)
            {
                string[] m = membros.Split(new Char[] { '&' });

                TMembros.BorderStyle = BorderStyle.Solid;
                TMembros.BorderWidth = 1;

                TableRow tRow = new TableRow();
                tRow.BorderStyle = BorderStyle.Solid;
                tRow.BorderWidth = 1;
                tRow.BackColor = Color.FromArgb(16, 148, 171);
                TMembros.Rows.Add(tRow);

                TableCell tCell = new TableCell();
                tCell.Text = "Nome do Membro";
                tCell.BorderStyle = BorderStyle.None;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "E-mail do Membro";
                tCell.BorderStyle = BorderStyle.None;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Tipo do Membro";
                tCell.BorderStyle = BorderStyle.None;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Excluir Membro";
                tCell.BorderStyle = BorderStyle.None;
                tRow.Cells.Add(tCell);

                for (int i = 0; i < m.Length; i++)
                {
                    if ((!String.IsNullOrEmpty(m[i])) & (m[i].CompareTo("\0") != 0))
                    {
                        string[] mIndividual = m[i].Split(new Char[] { '|' });

                        if (String.IsNullOrEmpty(tb_filtro.Text))
                        {
                            goto Inclui;
                        }
                        else if (mIndividual[1].IndexOf(tb_filtro.Text) > -1)
                        {
                            goto Inclui;
                        }
                        else goto Proximo;
                        
                        Inclui:
                            tRow = new TableRow();
                            tRow.BorderStyle = BorderStyle.Solid;
                            tRow.BorderColor = Color.Black;
                            tRow.BackColor = (i % 2 == 0 ? Color.White : Color.FromArgb(100, 196, 210));
                            tRow.BorderWidth = 1;
                            TMembros.Rows.Add(tRow);

                            //nome do membro
                            tCell = new TableCell();
                            tCell.BorderStyle = BorderStyle.None;
                            tCell.Text = mIndividual[0];
                            tRow.Cells.Add(tCell);

                            //email do membro
                            tCell = new TableCell();
                            tCell.BorderStyle = BorderStyle.None;
                            tCell.Text = mIndividual[1];
                            tRow.Cells.Add(tCell);

                            //tipo do membr
                            tCell = new TableCell();
                            tCell.BorderStyle = BorderStyle.None;
                            tCell.Text = mIndividual[2];
                            tRow.Cells.Add(tCell);

                            //botão para excluir membro
                            tCell = new TableCell();
                            tCell.BorderStyle = BorderStyle.None;
                            Button b = new Button { ID = "ExCluirMembro" + mIndividual[1], Text = "Excluir Membro", CommandArgument = mIndividual[1] };
                            b.Click += new EventHandler(btnExcluirMembro_click);
                            tCell.Controls.Add(b);
                            tRow.Cells.Add(tCell);
                        Proximo: ;
                    }
                }
            }

        }

        protected void BIncluirMembro_Click(object sender, EventArgs e)
        {
            WSAppTTSCP.WSAppTTSCPSoapClient cliente = new WSAppTTSCP.WSAppTTSCPSoapClient();
            
            if (TBNomeMembro.Text.Length<3)
            {
                LResultado.Text = "Resultado: Preencha o nome do membro!";
                return;
            }

            if (TBEmailMembro.Text.Length < 3)
            {
                LResultado.Text = "Resultado: Preencha o e-mail do membro!";
                return;
            }

            LResultado.Text = "Resultado: " + cliente.criarMembro(TBNomeMembro.Text, TBEmailMembro.Text, Convert.ToInt32(DDLTipoMembro.SelectedValue));
        }

        protected void BVerMembros_Click(object sender, EventArgs e)
        {
            
        }
        
    }
}