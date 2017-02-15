<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wavesdotnet Get Asset Balance</title>
    <script runat="server">
        Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim node1 As New Wavesdotnet.Node()
balance.text=node1.GetAssetBalance()
balance2.text=node1.GetAssetBalance("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW","A7t6CtfSLbqhgM93oz2gbUzE8MxGEqCFDYVHEMxvN17i",8).tostring()
assetname.text=node1.GetAssetName("A7t6CtfSLbqhgM93oz2gbUzE8MxGEqCFDYVHEMxvN17i")
        End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<br />Result of <i>node1.getassetbalance() without parameters</i>: <asp:Label ID="balance" runat="server"></asp:Label><br /><br />
Result of <i>node1.getassetbalance("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW","A7t6CtfSLbqhgM93oz2gbUzE8MxGEqCFDYVHEMxvN17i",8)</i>: <asp:Label ID="balance2" runat="server"></asp:Label> <asp:Label ID="assetname" runat="server"></asp:Label> 
    </div>
    </form>
</body>
</html>