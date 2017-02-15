<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Waesdotnet Node connection example</title>
    <script runat="server">
        Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim node1 As New Wavesdotnet.Node()
            connectcheck.Text = node1.ConnectionCheck()
Dim node2 As New Wavesdotnet.Node("192.168.1.25","6701")
connect2check.Text = node2.ConnectionCheck()
        End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<b>Node connection with default values(no parameters)</B><br /><i><b>VB</b> Dim node1 As New Wavesdotnet.Node()</i><br /><i><b>C#</b> Wavesdotnet.Node node1 = New Wavesdotnet.Node();</i><br />
<br />Result of <i>node1.ConnectionCheck</i>: <asp:Label ID="connectcheck" runat="server"></asp:Label><br /><br /><br />
<b>Node connection with custom values(url and rpcport passed as parameters)</b><br /><i><b>VB</b> Dim node2 As New Wavesdotnet.Node("192.168.1.25","6701")</i><br /><i><b>C#</b> Wavesdotnet.Node node2 = New Wavesdotnet.Node("192.168.1.25","6701");</i><br />
<br />Result of <i>node2.ConnectionCheck</i>: <asp:Label ID="connect2check" runat="server"></asp:Label><br />
    </div>
    </form>
</body>
</html>