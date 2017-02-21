<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wavesdotnet Example</title>
    <script runat="server">
            Public Node1 As New Wavesdotnet.Node()
        Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
Assetlist.DataSource = Node1.getAssetList("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW").balances
        Assetlist.DataBind()
                    End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
<asp:gridview id="Assetlist" 
                autogeneratecolumns="true" runat=server>

		      </asp:gridview>   
 </form>
</body>
</html>