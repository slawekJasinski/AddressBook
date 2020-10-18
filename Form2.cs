using MixERP.Net.VCards;
using MixERP.Net.VCards.Types;
using MixERP.Net.VCards.Extensions;
using MixERP.Net.VCards.Parser;
using MixERP.Net.VCards.Serializer;
using MixERP.Net.VCards.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Thought.vCards;


namespace Aplikacja_bankowa
{
    public partial class Form2 : Form
    {
        Form1 frm1 = new Form1();

        public Form2(Form1 form1)
        {
            InitializeComponent();
            frm1 = form1;
            comboBox1.Items.Add("Mężczyzna");
            comboBox1.Items.Add("Kobieta");
            comboBox1.Items.Add("Brak danych");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = new User(textBox1.Text, textBox2.Text, int.Parse(textBox3.Text), genderTranslate(comboBox1.Text), textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text)
            {
                firstName = textBox1.Text,
                lastName = textBox2.Text,
                age = int.Parse(textBox3.Text),
                //gender = (User.Gender)Enum.Parse(typeof(User.Gender), comboBox1.Text, true),
                gender = genderTranslate(comboBox1.Text),
                countryCode = textBox5.Text,
                city = textBox6.Text,
                street = textBox7.Text,
                houseNumber = textBox8.Text,
                flatNumber = textBox9.Text
            };
            VCard vCard = new VCard();
            vCard.Version = VCardVersion.V4;
            vCard.FirstName = textBox1.Text;
            vCard.LastName = textBox2.Text;
            vCard.FormattedName = String.Concat(textBox1.Text, "_", textBox2.Text);
            DateTime now = DateTime.Now;
            int year = now.Year - int.Parse(textBox3.Text);
            vCard.BirthDay = new DateTime(year, 1, 1);
            vCard.Gender = genderTranslate(comboBox1.Text);
            List<User> users = new List<User>();
            Address address = new Address();
            address.PostalCode = textBox5.Text;
            address.Region = textBox6.Text;
            address.Street = textBox7.Text;
            address.Locality = textBox8.Text;
            address.PoBox = textBox9.Text;
            List<Address> addresses = new List<Address>();
            addresses.Add(address);
            vCard.Addresses = addresses;
            string[] arr = new string[9];
            arr[0] = textBox1.Text;
            arr[1] = textBox2.Text;
            arr[2] = textBox3.Text;
            arr[3] = comboBox1.Text;
            arr[4] = textBox5.Text;
            arr[5] = textBox6.Text;
            arr[6] = textBox7.Text;
            arr[7] = textBox8.Text;
            arr[8] = textBox9.Text;
            ListViewItem lvi = new ListViewItem(arr);
            frm1.listView1.Items.Add(lvi);
            string userPath = frm1.pathBox.Text;
            string path = Path.Combine(userPath + vCard.FormattedName + ".vcf");
            File.WriteAllText(path, vCard.Serialize());
            const string message = "Pomyślnie dodano użytkownika";
            const string caption = "OK";
            var result = MessageBox.Show(message, caption,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private Gender genderTranslate(string gender)
        {
            if (gender == "Mężczyzna") return Gender.Male;
            else if (gender == "Kobieta") return Gender.Female;
            else return Gender.Other;
        }
    }
}