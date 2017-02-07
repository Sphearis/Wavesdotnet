<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script runat="server">
	
		Sub Getwalletbalance(ByVal sender As Object, ByVal e As EventArgs)
		''connect to any wallet address through the specified node
		Dim Mywallet As New Wavesdotnet.WalletConnection("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW","localhost",6869)
		
		''use the GetAssetName function on the specified assetID
		assetname.text=Mywallet.GetAssetName(assetidinput.text)
		End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Get Asset Name:<br />AssetID <asp:textbox id=assetidinput width=300 runat=server /><asp:button text="Get Name" id=validatebtn onclick=getassetname runat=server /> <asp:Label ID="assetname" runat="server"></asp:Label><br />

    </div>
    </form>
</body>
</html>