using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiTap1
{
    public class nguoiDung
    { string maND;
        string hoten;
        string ngaysinh;
        string diachi;
        public string HoTen
        {
            get { return hoten; }
            set { hoten = value; }
        }
        public string MaND
        {
            get { return maND; }
            set { maND = value; }
        }
        public string NgaySinh 
        {
            get { return ngaysinh; }
            set { ngaysinh = value; }
        }
        public string DiaChi
        {
            get { return diachi; }
            set { diachi = value; }
        }
    }
}