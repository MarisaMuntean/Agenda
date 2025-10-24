using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace App
{
    public partial class Form1 : Form
    {
        List<Persoana> agenda = new List<Persoana>();
        TreeNode parentNode1 = new TreeNode();
        TreeNode parentNode2 = new TreeNode();
        TreeNode parentNode3 = new TreeNode();
        TreeNode parentNode4 = new TreeNode();
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "Prieteni", "Colegi", "Rude", "Diversi" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            parentNode1.Name = "Prieteni";
            parentNode1.Text = "Prieteni";
            treeView1.Nodes.Add(parentNode1);

            
            parentNode2.Name = "Colegi";
            parentNode2.Text = "Colegi";
            treeView1.Nodes.Add(parentNode2);

            
            parentNode3.Name = "Rude";
            parentNode3.Text = "Rude";
            treeView1.Nodes.Add(parentNode3);

            
            parentNode4.Name = "Diversi";
            parentNode4.Text = "Diversi";
            treeView1.Nodes.Add(parentNode4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Persoana p = new Persoana(textBox1.Text,textBox2.Text,dateTime1.Value,textBox3.Text,(Categorie)comboBox1.SelectedIndex);
            agenda.Add(p);

            TreeNode childNode = new TreeNode(p.Nume);
            childNode.Name = p.Nume;
            
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    parentNode1.Nodes.Add(childNode);
                    break;
                case 1:
                    parentNode2.Nodes.Add(childNode);
                    break;
                case 2:
                    parentNode3.Nodes.Add(childNode);
                    break;
                case 3:
                    parentNode4.Nodes.Add(childNode);
                    break;
                default:
                    MessageBox.Show("Categorie neselectata!");
                    break;
            }
            propertyGrid1.SelectedObject = p;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode nodSelectat = e.Node;
            Persoana persoanaSelectata = agenda.Find(p=>p.Nume==nodSelectat.Text);

            if (persoanaSelectata != null)
                propertyGrid1.SelectedObject = persoanaSelectata;
            else
            {
                propertyGrid1.SelectedObject = persoanaSelectata;
                MessageBox.Show("Persoana nu a fost gasita!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool found = false;

            if (propertyGrid1.SelectedObject == null)
            {
                MessageBox.Show("Selectați o persoană înainte de a șterge!");
                return;
            }

            string persoanaDeSters = (propertyGrid1.SelectedObject as Persoana).Nume;
            
            foreach (Persoana p in agenda)
            {
                if (persoanaDeSters == p.Nume)
                {
                    found = true;
                    if (MessageBox.Show("Doriti sa stergeti persoana [" + p.Nume + "]?", "Intrebare", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        agenda.Remove(p);

                        foreach (TreeNode node in treeView1.Nodes)
                        {
                            TreeNode nodeToRemove = node.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == p.Nume);
                            if (nodeToRemove != null)
                            {
                                node.Nodes.Remove(nodeToRemove);
                                propertyGrid1.SelectedObject = null;
                                break;
                            }
                        }

                    }
                    break;
                }                    
            }
            if(!found)
                MessageBox.Show("Persoana nu exista la contacte.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string persoanaCautata = textBox4.Text;
            bool found = false;

            foreach (Persoana p in agenda)
            {
                Debug.WriteLine(p.Nume);
                if (persoanaCautata == p.Nume)
                {
                    propertyGrid1.SelectedObject = p;
                    found = true;
                    break;
                }
                
            }
            if (found == false)
                MessageBox.Show($"{persoanaCautata} nu a fost gasita.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (Persoana p in agenda)
            {
                if (p.Nume.ToLower() == textBox1.Text.ToLower())
                {
                    textBox1.Clear();
                    label7.Text = "Persoana exista deja!";
                    break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string dir = Application.StartupPath;
            Debug.WriteLine(dir);
            StreamWriter sw = new StreamWriter(dir + "\\agenda.txt", false);

            foreach(Persoana p in agenda)
            {
                sw.WriteLine("Nume: " + p.Nume);
                sw.WriteLine("Categorie: " + p.Categorie);
                sw.WriteLine("Data nasterii: " + p.DataNasterii.ToShortDateString());
                sw.WriteLine("Telefon: " + p.Telefon);
                sw.WriteLine("Adresa: " + p.Adresa);
                sw.WriteLine("-----------------------------------------------");
            }
            sw.Close();
            Process.Start("notepad.exe", dir + "\\agenda.txt");
        }
    }
    internal class Persoana
    {
        private int index;
        private string nume;
        private DateTime dataNasterii;
        private string telefon;
        private string adresa;
        private Categorie categorie;

        [Description("Data nasterii"), Category("Date personale")]
        public DateTime DataNasterii //alternativa: string dataNasterii = datePicker.Value.ToShortDateString();
        {
            get
            {
                return dataNasterii;
            }
        }

        [Description("Index"), Browsable(false)]
        public int Index
        {
            get
            {
                return index;
            }
        }
        [Description("Numele complet al persoanei"), Category("Date personale")]

        public string Nume
        {
            get
            {
                return nume.ToString();
            }
        }

        [Description("Telefon"), Category("Date de contact")]

        public string Telefon
        {
            get
            {
                return telefon;
            }
            set
            {
                telefon = value;
            }
        }

        [Description("Categorie"), Category("Date de contact")]
        public Categorie Categorie
        {
            get
            {
                return categorie;
            }
            set
            {
                categorie = value;
            }
        }

        [Description("Adresa"), Category("Date de conatct")]
        public string Adresa
        {
            get
            {
                return adresa;
            }
            set
            {
                adresa = value;
            }
        }

        public Persoana(string nume, string adresa, DateTime dataNasterii, string telefon, Categorie categorie)
        {
            this.nume = nume;
            this.adresa = adresa;
            this.telefon = telefon;
            this.dataNasterii = dataNasterii;
            this.categorie = categorie;
        }

        
    }
    public enum Categorie : int
    {
        Prieteni,
        Colegi,
        Rude,
        Diversi
    };
}