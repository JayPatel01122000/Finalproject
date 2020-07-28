using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalProject
{
    public partial class Create_New_User : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jay\source\repos\FinalProject\Inventory.mdf;Integrated Security=True");
        public Create_New_User()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Registration WHERE User_Name ='" + textBox3.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter AB = new SqlDataAdapter(cmd);
            AB.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i == 0)
            {
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into Registration values('"+ textBox1.Text +"','"+ textBox2.Text +"','"+ textBox3.Text +"','"+ textBox4.Text+"','"+ textBox5.Text +"','"+ textBox6.Text +"')";
                cmd1.ExecuteNonQuery();

                textBox1.Text = "";textBox2.Text = "";textBox3.Text = "";
                textBox4.Text = "";textBox5.Text = "";textBox6.Text = "";
                display();
                MessageBox.Show("User record inserted successfull");
            }
            else
            {
                MessageBox.Show("This username already registered");
            }
        }

        private void Create_New_User_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            display();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void display()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Registration";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter AB = new SqlDataAdapter(cmd);
            AB.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Registration WHERE id = " + id +"";
            cmd.ExecuteNonQuery();
            display();
        }
    }
}
