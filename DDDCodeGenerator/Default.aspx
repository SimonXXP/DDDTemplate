<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShineTechQD.DDDCodeGenerator.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>DDD 代码生成器</h2>
            <asp:CheckBoxList ID="cb_entities" runat="server"></asp:CheckBoxList>
            <asp:CheckBox ID="cb_all" Text="全选" runat="server" OnCheckedChanged="cb_all_CheckedChanged"/>
            <asp:Button runat="server" Text="生成代码" OnClick="btn_create_code" />
            <asp:Label ID="l_msg" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
