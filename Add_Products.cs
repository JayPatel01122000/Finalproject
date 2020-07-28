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
    public partial class Add_Products : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jay\source\repos\FinalProject\Inventory.mdf;Integrated Security=True");
        public Add_Products()
        {
            InitializeComponent();
        }

        private void Add_Products_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            drop_down();
            data_grid();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert Into Products values('" + textBox1.Text + "','" + comboBox2.SelectedItem.ToString()+"')";
            cmd.ExecuteNonQuery();
            textBox1.Text = "";
            data_grid();
            MessageBox.Show("Record inserted successfully");
        }
        public void data_grid()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Products";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ab = new SqlDataAdapter(cmd);
            ab.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public void drop_down()
        {
            comboBox1.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Unit";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ab = new SqlDataAdapter(cmd);
            ab.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Unit"].ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            int i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            
            comboBox2.Items.Clear();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT * FROM Products WHERE id = " + i + "";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter ab2 = new SqlDataAdapter(cmd2);
            ab2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                comboBox2.Items.Add(dr2["Units"].ToString());
            }

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Products WHERE id = "+i+"";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ab = new SqlDataAdapter(cmd);
            ab.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox2.Text = dr["Product_Name"].ToString();
                comboBox2.SelectedText = dr["Units"].ToString();
            }
        }

        private void dataGridView1_CellClick(Object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            int i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            comboBox2.Items.Clear();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT * FROM Products WHERE id = " + i + "";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter ab2 = new SqlDataAdapter(cmd2);
            ab2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                comboBox2.Items.Add(dr2["Units"].ToString());
            }

            comboBox2.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Products WHERE id = " + i + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ab = new SqlDataAdapter(cmd);
            ab.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox2.Text = dr["Product_Name"].ToString();
                comboBox2.SelectedText = dr["Units"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            MessageBox.Show(i.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Products set Product_Name='" + textBox2.Text + "', Units='" + comboBox2.SelectedValue.ToString() + "' where id=" + i + "";
            cmd.ExecuteNonQuery();
            panel2.Visible = false;
            data_grid();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
 }

    

