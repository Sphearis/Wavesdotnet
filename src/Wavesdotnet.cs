using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Data;
using System.IO;
using Chaos.NaCl;

namespace Wavesdotnet
{

    public class Node
    {
        public static string nodeurl { get; set; }
        public static string noderpcport { get; set; }
        public static block latestblock { get; set; }

        public class AssetList
        {
            public string address { get; set; }
            public List<Balance> balances { get; set; }
        }

        public class Balance
        {
            public string assetId { get; set; }
            public long balance { get; set; }
            public bool reissuable { get; set; }
            public long quantity { get; set; }
            public Issuetransaction issueTransaction { get; set; }
        }

        public class Issuetransaction
        {
            public int type { get; set; }
            public string id { get; set; }
            public string sender { get; set; }
            public string senderPublicKey { get; set; }
            public string assetId { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public long quantity { get; set; }
            public int decimals { get; set; }
            public bool reissuable { get; set; }
            public int fee { get; set; }
            public long timestamp { get; set; }
            public string signature { get; set; }
        }

        public class block
        {
            public int height { get; set; }
        }
        public class assetbalance
        {
            public string address { get; set; }
            public string assetId { get; set; }
            public long balance { get; set; }
        }

        public class wavesbalance
        {
            public string address { get; set; }
            public int confirmations { get; set; }
            public double balance { get; set; }
        }

        public class Transaction
        {
            public int type { get; set; }
            public string id { get; set; }
            public string sender { get; set; }
            public string senderPublicKey { get; set; }
            public string recipient { get; set; }
            public string assetId { get; set; }
            public string name { get; set; }
            public int amount { get; set; }
            public object feeAsset { get; set; }
            public int fee { get; set; }
            public long timestamp { get; set; }
            public string attachment { get; set; }
            public string signature { get; set; }
            public int height { get; set; }
        }

