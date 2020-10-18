using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Types;
using MixERP.Net.VCards.Serializer;
using System.IO;

namespace Aplikacja_bankowa
{
    public partial class Form4 : Form
    {
        Form1 frm1 = new Form1();

        public Form4(Form1 form1)
        {
            InitializeComponent();
            frm1 = form1;
            ListView.SelectedIndexCollection indexes = frm1.listView1.SelectedIndices;
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

        private void frm4edit_Click(object sender, EventArgs e)
        {
            string[] arr = new string[9];
            arr[0] = textBox1.Text;
            arr[1] = textBox2.Text;
            arr[2] = textBox3.Text;
            arr[3] = textBox4.Text;
            ListViewItem lvi = new ListViewItem(arr);
            frm1.listView1.Items.Add(lvi);
            VCard vCard = new VCard();
            vCard.Version = VCardVersion.V4;
            vCard.FirstName = textBox1.Text;
            vCard.LastName = textBox2.Text;
            vCard.FormattedName = String.Concat(textBox1.Text, "_", textBox2.Text);
            DateTime now = DateTime.Now;
            int year = now.Year - int.Parse(textBox3.Text);
            vCard.BirthDay = new DateTime(year, 1, 1);
            vCard.Gender = Gender.Male;
            string path = Path.Combine(@"D:\" + vCard.FormattedName + ".vcf");
            ListView.SelectedIndexCollection indexes = frm1.listView1.SelectedIndices;
            foreach(int ix in indexes)
            {
                frm1.listView1.Items[ix].Remove();
            }
            File.WriteAllText(path, vCard.Serialize());
            MessageBox.Show("Edytowano użytkownika: \t" + vCard.FirstName + " " + vCard.LastName);
            this.Close();
        }
    }
}
