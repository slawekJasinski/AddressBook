using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Serializer;
using System.IO;

namespace Aplikacja_bankowa
{
    public partial class Form3 : Form
    {
        Form1 frm1 = new Form1();

        public Form3(Form1 form1)
        {
            InitializeComponent();
            frm1 = form1;
            ListView.SelectedIndexCollection indexes = frm1.listView1.SelectedIndices;
            if (indexes.Count == 0)
            {
                MessageBox.Show("Nie wybrano elementu z listy");
            }
            else
            {
                foreach (int ix in indexes)
                {
                    textBox1.Text = frm1.listView1.Items[ix].SubItems[0].Text;
                    textBox2.Text = frm1.listView1.Items[ix].SubItems[1].Text;
                    textBox3.Text = frm1.listView1.Items[ix].SubItems[2].Text;
                    textBox4.Text = frm1.listView1.Items[ix].SubItems[3].Text;
                    textBox5.Text = frm1.listView1.Items[ix].SubItems[4].Text;
                    textBox6.Text = frm1.listView1.Items[ix].SubItems[5].Text;
                    textBox7.Text = frm1.listView1.Items[ix].SubItems[6].Text;
                    textBox8.Text = frm1.listView1.Items[ix].SubItems[7].Text;
                    textBox9.Text = frm1.listView1.Items[ix].SubItems[8].Text;
                }
            }
            this.Close();
        }
    }
}
