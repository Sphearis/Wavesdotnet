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
using System.Security.Cryptography;

namespace Wavesdotnet
{

    public class Node
    {
        public static string nodeurl { get; set; }
        public static string noderpcport { get; set; }
        public static string backupnodeurl { get; set; }
        public static string backupnoderpcport { get; set; }
        public static Block latestblock { get; set; }

        public class Nodeadresses
        {
            public string address { get; set; }
        }
        public class Nxtconsensus
        {
            public int basetarget { get; set; }
            public string generationsignature { get; set; }
        }


        public class Block
        {
            public int version { get; set; }
            public long timestamp { get; set; }
            public string reference { get; set; }
            public Nxtconsensus nxtconsensus { get; set; }
            public List<Transaction> transactions { get; set; }
            public string generator { get; set; }
            public string signature { get; set; }
            public int fee { get; set; }
            public int blocksize { get; set; }
            public int height { get; set; }

        }

        public class AssetList
        {
            public string address { get; set; }
            public List<Balance> balances { get; set; }
        }
        public class newaddress
        {
        public string address { get; set; }
    }

        public class Balance
        {
            public string assetId { get; set; }
            public long balance { get; set; }
            public bool reissuable { get; set; }
            public long quantity { get; set; }
            public Transaction issueTransaction { get; set; }
        }
        public class Result
        {
            public string MarketName { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double Volume { get; set; }
            public string Last { get; set; }
            public double BaseVolume { get; set; }
            public string TimeStamp { get; set; }
            public double Bid { get; set; }
            public double Ask { get; set; }
            public int OpenBuyOrders { get; set; }
            public int OpenSellOrders { get; set; }
            public double PrevDay { get; set; }
            public string Created { get; set; }
            public object DisplayMarketName { get; set; }
        }

        public class Bittrexprice
        {
            public bool success { get; set; }
            public string message { get; set; }
            public List<Result> result { get; set; }
        }

        public class nodestatus
        {
            public string blockGeneratorStatus { get; set; }
            public string historySynchronizationStatus { get; set; }
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
            public string description { get; set; }
            public long amount { get; set; }
            public object feeAsset { get; set; }
            public int fee { get; set; }
            public long quantity { get; set; }
            public string leaseId { get; set; }
            public Boolean reissuable { get; set; }
            public int decimals { get; set; }
            public long timestamp { get; set; }
            public string attachment { get; set; }
            public string signature { get; set; }
            public int height { get; set; }
            public Order1 order1 { get; set; }
            public Order2 order2 { get; set; }
            public long price { get; set; }
        }
        public class AssetPair
        {
            public string amountAsset { get; set; }
            public string priceAsset { get; set; }
        }

        public class Order1
        {
            public string id { get; set; }
            public string senderPublicKey { get; set; }
            public string matcherPublicKey { get; set; }
            public AssetPair assetPair { get; set; }
            public string orderType { get; set; }
            public long price { get; set; }
            public long amount { get; set; }
            public long timestamp { get; set; }
            public long expiration { get; set; }
            public int matcherFee { get; set; }
            public string signature { get; set; }
        }

        public class AssetPair2
        {
            public string amountAsset { get; set; }
            public string priceAsset { get; set; }
        }
        public class Generatingbalance
        {
            public string address { get; set; }
            public int confirmations { get; set; }
            public long balance { get; set; }
        }
        public class Order2
        {
            public string id { get; set; }
            public string senderPublicKey { get; set; }
            public string matcherPublicKey { get; set; }
            public AssetPair2 assetPair { get; set; }
            public string orderType { get; set; }
            public long price { get; set; }
            public long amount { get; set; }
            public long timestamp { get; set; }
            public long expiration { get; set; }
            public int matcherFee { get; set; }
            public string signature { get; set; }
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
            backupnodeurl = "nodes.wavesnodes.com";
            backupnoderpcport = "80";
        }
        public Node(string urlset, string rpcportset, string backupurlset, string backuprpcportset)
        {
            nodeurl = urlset;
            noderpcport = rpcportset;
            backupnodeurl = backupurlset;
            backupnoderpcport = backuprpcportset;
        }

