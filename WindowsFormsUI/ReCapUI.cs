using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
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
    public partial class mainForm : Form
    {
        CarManager carManager;
        public mainForm()
        {
            carManager = new CarManager(new InMemoryCarDal());
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = "";
            foreach (Car car in carManager.GetAll())
            {
                text += "Description : " + car.Description + "\n" +
                    "Daily Price : " + car.DailyPrice.ToString() + "\n" +
                    "Model Year : " + car.ModelYear.ToString() + "\n\n";
            }

            richTextBox1.Text = text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = "";
            int id;
            try
            {
                id = int.Parse(textBox1.Text);
            }
            catch (Exception)
            {

                id = -1;
            }

            foreach (Car car in carManager.GetById(id))
            {
                text += "Description : " + car.Description + "\n" +
                "Daily Price : " + car.DailyPrice.ToString() + "\n" +
                "Model Year : " + car.ModelYear.ToString() + "\n\n";
            }
            richTextBox1.Text = text;
        }
    }
}
