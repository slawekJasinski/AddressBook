using MixERP.Net.VCards;
using MixERP.Net.VCards.Types;
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
namespace Aplikacja_bankowa
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            List<VCard> vCards = new List<VCard>();
            listView1.View = View.Details;
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Columns.Add("Imię", 100);
            listView1.Columns.Add("Nazwisko", 100);
            listView1.Columns.Add("Wiek", 100);
            listView1.Columns.Add("Płeć", 100);
            listView1.Columns.Add("Kod pocztowy", 100);
            listView1.Columns.Add("Miasto", 100);
            listView1.Columns.Add("Ulica", 100);
            listView1.Columns.Add("Nr domu", 100);
            listView1.Columns.Add("Nr mieszkania", 100);
        }

        private void CreateUser_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.Show();
        }

        private void ReadUser_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.Show();
        } 

        private void UpdateUser_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(this);
            form4.Show();
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            const string message = "Czy jesteś pewien, że chcesz usunąć kontakt?";
            const string caption = "Form Closing";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            ListView.SelectedIndexCollection indexes = this.listView1.SelectedIndices;
            foreach(int ix in indexes)
            {
                if (result == DialogResult.Yes)
                {
                    listView1.Items[ix].Remove();
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {

            List<string> list = listView1.Items.Cast<ListViewItem>()
                                 .Select(x => x.ToString()).ToList();
            /*
            string userPath = pathBox.Text;
            string[] files = Directory.GetFiles(userPath, "*.vcf");
            if (files.Length == 0) MessageBox.Show("Nie znaleziono pasujących elementów");
            else
            {
                foreach (var file in files)
                {
                    string fileContent = File.ReadAllText(file, Encoding.UTF8);
                    IEnumerable<VCard> vcards = Deserializer.GetVCards(fileContent);
            */
            IEnumerable<string> query = list.Where(n => n.ToLower().Contains(searchBox.Text.ToLower()));
                    foreach(string user in query)
                    {
                        MessageBox.Show("Znaleziono kontakt: " + user);
                    }
            // }
            //}
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (pathBox.Text == String.Empty)
            {
                MessageBox.Show("Pusta ścieżka. Uzupełnij ścieżkę do katalogu z kontaktami.");
            }
            else
            {

                try
                {
                    string userPath = pathBox.Text;
                    string[] files = Directory.GetFiles(userPath, "*.vcf");
                    foreach(var file in files)
                    {
                        string fileContent = File.ReadAllText(file, Encoding.UTF8);
                        IEnumerable<VCard> vcards = Deserializer.GetVCards(fileContent);
                        foreach (var vcard in vcards)
                        {
                            string[] arr = new string[9];
                            arr[0] = vcard.FirstName;
                            arr[1] = vcard.LastName;
                            int year1 = DateTime.Now.Year;
                            int year2 = vcard.BirthDay.Value.Year;
                            arr[2] = (DateTime.Now.Year - vcard.BirthDay.Value.Year).ToString();
                            arr[3] = vcard.Gender.ToString();
                            arr[4] = vcard.Addresses.ElementAt(0).PostalCode;
                            arr[5] = vcard.Addresses.ElementAt(0).Region;
                            arr[6] = vcard.Addresses.ElementAt(0).Street;
                            arr[7] = vcard.Addresses.ElementAt(0).Locality;
                            arr[8] = vcard.Addresses.ElementAt(0).PoBox;
                            ListViewItem lvi = new ListViewItem(arr);
                            listView1.Items.Add(lvi);
                            MessageBox.Show("Dodano użytkownika: \t" + vcard.FirstName + " " + vcard.LastName);
                        }
                    }
                }
                catch (System.Exception) 
                {
                    MessageBox.Show("Nie znaleziono kontaktów.");
                }
            }
        }

    }
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public string formattedName { get; set; }
        public Gender gender { get; set; }
        public string countryCode { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string houseNumber { get; set; }
        public string flatNumber { get; set; }

        public User(string firstName, string lastName, int age, Gender gender, string countryCode, string city, string street, string houseNumber, string flatNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.formattedName = String.Concat(firstName, " ", lastName);
            this.age = age;
            this.gender = gender;
            this.countryCode = countryCode;
            this.city = city;
            this.street = street;
            this.houseNumber = houseNumber;
            this.flatNumber = flatNumber;
        }
        User(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.formattedName = String.Concat(firstName, " ", lastName);
            this.email = null;
            this.age = age;
        }
        User(string firstName, string lastName, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.formattedName = String.Concat(firstName, " ", lastName);
            this.age = Int32.MinValue;
        }
    }
    

}
