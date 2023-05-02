using Npgsql;
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

namespace Rad_system_any2
{
    public partial class AddFuturaInfoForm : Form
    {

        NpgsqlConnection con;

        public AddFuturaInfoForm(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
        }



        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                //NpgsqlCommand cmd = new NpgsqlCommand($"insert into Product values(default, {textBox1.Text}, {textBox2.Text})", con);
                NpgsqlCommand cmd = new NpgsqlCommand($"insert into FuturaInfo(id_fut_info, id_futura, id_product, quantity, price) values(default, :id_futura, :id_product, :quantity, :price)", con);


                cmd.Parameters.AddWithValue("id_futura", Convert.ToInt64(textBox1.Text));
                cmd.Parameters.AddWithValue("id_product", Convert.ToInt64(textBox2.Text));
                cmd.Parameters.AddWithValue("quantity", Convert.ToInt64(textBox3.Text));
                cmd.Parameters.AddWithValue("price", Convert.ToInt64(textBox4.Text));

                cmd.ExecuteNonQuery();
                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Any exception");
            }



        }
    }
}
