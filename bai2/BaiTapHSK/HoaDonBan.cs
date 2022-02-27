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
namespace BaiTapHSK
{
    public partial class HoaDonBan : Form
    {
        public HoaDonBan()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void HoaDonBan_Load(object sender, EventArgs e)
        {
            string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection con = new SqlConnection(db))
            {


                using (SqlCommand cmd = new SqlCommand("select*from HD", con))
                {
                    con.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                    dataGridView1.Columns[0].HeaderText = "Mã hóa đơn";
                    dataGridView1.Columns[1].HeaderText = "Tên khách hàng";
                    dataGridView1.Columns[2].HeaderText = "Mã khách hàng";
                    dataGridView1.Columns[3].HeaderText = "Tên nhân viên";
                    dataGridView1.Columns[4].HeaderText = "Mã nhân viên";
                    dataGridView1.Columns[5].HeaderText = "Ngày đặt hàng";
                    dataGridView1.Columns[6].HeaderText = "Ngày giao hàng";
                    dataGridView1.Columns[7].HeaderText = "Địa chỉ giao";

                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             txtMaHD.Text = dataGridView1.CurrentRow.Cells["iMaHD"].Value.ToString();
             txtNgaydat.Text = dataGridView1.CurrentRow.Cells["dNgayDat"].Value.ToString();
             txtNgayGiao.Text = dataGridView1.CurrentRow.Cells["dNgayGiao"].Value.ToString();
             txtDiaChi.Text = dataGridView1.CurrentRow.Cells["sDiaChiGiao"].Value.ToString();
            txtMaKH.Text = dataGridView1.CurrentRow.Cells["iMaKH"].Value.ToString();
            txtMaNV.Text = dataGridView1.CurrentRow.Cells["iMaNV"].Value.ToString();

        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(db))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cnn;
                    cmd.CommandText = "ThemHDB";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@iMaHD", txtMaHD.Text);
                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@MaKH", txtMaKH.Text);
                    cmd.Parameters.AddWithValue("@NgayDat", txtNgaydat.Text);
                    cmd.Parameters.AddWithValue("@NgayGiao",txtNgayGiao.Text);
                    cmd.Parameters.AddWithValue("@DiaChiGiao", txtDiaChi.Text);

                    using (SqlCommand check = new SqlCommand("select *from HD", cnn))
                    {
                        bool KT = false;
                        cnn.Open();
                        using (SqlDataReader reader = check.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (int.Parse(txtMaHD.Text) == reader.GetInt32(0))
                                {
                                    MessageBox.Show("Mã hóa đơn này đã tồn tại. Mời nhập mã khác");
                                    KT = true;
                                }

                            }
                            reader.Close();

                        }
                        if (KT == false)
                        {
                            cmd.ExecuteNonQuery();
                            HoaDonBan_Load(sender, e);

                        }

                    }


                }
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(db))
            {   txtMaHD.Enabled=false;
                using (SqlCommand cmd = new SqlCommand())
                {  
                    cmd.Connection = cnn;
                    cmd.CommandText = "SuaHDB";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaHD", txtMaHD.Text);
                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@MaKH", txtMaKH.Text);
                    cmd.Parameters.AddWithValue("@NgayDat", txtNgaydat.Text);
                    cmd.Parameters.AddWithValue("@NgayGiao", txtNgayGiao.Text);
                    cmd.Parameters.AddWithValue("@DiaChiGiao", txtDiaChi.Text);
                    cnn.Open();
                    
                    cmd.ExecuteNonQuery();
                    HoaDonBan_Load(sender, e);

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
                        cmd.CommandText = "XoaHDB";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaHD", txtMaKH.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        HoaDonBan_Load(sender, e);

                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)

        {
            txtMaHD.Enabled = false;
            txtMaKH.Enabled = false;
            txtMaNV.Enabled = false;
            txtNgaydat.Enabled = false;
            txtNgayGiao.Enabled = false;
            txtDiaChi.Enabled  = false;
            if (danhsach.Text == "Mã hóa đơn")
            {
                txtMaHD.Enabled = true;
               
                string RowFilter = string.Format("CONVERT({0}, System.String) like '%{1}%'", "iMaHD"
                        , txtMaHD.Text.Trim());
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = RowFilter;

            }
            else
            {
                if (danhsach.Text == "Mã khách hàng")
                {
                    txtMaKH.Enabled = true;
                    string RowFilter = string.Format("CONVERT({0}, System.String) like '%{1}%'",
                             "iMaKH", txtMaKH.Text.Trim());
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = RowFilter;
                }
                else
                {
                    if (danhsach.Text == "Mã nhân viên")
                    {
                        txtMaNV.Enabled = true;
                        string RowFilter = string.Format("CONVERT({0}, System.String) like '%{1}%'",
                             "iMaNV", txtMaNV.Text.Trim());
                        (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = RowFilter;
                    }
                    else
                    {
                        if (danhsach.Text == "Ngày đặt hàng")
                        {
                            txtNgaydat.Enabled = true;
                            string RowFilter = string.Format("CONVERT({0}, System.String) like '%{1}%'",
                              "dNgayDat", txtNgaydat.Text.Trim());
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = RowFilter;
                        }
                        else
                        {
                            if (danhsach.Text == "Ngày giao hàng")
                            {
                                string RowFilter = string.Format("CONVERT({0}, System.String) like '%{1}%'",
                              "dNgayGiao", txtNgayGiao.Text.Trim());
                                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = RowFilter;
                            }
                            else
                            {
                                txtDiaChi.Enabled = true;
                                string rowFilter = string.Format("{0} like '{1}'", "sDiaChiGiao", "*" + txtDiaChi.Text + "*");
                                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                            }

                        }
                    }

                }

            }
       
        }

        private void txtMaHD_TextChanged(object sender, EventArgs e)
        {

        }

        private void HoaDonBan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("do you want to exit?", "thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txtMaHD.Enabled = true;
            txtMaKH.Enabled = true;
            txtMaNV.Enabled = true;
            txtDiaChi.Enabled=true;
            txtNgaydat.Enabled=true;    
            txtNgayGiao.Enabled=true;
            txtMaHD.Text = ("");
            txtDiaChi.Text = ("");
            txtMaKH.Text = ("");
            txtMaNV.Text = ("");
            txtNgaydat.Text = ("");
            txtNgayGiao.Text = ("");
            
        }
    }
}
