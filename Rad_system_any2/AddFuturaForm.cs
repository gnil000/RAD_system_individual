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
        DataSet ds;
        DataTable dt;
        NpgsqlConnection npgsqlConnection;

        public AddFuturaForm(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;



           // con = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Rad_system_shop");

            //con.Open();
            ConnectToDataBase();
            dataGridView1.Columns[0].HeaderText = "Код клиента";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Адресс";
            dataGridView1.Columns[3].HeaderText = "Телефон";

        }

        private void AddFuturaForm_Load(object sender, EventArgs e)
        {

        }

        void ConnectToDataBase()
        {
            ds = new DataSet();
            dt = new DataTable();
            string sql = "select * from client";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);

            ds.Reset();
            dataAdapter.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand($"insert into futura(id_futura, data_fut, id_client, predoplata) values(default, :data_fut , :id_client , :predoplata)", con);

                var dat = DateTime.Now;
                cmd.Parameters.AddWithValue("data_fut", dat);
                cmd.Parameters.AddWithValue("id_client", (int)dataGridView1.CurrentRow.Cells["id_client"].Value);
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
