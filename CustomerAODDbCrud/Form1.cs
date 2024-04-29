using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace CustomerAODDbCrud
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        private void ClearForm()
        {
            txtId.Clear();
            txtName.Clear();
            txtMobile.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtPassword.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "insert into customer values(@name,@mobile, @email, @address, @password)";
                cmd = new SqlCommand(qry, con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
             

                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Customer Details saved...");
                    ClearForm();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "select * from customer where id = @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", txtId.Text);

                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                     

                        txtName.Text = dr["name"].ToString();
                        txtMobile.Text = dr["mobile"].ToString();
                        txtEmail.Text = dr["email"].ToString();
                        txtAddress.Text = dr["address"].ToString();
                        txtPassword.Text = dr["password"].ToString();   
                    }
                }
                else
                {
                    MessageBox.Show("Customer Not Found..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update customer set name=@name, mobile=@mobile, email=@email,address=@address,password=@password where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", txtId.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);


                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Customer Details saved...");
                    ClearForm();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from customer where id = @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", txtId.Text);
                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Customer Record Deleted...");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from customer";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(dr);

                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
