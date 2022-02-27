using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BaiTap1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<nguoiDung> danhsach;
            danhsach = (List<nguoiDung>)Application["danhsach_user"];
            if (IsPostBack)
            {
                if (Request.Form["them"] == "Thêm")
                {
                    nguoiDung nd = new nguoiDung();
                    nd.MaND = Request.Form["ma"];
                    nd.HoTen = Request.Form["hoten"];
                    nd.NgaySinh = Request.Form["ngaysinh"];
                    nd.DiaChi = Request.Form["diachi"];
                    danhsach.Add(nd);
                    Application["danhsach_user"] = danhsach;

                }
                if(Request.Form["sua"]=="Sửa")
                {
                    foreach(nguoiDung nd in danhsach)
                    {
                        if (string.Equals(Request.Form["ma"], nd.MaND))
                        {
                            nd.MaND = Request.Form["ma"];
                            nd.HoTen = Request.Form["hoten"];
                            nd.NgaySinh= Request.Form["ngaysinh"];
                            nd.DiaChi= Request.Form["diachi"];
                            Response.Redirect("WebForm1.aspx");
                        }
                    }
                }
               
            }
            if (Request.QueryString["hoten"] != "")
            {
                ma.Value = Request.QueryString["ma"];
                hoten.Value = Request.QueryString["hoten"];
                ngaysinh.Value = Request.QueryString["ngaysinh"];
                diachi.Value = Request.QueryString["diachi"];

            }
           


            string chuoi = "";
            chuoi += "<table border='1'>";
            chuoi += "<tr>";
            chuoi += "<td>STT</td>";
            chuoi += "<td>Mã người dùng</td>";
            chuoi += "<td>Họ Tên</td>";
            chuoi += "<td>Ngày sinh</td>";
            chuoi += "<td>Địa chỉ</td>";
            chuoi += "<td>Tác vụ</td>";
            chuoi += "</tr>";
            
            int i = 1;
            foreach(nguoiDung nd in danhsach)
            {
                chuoi += "<tr>";
                chuoi += "<td>"+(i++)+"</td>";
                chuoi += "<td>"+ nd.MaND+"</td>";
                chuoi += "<td>" + nd.HoTen+"</td>";
                chuoi += "<td>" + nd.NgaySinh+"</td>";
                chuoi += "<td>" + nd.DiaChi+"</td>";
                chuoi += "<td><a href='WebForm1.aspx?ma="+nd.MaND+"&hoten=" + nd.HoTen + "&ngaysinh=" + nd.NgaySinh + "&diachi=" + nd.DiaChi + "'>Sửa</a></td>";
                chuoi += "</tr>";
            }
            chuoi += "</table>";
            bang.InnerHtml = chuoi;
        }

    }
}