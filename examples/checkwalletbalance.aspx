<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script runat="server">
		Sub Getwalletbalance(ByVal sender As Object, ByVal e As EventArgs)
    
		''connect to the wallet address entered in the textbox through the specified node
		Dim Mywallet As New Wavesdotnet.WalletConnection(addressinput.text,"localhost",6869)
		
		''use the GetWavesbalance function on the walletconnection
		wavesbalance.text=Mywallet.GetWavesBalance()
		End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
    Check Waves balance:<br />Wallet address <asp:textbox id=addressinput width=300 runat=server /><asp:button text="Check balance" id=validatebtn onclick=getwalletbalance runat=server /> <asp:Label ID="wavesbalance" runat="server"></asp:Label> Waves<br />
    </form>
</body>
</html>
