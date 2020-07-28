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
    public partial class Buying_Info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jay\source\repos\FinalProject\Inventory.mdf;Integrated Security=True");
        public Buying_Info()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Buying_Info_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            Product_Name();
            Dealer_Name();
        }
        public void Product_Name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Products";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ab = new SqlDataAdapter(cmd);
            ab.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Product_Name"].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Products WHERE Product_Name = '" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ab = new SqlDataAdapter(cmd);
            ab.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                label3.Text = dr["Units"].ToString();
            }
        }
        public void Dealer_Name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Dealer_Information";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ab = new SqlDataAdapter(cmd);
            ab.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["Dealer_Name"].ToString());
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from stock where product_name=' " + comboBox1.Text + "'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter ab1 = new SqlDataAdapter(cmd1);
            ab1.Fill(dt1);
            i = Convert.ToInt32(dt1.Rows.Count.ToString());
            
            if (i == 0)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " insert into Buying_information values('" + comboBox1.Text + "','" + textBox1.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd - MM - yyyy") +"','" + comboBox2.Text + "','" + comboBox3.Text + "','" + dateTimePicker2.Value.ToString("dd - MM - yyyy") +"','" + textBox4.Text + "')";
                cmd.ExecuteNonQuery();

                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = " insert into stock values('" + comboBox1.Text + "','" + textBox1.Text + "','" + label3.Text + "')";
                cmd3.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = " insert into Buying_information values('" + comboBox1.Text + "','" + textBox1.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd - MM - yyyy") +"','" + comboBox2.Text + "','" + comboBox3.Text + "','" + dateTimePicker2.Value.ToString("dd - MM - yyyy") +"','" + textBox4.Text + "')";
        
        cmd2.ExecuteNonQuery();

                SqlCommand cmd5 = con.CreateCommand();
                cmd5.CommandType = CommandType.Text;
                cmd5.CommandText = "update Stock set Product_Quantity=Product_Quantity + " + textBox1.Text + " where Product_Name='" + comboBox1.Text + "' ";
                cmd5.ExecuteNonQuery();

            }

        }
    }
}

