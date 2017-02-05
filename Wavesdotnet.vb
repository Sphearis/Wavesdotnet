Imports System.Net

Public Class WalletConnection
    Public Property address As String
    Public Property confirmations As Integer
    Public Property balance As Double
    Public Property assetID As String
    Private walletaddress As String
    Private nodeurl As String
    Private nodeport As Integer
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