        public string ConnectionCheck()
        {
            nodestatus mainnodestatus;
            nodestatus backupnodestatus;
            string statusmain;
            string statusbackup;
            try
            {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/node/status");
            mainnodestatus = JsonConvert.DeserializeObject<nodestatus>(json);
                statusmain = mainnodestatus.blockGeneratorStatus;
    }
            catch
            {
                statusmain = "offline";
            }
            try
            {
                WebClient wc2 = new WebClient();
                var json2 = wc2.DownloadString("http://" + backupnodeurl + ":" + backupnoderpcport + "/node/status");
                backupnodestatus = JsonConvert.DeserializeObject<nodestatus>(json2);
                statusbackup = backupnodestatus.blockGeneratorStatus;
            }
            catch
            {
                statusbackup = "offline";
            }
            string cinfo = "Connection to Node URL:" + nodeurl + " Port:" + noderpcport + " " + statusmain;
            cinfo= cinfo+ " ,Connected to BackupNode URL:" + backupnodeurl + " Port:" + backupnoderpcport + " " + statusbackup;
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
            wavesbalance GetWavesBalance;
            try
            {
                var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/addresses/balance/" + walletaddress);
                GetWavesBalance = JsonConvert.DeserializeObject<wavesbalance>(json);
            }
            catch
            {
                var jsonbck = wc.DownloadString("http://" + backupnodeurl + ":" + backupnoderpcport + "/addresses/balance/" + walletaddress);
                GetWavesBalance = JsonConvert.DeserializeObject<wavesbalance>(jsonbck);
            }

            double gotbalance = GetWavesBalance.balance / 100000000;
            return gotbalance;
        }
        public double GetGeneratingBalance(string walletaddress)
        {
            WebClient wc = new WebClient();
            Generatingbalance GetWavesBalance;
            try
            {
                var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/addresses/effectiveBalance/" + walletaddress);
                GetWavesBalance = JsonConvert.DeserializeObject<Generatingbalance>(json);
            }
            catch
            {
                var jsonbck = wc.DownloadString("http://" + backupnodeurl + ":" + backupnoderpcport + "/addresses/effectiveBalance/" + walletaddress);
                GetWavesBalance = JsonConvert.DeserializeObject<Generatingbalance>(jsonbck);
            }

            double gotbalance = GetWavesBalance.balance / 100000000;
            return gotbalance;
        }
        public Dictionary<string, double> GetDistribution(string assetID)
        {
            WebClient wc = new WebClient();
            Dictionary<string, double> values;
            try
            {
                var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/assets/"+ assetID + "/distribution");
                values = JsonConvert.DeserializeObject<Dictionary<string, double>>(json);
            }
            catch
            {
                var jsonbck = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/assets/" + assetID + "/distribution");
                values = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonbck);
            }
            return values;
        }
        public string GetAssetBalance()
        {
            string error = "ERROR No parameters entered. Usage: GetAssetBalance(\"walletaddress\",\"assetID\", decimals)";
            return error;
        }
        public double GetAssetBalance(string walletaddress, string assetID, int decimals)
        {
            WebClient wc = new WebClient();
            assetbalance GetAssetBalance;
            try
            {
                var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/assets/balance/" + walletaddress + "/" + assetID);
                GetAssetBalance = JsonConvert.DeserializeObject<assetbalance>(json);
            }
            catch
            {
                var jsonbck = wc.DownloadString("http://" + backupnodeurl + ":" + backupnoderpcport + "/assets/balance/" + walletaddress + "/" + assetID);
                GetAssetBalance = JsonConvert.DeserializeObject<assetbalance>(jsonbck);
            }
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
            Transaction Getassettx;
            try
            {
                var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/transactions/info/" + assetID);
                Getassettx = JsonConvert.DeserializeObject<Transaction>(json);
            }
            catch
            {
                var jsonbck = wc.DownloadString("http://" + backupnodeurl + ":" + backupnoderpcport + "/transactions/info/" + assetID);
                Getassettx = JsonConvert.DeserializeObject<Transaction>(jsonbck);
            }
            return Getassettx.name;
        }
        public dynamic GetBlockhistory(int startingblock, int endingblock)
        {
            WebClient wc = new WebClient();
            dynamic bhistory;
            try
            {
                var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/blocks/seq/" + startingblock + "/" + endingblock);
                bhistory = JsonConvert.DeserializeObject<List<Block>>(json);
            }
            catch
            {
                var jsonbck = wc.DownloadString("http://" + backupnodeurl + ":" + backupnoderpcport + "/blocks/seq/" + startingblock + "/" + endingblock);
                bhistory = JsonConvert.DeserializeObject<List<Block>>(jsonbck);
            }
            return bhistory;
        }
        public dynamic GetBlocktxlist(int blocknbr)
        {
            WebClient wc = new WebClient();
            dynamic btxlist;
            try
            {
                var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/blocks/at/" + blocknbr);
                btxlist = JsonConvert.DeserializeObject<Block>(json);
            }
            catch
            {
                var jsonbck = wc.DownloadString("http://" + backupnodeurl + ":" + backupnoderpcport + "/blocks/at/" + blocknbr);
                btxlist = JsonConvert.DeserializeObject<Block>(jsonbck);
            }
            return btxlist.transactions;
        }
        public string GetBittrexBTCPrice(string coinname)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("https://bittrex.com/api/v1.1/public/getmarketsummary?market=btc-" + coinname);
            Result Getbtcprice = JsonConvert.DeserializeObject<Result>(json);
            return Getbtcprice.Last;
        }
        public string GetBittrexDollarPrice(string coinname)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("https://bittrex.com/api/v1.1/public/getmarketsummary?market=btc-" + coinname);
            Result Getbtcprice = JsonConvert.DeserializeObject<Result>(json);
            WebClient wc2 = new WebClient();
            var json2 = wc2.DownloadString("https://bittrex.com/api/v1.1/public/getmarketsummary?market=usd-btc");
            Result Getusdprice = JsonConvert.DeserializeObject<Result>(json2);
            Decimal Conversion = Convert.ToDecimal(Getbtcprice.Last) * Convert.ToDecimal(Getusdprice.Last);
            return Conversion.ToString();
        }
        public Transaction GetAssetInfos(string assetID)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/transactions/info/" + assetID);
            Transaction Getassettx = JsonConvert.DeserializeObject<Transaction>(json);
            return Getassettx;
        }
        public dynamic GetAssetList(string walletaddress)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/assets/balance/" + walletaddress);
            dynamic datalist = JsonConvert.DeserializeObject<AssetList>(json);
            return datalist;
        }
        public int GetLowTransactioncount(string walletaddress)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/transactions/address/" + walletaddress+ "/limit/10");
            dynamic datalist = JsonConvert.DeserializeObject<Transaction>(json);
            return datalist.count();
        }

        public object TxInfo(string txID)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/transactions/info/" + txID);
            Transaction txinfo = JsonConvert.DeserializeObject<Transaction>(json);
            return txinfo;
        }
        public nodestatus Getnodestatus()
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/node/status");
            nodestatus status = JsonConvert.DeserializeObject<nodestatus>(json);
            return status;
        }
        public Transaction SendWaves(string sender, string recipient, long amount, string attachment, string apikey)
        {
            Uri getnode = new Uri("http://" + nodeurl + ":" + noderpcport + "/assets/transfer");
            string resp = "{\"sender\": \"" + sender + "\", \"recipient\": \"" + recipient + "\", \"fee\": 100000, \"amount\": " + amount + ", \"attachment\": \""+ base58encode(attachment) + "\" }";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getnode);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("api_key", apikey);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(resp);
            request.ContentLength = bytes.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                WebResponse response = request.GetResponse();
            }
            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());
            string POSTResp = sr.ReadToEnd();
            Transaction transactionreport = JsonConvert.DeserializeObject<Transaction>(POSTResp);
            return transactionreport;
        }
        public string CreateAddress(string apikey)
        {
            Uri getnode = new Uri("http://" + nodeurl + ":" + noderpcport + "/addresses");
            string resp = "{\"attachment\": \"faucet\" }";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getnode);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("api_key", apikey);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(resp);
            request.ContentLength = bytes.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                WebResponse response = request.GetResponse();
            }
            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());
            string POSTResp = sr.ReadToEnd();
            newaddress newaddress = JsonConvert.DeserializeObject<newaddress>(POSTResp);
            return newaddress.address;
        }

        public Transaction SendAsset(string assetID, string sender, string recipient, long amount, string attachment, string apikey)
        {
            Uri getnode = new Uri("http://" + nodeurl + ":" + noderpcport + "/assets/transfer");
            string resp = "{\"assetId\": \"" + assetID + "\", \"sender\": \"" + sender + "\", \"recipient\": \"" + recipient + "\", \"fee\": 100000, \"amount\": " + amount + ", \"attachment\": \"" + base58encode(attachment) + "\" }";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getnode);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("api_key", apikey);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(resp);
            request.ContentLength = bytes.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
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
            Block blockinfo = JsonConvert.DeserializeObject<Block>(json);
            return blockinfo.height;
        }
        public object Getnodeaddresses()
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/addresses");
            List<string> Nodeaddresses = JsonConvert.DeserializeObject<List<string>>(json);
            return Nodeaddresses;
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
                latestblock = JsonConvert.DeserializeObject<Block>(jsonString);
            }
        }
        public Transaction PublishAssetTransaction(string assetID, string senderPublicKey, string recipient, double amount, string attachment, string privkey)
        {
            
            Byte[] Bytedata = new Byte[152];
            Bytedata = Encoding.Unicode.GetBytes("4" + senderPublicKey + "0" + assetID + "0" + DateTimeOffset.Now.ToString() + amount + "100000" + recipient + attachment.Length + attachment);
             var b = new byte[32];
            var privatekeySeed = new RNGCryptoServiceProvider();
            privatekeySeed.GetBytes(b);
            Byte[] expandedkey = Ed25519.ExpandedPrivateKeyFromSeed(Encoding.Unicode.GetBytes(privkey));
                        Ed25519.Sign(Bytedata, expandedkey);
            Uri getnode = new Uri("http://" + nodeurl + ":" + noderpcport + "/assets/broadcast/transfer");
            string resp = "{\"assetId\": \"" + assetID + "\", \"senderPublicKey\": \"" + senderPublicKey + "\", \"recipient\": \"" + recipient + "\", \"amount\": " + amount + ", \"fee\": 100000, \"attachment\": \"" + base58encode(attachment) + "\", \"signature\":  \"" + Encoding.Unicode.GetString(Bytedata) + "\" }";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getnode);
            request.Method = "POST";
            request.ContentType = "Content-Type: application/json";
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
            request.ContentType = "Content-Type: application/json";
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
