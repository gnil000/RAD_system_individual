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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rad_system_any2
{
    public partial class AddFuturaForm : Form
    {
        NpgsqlConnection con;

        public AddFuturaForm(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;

        }

        private void AddFuturaForm_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand($"insert into futura(id_futura, data_fut, id_client, predoplata) values(default, :data_fut , :id_client , :predoplata)", con);

                var dat = DateTime.Now;
                cmd.Parameters.AddWithValue("data_fut", dat);
                cmd.Parameters.AddWithValue("id_client", Convert.ToInt64(textBox2.Text));
                cmd.Parameters.AddWithValue("predoplata", checkBox1.Checked);

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
