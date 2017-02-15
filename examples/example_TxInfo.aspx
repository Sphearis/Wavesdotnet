<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wavesdotnet Get Asset name</title>
    <script runat="server">
        Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim node1 As New Wavesdotnet.Node()
txinfo.text=node1.TxInfo()
txinfo2.text=node1.TxInfo("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC").type
txinfo3.text=node1.TxInfo("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC").sender
txinfo4.text=node1.TxInfo("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC").fee
txinfo5.text=node1.TxInfo("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC").amount
        End Sub
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<br />Result of <i>node1.transactioninfo() without parameters</i>: <asp:Label ID="txinfo" runat="server"></asp:Label><br /><br />
Result of <i>node1.transactioninfo("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC").type</i>: <asp:Label ID="txinfo2" runat="server"></asp:Label><br  />
Result of <i>node1.transactioninfo("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC").sender</i>: <asp:Label ID="txinfo3" runat="server"></asp:Label><br  />
Result of <i>node1.transactioninfo("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC").fee</i>: <asp:Label ID="txinfo4" runat="server"></asp:Label><br  />
Result of <i>node1.transactioninfo("xy3awDHd1TsMpnjnPHdZ8wEtEXezxx4m1SfLmSdLyqC").amount</i>: <asp:Label ID="txinfo5" runat="server"></asp:Label><br  />
    </div>
    </form>
</body>
</html>