using System.Windows.Forms;
using System.Net;
using System.IO;

namespace GetPChomeStockTest
{
    public partial class Form1 : Form
    {
        private WebClient webclient = new WebClient();
        private MemoryStream memorystream = null;

        public Form1()
        {
            InitializeComponent();
        }
        private void GetValueTest(string StockID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            memorystream = new MemoryStream(webclient.DownloadData(@"https://stock.pchome.com.tw/stock/sid" + StockID + ".html"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(memorystream);

            HtmlAgilityPack.HtmlDocument docData = new HtmlAgilityPack.HtmlDocument();
            //Get Construction            
            docData.LoadHtml(doc.DocumentNode.SelectSingleNode(@"//div[@id='stock_info_data_a']").InnerHtml);
            //Get Content
            string Nprice = docData.DocumentNode.SelectSingleNode(@"/span[@class='data_close s-up']").InnerText;

            MessageBox.Show(Nprice);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            GetValueTest(textBox1.Text.Trim());
        }
    }
}
