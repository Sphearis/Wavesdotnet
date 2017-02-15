<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wavesdotnet Get Asset Name</title>
    <script runat="server">
        Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim node1 As New Wavesdotnet.Node()
assetname.text=node1.GetAssetName()
assetname2.text=node1.GetAssetName("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC")
        End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<br />Result of <i>node1.GetAssetName() without parameters</i>: <asp:Label ID="assetname" runat="server"></asp:Label><br /><br />
Result of <i>node1.GetAssetName("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC")</i>: <asp:Label ID="assetname2" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>