# Wavesdotnet
Asp .net components to interact with the Wavesplatform

You can find more information about the Waves platform here: https://wavesplatform.com/

This dll will allow you to interact with the Waves blockchain from any asp .net webpage or application, you'll be able to issue commands to your own node or to any external one with rpc enabled such as:
- Check any Waves balance on a wallet (already included in this version)
- Check any Token balance on a wallet (already included in this version)
- Gather information about transactions and blocks
- Transfer Waves or Tokens from one wallet to another
- Issue or reissue Tokens
- Trade Waves and Tokens on the DEX (Decentralized Exchange)
- ...

This should be enough to get you started with integrating the Waves platform into your web projects. I made this as simple and useful as possible, you can test this with 3 lines of code(see the sample below)!

Possible uses are:
- Adding a personal cryptocurrency to your website to use it as a reward or payment system
- Create a trading bot
- Create your own exchange
- Transfer and store data securely, send messages on the blockchain(140 chars can be sent with any transaction)
- Create a blockchain explorer or blockchain analysis tools

How to install it?

- If it doesn't exist yet, create a bin folder in the root of your web application and put the dll there
- That's all

If you use Visual studio, you can simply add the dll as a reference in your project to use all the commands available.

How to use it?

First, you have to create a connection to a wallet:

VB: Dim Mywallet As New Wavesdotnet.WalletConnection("Walletaddress","NodeIP",port)

Walletaddress: The wallet address you wish to connect to (for example 3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW)
NodeIP: You can input the public IP of any node with rpc enabled or localhost if you run your own node on the same server(rpc has to be enabled in the config file). For more information about running your own node, I found this tutorial to be the easiest to follow: https://www.cryptocompare.com/mining/guides/how-to-mine-waves/
! You only need 10000 Waves to generate blocks, you can use an empty node for your developments.
port: The node rpc port, default is 6869

When the connection is made, you can use these commands(more to come):
- Mywallet.GetWavesBalance() - Returns the waves balance of the connected wallet(as Double)

- Mywallet.GetAssetBalance("assetID",decimals) - Returns the specified asset balance of the connected wallet(as Double)
assetID: This is not the name but the unique ID. To find this ID, go into the portfolio section of your wallet and click on the "Details" button next to the Token you wish to know the balance, the value next to "Identifier" is what you're looking for. 
decimals: Number of decimals used by the token, used to get the correct value as tokens can get any amount of decimals when they are created.

Simple VB.net webpage Example:
```
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wavesdotnet Example</title>
    <script runat="server">
        Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim Mywallet As New Wavesdotnet.WalletConnection("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW","localhost",6869)
            getwavesbalance.Text = Mywallet.GetWavesBalance()
	          getassetbalance.text = Mywallet.GetAssetBalance("A7t6CtfSLbqhgM93oz2gbUzE8MxGEqCFDYVHEMxvN17i",8)
        End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="getwavesbalance" runat="server"></asp:Label> Waves<br />
    <asp:Label ID="getassetbalance" runat="server"></asp:Label> SphearX
    </div>
    </form>
</body>
</html>
```
To execute this example, you have to run a full node on the same server as the one used to host the webpage and enable rpc, or you can use another node but you have to replace "localhost" and 6869 accordingly.

