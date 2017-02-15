# Wavesdotnet v0.1
.Net framework you can use to interact with the Wavesplatform in your asp.Net web pages or .Net apps

New in v0.1:
- First official version, commands will be kept as they're integrated right now. Nothing will be deprecated in the future(or kept as legacy commands if needed), you can now use and update the api without having to worry about updating your own code.
- Commands based on a node connection instead of a wallet connection
- Using newtonsoft.json for maximum performances http://www.newtonsoft.com/json (newtonsoft.json.dll must also be present in your web application \bin directory)
- Library returns full objects to work with instead of simple variables, you're free to use Node.TxInfo("txid").fee or Node.TxInfo("txid").sender for example
- New examples available in the "examples" folder

You can find more information about the Waves platform here: https://wavesplatform.com/

This framework will allow you to interact with the Waves blockchain from any asp .net webpage or application, you'll be able to issue commands to your own node or to any external one with rpc enabled such as:
- Check any Waves balance on a wallet (already included in this version)
- Check any Token balance on a wallet (already included in this version)
- Getting any information about a transaction (already included in this version)
- Getting the asset list (with assetid, name and balance) of a wallet (in the next update)
- Transfer Waves or Tokens from one wallet to another
- Issue or reissue Tokens
- Trade Waves and Tokens on the DEX (Decentralized Exchange)
- ...

This should be enough to get you started with integrating the Waves platform into your web projects. I made this as simple and useful as possible, you can test this with 3 lines of code!(see the example below)

Possible uses are:
- Adding a personal cryptocurrency to your website to use it as a reward or payment system
- Create a trading bot
- Create your own exchange
- Transfer and store data securely, send messages on the blockchain(140 chars can be sent with any transaction)
- Create a blockchain explorer or blockchain analysis tools

##How to install it?

- If it doesn't exist yet, create a bin folder in the root of your web application, put the wavesdotnet.dll and the newtonsoft.jon.dll there
- That's all

If you use Visual studio, you can simply add the two dlls as a reference in your project to use all the commands available.

##How to use it?

First, you have to create a connection to a full node with rpc enabled:

VB: **Dim Node1 As New Wavesdotnet.Node("NodeIPorUrl","rpcport")**
C#: **Wavesdotnet.Node Node1 = new Wavesdotnet.Node("NodeIPorUrl","rpcport");**

NodeIPorUrl: You can input the public IP or Url of any node with rpc enabled or localhost if you run your own node on the same server(rpc has to be enabled in the config file). 
rpcport: The node rpc port(default is 6869)

For more information about running your own node, I found this tutorial to be the easiest to follow: https://www.cryptocompare.com/mining/guides/how-to-mine-waves/
**Remark:** You don't have to get 10000 Waves to run your own full node, that's only for generating blocks(mining). You can use a node with a new empty wallet to run your projects.

**If you run the command without any parameters, the default values of "localhost" and "6869"are used for NodeUrl and RPCPort respectively.**

When the connection is made, you can use these commands(more to come):
- **Node1.ConnectionCheck()** - Returns "NodeIPorUrl:port" of the Node1 connection as a string
- **Node1.GetWavesBalance("walletaddress")** - Returns the waves balance of the specified wallet address (as Double with corrected decimals)

- **Node1.GetAssetBalance("walletaddress", "assetID", decimals)** - Returns the specified asset balance of the specified wallet address(as Double with number of decimals specified in decimals)

**Remark:** assetID isn't the name but the unique identifier of the token. To find this ID, go into the portfolio section of your wallet(the suitcase) and click on the "Details" button next to the Token you wish to know the balance, the value next to "Identifier" is what you're looking for. 
decimals: Number of decimals of the token, used to get the correct value as tokens can get any amount of decimals when they are created.

- **Node1.TxInfo("TxID")** - Returns information about the transaction specified, informations available are type, id, sender, senderpublickey, recipient, assetid, amount, fee, timestamp, attachment, signature, height, name(for asset creation transactions) and description.
For example, *Node1.TxInfo("566kvw3YVxKc9LPt2UxCCGcnK7DSRK7qWFF84YrDrGGA").recipient* returns the recipient of that transaction.

- **Node1.GetAssetName("AssetID")** - Returns the specified asset name from its ID

##Example in VB
```
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wavesdotnet Example</title>
    <script runat="server">
        Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim Node1 As New Wavesdotnet.Node()
            getwavesbalance.Text = Node1.GetWavesBalance("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW")
	          getassetbalance.text = Node1.GetAssetBalance("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW","A7t6CtfSLbqhgM93oz2gbUzE8MxGEqCFDYVHEMxvN17i",8)
        End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="getwavesbalance" runat="server"></asp:Label> Waves<br />
    <asp:Label ID="getassetbalance" runat="server"></asp:Label> SphearX
    </form>
</body>
</html>
```

##Example in C#
```
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wavesdotnet Example</title>
    <script runat="server">
         protected void Page_Load(object sender, EventArgs e)
    {
        Wavesdotnet.Node Node1 = new Wavesdotnet.Node();
	getwavesbalance.Text = Node1.GetWavesBalance("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW");
	getassetbalance.text = Node1.GetAssetBalance("3PE9n5HRUsU6kjknatxPfvam7WmKy8EJcRW","A7t6CtfSLbqhgM93oz2gbUzE8MxGEqCFDYVHEMxvN17i",8);
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="getwavesbalance" runat="server"></asp:Label> Waves<br />
    <asp:Label ID="getassetbalance" runat="server"></asp:Label> SphearX
    </form>
</body>
</html>
```

More examples are located in the "Examples" folder.

To run these, you have to run a full node on the same server(or development computer) as the one used to host the webpage and enable rpc, or you can use a node hosted on another server but you have to replace "localhost" and "6869" accordingly.

