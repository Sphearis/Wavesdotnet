using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace Wavesdotnet
{

    public class Node
    {
        public static string nodeurl { get; set; }
        public static string noderpcport { get; set; }

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
        public object TxInfo(string txID)
        {
            WebClient wc = new WebClient();
            var json = wc.DownloadString("http://" + nodeurl + ":" + noderpcport + "/transactions/info/" + txID);
            Transaction txinfo = JsonConvert.DeserializeObject<Transaction>(json);
            return txinfo;
        }
        public string TxInfo()
        {
            string error = "ERROR No parameters entered. Usage: TxInfo(\"txID\")";
            return error;
        }
    }

}
