<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="BaiTap1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <div>
            Mã người dùng<input name="ma" id="ma" value="" runat="server" />
            <br />
            Họ tên<input name="hoten" id="hoten" value="" runat="server" />
            <br />
            Ngày sinh<input name="ngaysinh" id="ngaysinh" value="" runat="server" />
            <br />
            Địa chỉ <input name="diachi" id="diachi" value="" runat="server" />
            <br />
            <input type="submit" name="them" id="them" value="Thêm"  />
             <input type="submit" name="sua" id="sua" value="Sửa"  />

            <br />
            <div id="bang" name="bang" runat="server">

            </div>
        </div>
    </form>
</body>
</html>
