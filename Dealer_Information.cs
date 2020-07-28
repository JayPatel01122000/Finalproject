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
    public partial class Dealer_Information : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jay\source\repos\FinalProject\Inventory.mdf;Integrated Security=True");

        public Dealer_Information()
        {
            InitializeComponent();
        }

        private void Dealer_Information_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            gr();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Dealer_Information values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+ textBox4.Text +"','"+ textBox5.Text+"')";
            cmd.ExecuteNonQuery();


            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            gr();
            MessageBox.Show("Record inserted Suceessfully");
        }

        public void gr()
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Dealer_Information";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ab = new SqlDataAdapter(cmd);
            ab.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Dealer_Information WHERE id = "+ id +"";
            cmd.ExecuteNonQuery();

            gr();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Dealer_Information WHERE id = "+ id +"";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ab = new SqlDataAdapter(cmd);
            ab.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                textBox6.Text = dr["Dealer_Name"].ToString();
                textBox7.Text = dr["Dealer_Company_Name"].ToString();
                textBox8.Text = dr["Contact"].ToString();
                textBox9.Text = dr["Address"].ToString();
                textBox10.Text = dr["City"].ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Dealer_Information set Dealer_Name = '" + textBox6.Text + "',Dealer_Company_Name ='"+ textBox7.Text + "',contact = '"+ textBox8.Text + "',address = '"+ textBox9.Text + "', city = '"+ textBox10.Text + "' WHERE id = " + id + "";
            cmd.ExecuteNonQuery();
            panel2.Visible = false;
            gr();
        }
    }
}
