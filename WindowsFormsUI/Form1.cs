using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsUI
{
    public partial class Form1 : Form
    {
        CarManager carManager = new CarManager(new InMemoryCarDal());
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string text = "";
            foreach (var item in carManager.GetAll())
            {
                text += item.Description + "\n";
            }
            label1.Text = text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string t = textBox1.Text;
            string text = "";
            try
            {
                foreach (var item in carManager.GetById(int.Parse(t)))
                {
                    text += item.Description + "\n";
                }
                label1.Text = text;
            }
            catch (Exception)
            {

                t = "-1";
            }
            
        }
    }
}
