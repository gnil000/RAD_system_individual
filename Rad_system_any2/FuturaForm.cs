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
    public partial class FuturaForm : Form
    {
        DataSet ds, ds2;
        DataTable dt, dt2;
        NpgsqlConnection npgsqlConnection;


        public FuturaForm()
        {
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Rad_system_shop");

            npgsqlConnection.Open();
            ConnectToDataBase();
            
            dataGridView1.Columns[0].HeaderText = "Код накладной";
            dataGridView1.Columns[1].HeaderText = "Дата создания";
            dataGridView1.Columns[2].HeaderText = "ФИО клиента";
            dataGridView1.Columns[3].HeaderText = "Сумма";
            dataGridView1.Columns[4].HeaderText = "Предоплата";


            WatchFuturaInfo();
            dataGridView2.Columns[0].HeaderText = "Код отчёта";
            dataGridView2.Columns[1].HeaderText = "Код накладной";
            dataGridView2.Columns[2].HeaderText = "Продукт";
            dataGridView2.Columns[3].HeaderText = "Количество";
            dataGridView2.Columns[4].HeaderText = "Сумма";
        }

        private void FuturaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            npgsqlConnection.Close();
        }



        void ConnectToDataBase()
        {
            ds = new DataSet();
            dt = new DataTable();
            //string sql = "select * from futura";

            string sql = "select id_futura, data_fut, client.name_client, total_sum, predoplata from futura inner join client on futura.id_client = client.id_client ";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds.Reset();
            dataAdapter.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }

        void WatchFuturaInfo() { 
            ds2 = new DataSet();
            dt2 = new DataTable();
            string sql = "select id_fut_info, id_futura, product.name_product, quantity, price from futurainfo inner join product on futurainfo.id_product = product.id_product";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds2.Reset();
            dataAdapter.Fill(ds2);
            dt2 = ds2.Tables[0];
            dataGridView2.DataSource = dt2;

        }


        private void PickFuturaInfo(object sender, EventArgs e) {
            int id = (int)dataGridView1.CurrentRow.Cells["id_futura"].Value;


            string sql = $"select id_fut_info, id_futura, product.name_product, quantity, price from futurainfo inner join product on futurainfo.id_product = product.id_product where id_futura = {id}";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds2.Reset();
            dataAdapter.Fill(ds2);
            dt2 = ds2.Tables[0];
            dataGridView2.DataSource = dt2;

        }



        private void Delete(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from futura where id_futura = :id", npgsqlConnection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }


        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int id = (int)dataGridView1.CurrentRow.Cells["id_futura"].Value;
            AddFuturaForm f1 = new AddFuturaForm(npgsqlConnection);
            f1.ShowDialog();
            ConnectToDataBase();
        }

        private void добавитьВоВторуюТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_futura"].Value;
            AddFuturaInfoForm f1 = new AddFuturaInfoForm(npgsqlConnection, id);
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
            int id = (int)dataGridView1.CurrentRow.Cells["id_futura"].Value;
            Delete(id);
            //Update();
            ConnectToDataBase();

        }



    }
}
