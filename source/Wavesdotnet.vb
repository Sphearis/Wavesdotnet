Imports System.Net

Public Class WalletConnection
    Public Property address As String
    Public Property confirmations As Integer
    Public Property balance As Double
    Public Property balances As ArrayList
    Public Property assetID As String
    Private walletaddress As String
    Private nodeurl As String
    Private nodeport As Integer

    Public Class Assetinfo
        Public Property assetID As String
        Public Property balance As Double
        Public Property issued As Boolean
    End Class
    Public Class Transactioninfo
        Public Property type As Integer
        Public Property id As String
        Public Property sender As String
        Public Property senderpublickey As String
        Public Property recipient As String
        Public Property assetid As String
        Public Property amount As Double
        Public Property fee As Double
        Public Property timestamp As Double
        Public Property attachment As String
        Public Property signature As String
        Public Property height As Integer
        Public Property name As String
        Public Property description As String
    End Class
    Function Getassetlist()
        Dim json As String
        Dim wc As New WebClient
        Dim rpcpath As String = "http://" & nodeurl & ":" & nodeport & "/assets/balance/" & walletaddress
        json = wc.DownloadString(rpcpath)
        Dim jss As New JavaScriptSerializer()
        Dim getinfowallet As WalletConnection = jss.Deserialize(Of WalletConnection)(json)
        Dim getassetinfo As ArrayList = getinfowallet.balances
        Return getassetinfo
    End Function
    Function Getassetname(AssetID As String)
        Dim json As String
        Dim wc As New WebClient
        Dim rpcpath As String = "http://" & nodeurl & ":" & nodeport & "/transactions/info/" & AssetID
        json = wc.DownloadString(rpcpath)
        Dim jss As New JavaScriptSerializer()
        Dim getinfowallet As Transactioninfo = jss.Deserialize(Of Transactioninfo)(json)
        Dim getassetnamer As String = getinfowallet.name
        Return getassetnamer
    End Function
    Function Gettransactioninfo(TransactionID As String)
        Dim json As String
        Dim wc As New WebClient
        Dim rpcpath As String = "http://" & nodeurl & ":" & nodeport & "/transactions/info/" & TransactionID
        json = wc.DownloadString(rpcpath)
        Dim jss As New JavaScriptSerializer()
        Dim gettransaction As Transactioninfo = jss.Deserialize(Of Transactioninfo)(json)
        Return gettransaction
    End Function
    Function GetWavesBalance() As Double
        Dim json As String
        Dim wc As New WebClient
        Dim rpcpath As String = "http://" & nodeurl & ":" & nodeport & "/addresses/balance/" & walletaddress
        json = wc.DownloadString(rpcpath)
        Dim jss As New JavaScriptSerializer()
        Dim getinfowallet As WalletConnection = jss.Deserialize(Of WalletConnection)(json)
        Return getinfowallet.balance / 100000000
    End Function
    Function GetAssetBalance(assetID As String, decimals As Integer) As Double
        Dim json As String
        Dim wc As New WebClient
        Dim rpcpath As String = "http://" & nodeurl & ":" & nodeport & "/assets/balance/" & walletaddress & "/" & assetID
        json = wc.DownloadString(rpcpath)
        Dim jss As New JavaScriptSerializer()
        Dim getinfowallet As WalletConnection = jss.Deserialize(Of WalletConnection)(json)
        Return getinfowallet.balance / (10 ^ decimals)
    End Function
    Public Sub New(ByVal parwalletaddress As String, ByVal parnodeurl As String, ByVal parnodeport As Integer)
        walletaddress = parwalletaddress
        nodeurl = parnodeurl
        nodeport = parnodeport


    End Sub
    Public Sub New()
    End Sub
End Class