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
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jay\source\repos\FinalProject\Inventory.mdf;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Registration WHERE User_Name ='"+ textBox1.Text +"' and Password = '"+ textBox2.Text +"' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter AB = new SqlDataAdapter(cmd);
            AB.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());  
            if(i == 0)
            {
                MessageBox.Show("Incorrect Information");
            }
            else
            {
                this.Hide();
                MDIParent1 mdi = new MDIParent1();
                mdi.Show();
            }
        }
        private void Login_Load_1(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open);
            {
                con.Close();
            }
            con.Open();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
