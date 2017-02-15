<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wavesdotnet Get Waves balance</title>
    <script runat="server">
        Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim node1 As New Wavesdotnet.Node()
balance.text=node1.GetWavesBalance()
balance2.text=node1.GetWavesBalance("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW")
        End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<br />Result of <i>node1.GetWavesBalance() without parameters</i>: <asp:Label ID="balance" runat="server"></asp:Label><br /><br />
Result of <i>node1.GetWavesBalance("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW")</i>: <asp:Label ID="balance2" runat="server"></asp:Label> Waves
    </div>
    </form>
</body>
</html>