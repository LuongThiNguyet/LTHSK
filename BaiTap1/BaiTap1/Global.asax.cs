using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace BaiTap1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["danhsach_user"] = new List<nguoiDung>();
            nguoiDung nd = new nguoiDung();
            nd.MaND = "MD1";
            nd.HoTen = "Hoàng Thu Uyên";
            nd.NgaySinh = "2/2/2002";
            nd.DiaChi = "Hà Nội";
            List<nguoiDung> ds;
            ds = (List<nguoiDung>)Application["danhsach_user"];
            ds.Add(nd);
            Application["danhsach_user"] = ds;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}