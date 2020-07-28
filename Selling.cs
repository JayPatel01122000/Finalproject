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
    public partial class Selling : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jay\source\repos\FinalProject\Inventory.mdf;Integrated Security=True");
        DataTable dt = new DataTable();
        int tot = 0;

        public Selling()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Selling_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            dt.Clear();
            dt.Columns.Add("product");
            dt.Columns.Add("price");
            dt.Columns.Add("qty");
            dt.Columns.Add("total");

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            listBox1.Visible = true;

            listBox1.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Stock where Product_Name like ('" + textBox3.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["Product_Name"].ToString());
            }

        }
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
                try
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        this.listBox1.SelectedIndex = this.listBox1.SelectedIndex + 1;
                    }

                    if (e.KeyCode == Keys.Up)
                    {
                        this.listBox1.SelectedIndex = this.listBox1.SelectedIndex - 1;
                    }

                    if (e.KeyCode == Keys.Enter)
                    {
                        textBox3.Text = listBox1.SelectedIndex.ToString();
                        listBox1.Visible = false;
                        textBox4.Focus();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        private void textBox5_Leave(object sender, EventArgs e)
        {
            try
            {
                textBox6.Text = Convert.ToString(Convert.ToInt32(textBox4.Text) * Convert.ToInt32(textBox5.Text));
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            ////SqlCommand cmd = con.CreateCommand();
            ////cmd.CommandType = CommandType.Text;
            ////cmd.CommandText = "select top 1 * from Buying_Information where Product_Name='" + textBox3.Text + "' order by id desc";
            ////cmd.ExecuteNonQuery();
            ////DataTable dt = new DataTable();
            ////SqlDataAdapter da = new SqlDataAdapter(cmd);
            ////da.Fill(dt);
            ////foreach (DataRow dr in dt.Rows)
            ////{
            ////    textBox4.Text = dr["Price"].ToString();
            ////}
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    textBox6.Text = Convert.ToString(Convert.ToInt32(textBox4.Text) * Convert.ToInt32(textBox5.Text));
            //}
            //catch (Exception ex)
            //{

            //}
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 1 * from Buying_Information where Product_Name='" + textBox3.Text + "' order by id desc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox4.Text = dr["Price"].ToString();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int stock = 0;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from Stock where Product_Name='" + textBox3.Text + "'";
            cmd1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            foreach (DataRow dr1 in dt.Rows)
            {
                stock = Convert.ToInt32(dr1["Product_Quantity"].ToString());
            }
            if (Convert.ToInt32(textBox5.Text) > stock)
            {
                MessageBox.Show("this much value is not available");
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["product"] = textBox3.Text;
                dr["price"] = textBox4.Text;
                dr["qty"] = textBox5.Text;
                dr["total"] = textBox6.Text;
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;

                tot = tot + Convert.ToInt32(dr["total"].ToString());

                label10.Text = tot.ToString();


            }

            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tot = 0;
                dt.Rows.RemoveAt(Convert.ToInt32(dataGridView1.CurrentCell.RowIndex.ToString()));
                foreach (DataRow dr1 in dt.Rows)
                {
                    tot = tot + Convert.ToInt32(dr1["total"].ToString());
                    label10.Text = tot.ToString();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String orderid = "";
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " insert into Order_User values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Value.ToString("dd - MM - yyyy") + "')";
            cmd.ExecuteNonQuery();


            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "Select top[ 1 * from Order_User order by id desc";
        cmd2.ExecuteNonQuery();

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                orderid = dr2["id"].ToString();
            }

            foreach (DataRow dr in dt.Rows)
            {
                int qty = 0;
                string pname = "";

                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = " insert into Order_Item values('" + orderid.ToString() + "','" + dr["Product"].ToString() + "','" + dr["price"].ToString() + "','" + dr["qty"].ToString() + "','" + dr["total"].ToString() + "')";
                cmd3.ExecuteNonQuery();

                qty = Convert.ToInt32(dr["Quantity"].ToString());
                pname = dr["Product"].ToString();

                SqlCommand cmd6 = con.CreateCommand();
                cmd6.CommandType = CommandType.Text;
                cmd6.CommandText = "Update Stock set Product_Quantity = Product _Quantity-" + qty + " where Product_Name='" + pname.ToString() + ".'";

            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            label10.Text = "";

            dt.Clear();
            dataGridView1.DataSource = dt;

            MessageBox.Show("record inserted successfully");

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