        public Node()
        {
            nodeurl = "localhost";
            noderpcport = "6869";
        }
        public Node(string urlset, string rpcportset)
        {
            nodeurl = urlset;
            noderpcport = rpcportset;
        }
        public string ConnectionCheck()
        {
            string cinfo = "Connected to Node URL:" + nodeurl + " Port:" + noderpcport;

            return cinfo;

        }
        public string GetWavesBalance()
        {
            string error = "ERROR No parameters entered. Usage: GetWavesBalance(\"walletaddress\")";
            return error;
        }
        public double GetWavesBalance(string walletaddress)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/addresses/balance/" + walletaddress);
            wavesbalance GetWavesBalance = JsonConvert.DeserializeObject<wavesbalance>(json);
            double gotbalance = GetWavesBalance.balance / 100000000;
            return gotbalance;
        }
        public string GetAssetBalance()
        {
            string error = "ERROR No parameters entered. Usage: GetAssetBalance(\"walletaddress\",\"assetID\", decimals)";
            return error;
        }
        public double GetAssetBalance(string walletaddress, string assetID, int decimals)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/assets/balance/" + walletaddress + "/" + assetID);
            assetbalance GetAssetBalance = JsonConvert.DeserializeObject<assetbalance>(json);
            double gotbalance = GetAssetBalance.balance / Math.Pow(10, decimals);
            return gotbalance;
        }
        public string GetAssetName()
        {
            string error = "ERROR No parameters entered. Usage: GetAssetName(\"assetID\")";
            return error;
        }
        public string GetAssetName(string assetID)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/transactions/info/" + assetID);
            Transaction Getassettx = JsonConvert.DeserializeObject<Transaction>(json);
            return Getassettx.name;
        }
        public dynamic GetAssetList(string walletaddress)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/assets/balance/" + walletaddress);
            dynamic datalist = JsonConvert.DeserializeObject<AssetList>(json);
            return datalist;
        }

        public object TxInfo(string txID)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/transactions/info/" + txID);
            Transaction txinfo = JsonConvert.DeserializeObject<Transaction>(json);
            return txinfo;
        }
        public Transaction SendWaves(string sender, string recipient, long amount, string attachment, string apikey)
        {
            Uri getnode = new Uri("http://" + nodeurl + ":" + noderpcport + "/assets/transfer");
            string resp = "{\"sender\": \"" + sender + "\", \"recipient\": \"" + recipient + "\", \"fee\": 100000, \"amount\": " + amount + ", \"attachment\": \""+ base58encode(attachment) + "\" }";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getnode);
            request.Method = "POST";
            request.ContentType = "text/plain;charset=utf-8";
            request.Headers.Add("api_key", apikey);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(resp);
            request.ContentLength = bytes.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                WebResponse response = request.GetResponse();
            }
            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());
            string POSTResp = sr.ReadToEnd();
            Transaction transactionreport = JsonConvert.DeserializeObject<Transaction>(POSTResp);
            return transactionreport;
        }

        public Transaction SendAsset(string assetID, string sender, string recipient, long amount, string attachment, string apikey)
        {
            Uri getnode = new Uri("http://" + nodeurl + ":" + noderpcport + "/assets/transfer");
            string resp = "{\"assetId\": \"" + assetID + "\", \"sender\": \"" + sender + "\", \"recipient\": \"" + recipient + "\", \"fee\": 100000, \"amount\": " + amount + ", \"attachment\": \"" + base58encode(attachment) + "\" }";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getnode);
            request.Method = "POST";
            request.ContentType = "text/plain;charset=utf-8";
            request.Headers.Add("api_key", apikey);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(resp);
            request.ContentLength = bytes.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                WebResponse response = request.GetResponse();
            }
            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());
            string POSTResp = sr.ReadToEnd();
            Transaction transactionreport = JsonConvert.DeserializeObject<Transaction>(POSTResp);
            return transactionreport;
        }
        public string TxInfo()
        {
            string error = "ERROR No parameters entered. Usage: TxInfo(\"txID\")";
            return error;
        }
        public int Getblockheight()
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/blocks/height");
            block blockinfo = JsonConvert.DeserializeObject<block>(json);
            return blockinfo.height;
        }
        public async Task Getlatestblock()
        {
            using (var client = new HttpClient())
            {
                //HTTP get
                HttpResponseMessage response = await client.GetAsync("https://" + nodeurl + ":" + noderpcport + "/blocks/height");
                response.EnsureSuccessStatusCode();
                var jsonString = "";
                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync().Result;

                }
                else
                {
                    jsonString = "{ \"height\":371180}";
                }
                latestblock = JsonConvert.DeserializeObject<block>(jsonString);
            }
        }
        public Transaction PublishAssetTransaction(string assetID, string senderPublicKey, string recipient, double amount, string attachment, string privkey)
        {
            Byte[] Bytedata = new Byte[152];
            Bytedata = Encoding.Unicode.GetBytes("4" + senderPublicKey + "0" + assetID + "0" + DateTimeOffset.Now.ToString() + amount + "100000" + recipient + attachment.Length + attachment);
            Byte[] expandedkey = Ed25519.ExpandedPrivateKeyFromSeed(Encoding.Unicode.GetBytes(privkey));
            Ed25519.Sign(Bytedata, expandedkey);
            Uri getnode = new Uri("http://" + nodeurl + ":" + noderpcport + "/assets/broadcast/transfer");
            string resp = "{\"assetId\": \"" + assetID + "\", \"senderPublicKey\": \"" + senderPublicKey + "\", \"recipient\": \"" + recipient + "\", \"amount\": " + amount + ", \"fee\": 100000, \"attachment\": \"" + base58encode(attachment) + "\", \"signature\":  \"" + Encoding.Unicode.GetString(Bytedata) + "\" }";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getnode);
            request.Method = "POST";
            request.ContentType = "text/plain;charset=utf-8";
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(resp);
            request.ContentLength = bytes.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                WebResponse response = request.GetResponse();
            }
            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());
            string POSTResp = sr.ReadToEnd();
            Transaction transactionreport = JsonConvert.DeserializeObject<Transaction>(POSTResp);
            return transactionreport;

        }
        public string base58encode(string toencode)
        {
            return toencode;
        }
        public Transaction PublishWavesTransaction(string senderPublicKey, string recipient, double amount, string attachment, string privkey)
        {
            Byte[] Bytedata = new Byte[119];
            Bytedata = Encoding.Unicode.GetBytes("4" + senderPublicKey + "0" + "0" + DateTimeOffset.Now.ToString() + amount + "100000" + recipient + attachment.Length + attachment);
            Byte[] expandedkey = Ed25519.ExpandedPrivateKeyFromSeed(Encoding.Unicode.GetBytes(privkey));
            Ed25519.Sign(Bytedata, expandedkey);
            Uri getnode = new Uri("http://" + nodeurl + ":" + noderpcport + "/assets/broadcast/transfer");
            string resp = "{\"senderPublicKey\": \"" + senderPublicKey + "\", \"recipient\": \"" + recipient + "\", \"amount\": " + amount + ", \"fee\": 100000, \"attachment\": \"" + base58encode(attachment) + "\", \"signature\":  \"" + Encoding.Unicode.GetString(Bytedata) + "\" }";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getnode);
            request.Method = "POST";
            request.ContentType = "text/plain;charset=utf-8";
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(resp);
            request.ContentLength = bytes.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                WebResponse response = request.GetResponse();
            }
            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());
            string POSTResp = sr.ReadToEnd();
            Transaction transactionreport = JsonConvert.DeserializeObject<Transaction>(POSTResp);
            return transactionreport;

        }

    }
}
