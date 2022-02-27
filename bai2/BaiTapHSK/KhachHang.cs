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
using System.Configuration;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace BaiTapHSK
{
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
            string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            txt_MaHD.Enabled = false;
            string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection con = new SqlConnection(db))
            {


                using (SqlCommand cmd = new SqlCommand("select*from tblKhachHang", con))
                {
                    con.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }

                }

            }
            report();
        }
        private void report()
        {
            ReportDocument cry = new ReportDocument();
            cry.Load(@"D:\LTHSK\BaiTapHSK\BaiTapHSK\CrystalReport1.rpt");
            crystalReportViewer1.ReportSource = cry;
            crystalReportViewer1.Refresh();

        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(db))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cnn;
                    cmd.CommandText = "ThemKH";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaKH", txtMaKH.Text);
                    cmd.Parameters.AddWithValue("@Hoten", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@DT", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);

                    using (SqlCommand check = new SqlCommand("select *from tblKhachHang", cnn))
                    {
                        bool KT = false;
                        cnn.Open();
                        using (SqlDataReader reader = check.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (int.Parse(txtMaKH.Text) == reader.GetInt32(0))
                                {
                                    MessageBox.Show("Mã khách hàng này đã tồn tại. Mời nhập mã khác");
                                    KT = true;
                                }

                            }
                            reader.Close();

                        }
                        if (KT == false)
                        {
                            cmd.ExecuteNonQuery();
                            KhachHang_Load(sender, e);

                        }

                    }


                }
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(db))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "suaKhachHang";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@maKH", txtMaKH.Text);
                    cmd.Parameters.AddWithValue("@Hoten", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@DT", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    KhachHang_Load(sender, e);



                }
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;

            using (SqlConnection cnn = new SqlConnection(db))
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandText = "XoaKH";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@maKH", txtMaKH.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        KhachHang_Load(sender, e);

                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.Text = dataGridView1.CurrentRow.Cells["iMaKH"].Value.ToString();
            txtHoTen.Text = dataGridView1.CurrentRow.Cells["sHoTen"].Value.ToString();
            txtSDT.Text = dataGridView1.CurrentRow.Cells["sDienThoai"].Value.ToString();
            txtDiaChi.Text = dataGridView1.CurrentRow.Cells["sDiaChi"].Value.ToString();
        }

        private void txtMaKH_Validating(object sender, CancelEventArgs e)
        {
           /* string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(db))
            {
                cnn.Open();

                using (SqlCommand kt = new SqlCommand("select *from tblKhachHang", cnn))
                {


                    using (SqlDataReader reader = kt.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (int.Parse(txtMaKH.Text) == reader.GetInt32(0))
                            {
                                errorProvider1.SetError(txtMaKH, "Mã KH không được trùng");
                                break;
                            }
                            else
                            {
                                errorProvider1.SetError(txtMaKH, "");
                            }

                        }
                        reader.Close();

                    }



                }
            }*/
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
            txtHoTen.Enabled = false;
            txtMaKH.Enabled = false;
            txt_MaHD.Enabled = false;
               
            if(listBox1.Text=="Họ Tên")
            {
                txtHoTen.Enabled = true;
                string rowFilter = string.Format("{0} like '{1}'", "sHoTen", "*" + txtHoTen.Text + "*");
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            }
            else
            {
                if(listBox1.Text=="Mã khách hàng")
                {
                    txtMaKH.Enabled=true;
                    string RowFilter = string.Format("CONVERT({0}, System.String) like '%{1}%'",
                             "iMaKH", txtMaKH.Text.Trim());
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = RowFilter;
                }
                else
                {
                    if(listBox1.Text =="Địa chỉ khách hàng")
                    {   txtDiaChi.Enabled=true;
                        string rowFilter = string.Format("{0} like '{1}'", "sDiaChi", "*" + txtDiaChi.Text + "*");
                        (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                    }
                    else
                    {
                        txt_MaHD.Enabled = true;
                        
                        string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(db))
                        {


                            using (SqlCommand cmd = new SqlCommand("select*from KH_HD", con))
                            {
                                con.Open();
                                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                                {
                                    DataTable dt = new DataTable();
                                    adapter.Fill(dt);

                                    dataGridView1.DataSource = dt;
                                    string RowFilter = string.Format("CONVERT({0}, System.String) like '%{1}%'",
                              "iMaHD", txt_MaHD.Text.Trim());
                                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = RowFilter;
                                }

                            }

                        }
                    }

                }

            } 
                
            
            
        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {
           
            
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            /*string rowFilter = string.Format("{0} like '{1}'", "sHoTen", "*" + txtHoTen.Text + "*");
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter;*/
        }

        private void KhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("do you want to exit?", "thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btn_BaoCao_Click(object sender, EventArgs e)
        {
            report();
        }
    }
}

