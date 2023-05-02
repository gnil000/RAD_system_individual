using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rad_system_any2
{
    public partial class ClientForm : Form
    {

        DataSet ds;
        DataTable dt;
        NpgsqlConnection npgsqlConnection;


        public ClientForm()
        {
            InitializeComponent();

            npgsqlConnection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Rad_system_shop");

            npgsqlConnection.Open();
            ConnectToDataBase();
            dataGridView1.Columns[0].HeaderText = "Код клиента";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Адресс";
            dataGridView1.Columns[3].HeaderText = "Телефон";
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
        }


        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            npgsqlConnection.Close();
        }



        void ConnectToDataBase()
        {
            ds = new DataSet();
            dt = new DataTable();
            string sql = "select * from client";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds.Reset();
            dataAdapter.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }



        private void Delete(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from client where id_client = :id", npgsqlConnection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }


        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClientForm f1 = new AddClientForm(npgsqlConnection);
            f1.ShowDialog();
            ConnectToDataBase();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int id = (int)dataGridView1.CurrentRow.Cells["id_product"].Value;
            //string name = (string)dataGridView1.CurrentRow.Cells["name_product"].Value;
            //double price = Convert.ToDouble(dataGridView1.CurrentRow.Cells["price"].Value);
            //ChangeGoodForm f = new ChangeForm1(npgsqlConnection, id, name, price);
            //f.ShowDialog();
            //Update();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_client"].Value;
            Delete(id);
            //Update();
            ConnectToDataBase();

        }




    }
}
