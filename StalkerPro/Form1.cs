using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace StalkerPro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Debug(string message)
        {
            txtLog.AppendText(message + Environment.NewLine);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text;

            if (string.IsNullOrWhiteSpace(url))
            {
                Debug("Ange en giltig URL.");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Hämta HTML
                    string htmlContent = await client.GetStringAsync(url);
                    Debug("Lyckades hämta HTML");

                    // Ladda HTML i HtmlAgilityPack
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);
                    Debug("Lyckades ladda in HTML fil i dokument");

                    // Exempel: Hämta alla länkar
                    var links = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
                    if (links != null)
                    {
                        Debug("Hittade länkar");
                        lstResults.Items.Clear();
                        Debug("Rensade listan");
                        foreach (var link in links)
                        {
                            string href = link.Attributes["href"].Value;
                            lstResults.Items.Add(href);
                            Debug("Hittade en länk: " + href);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Inga länkar hittades.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Debug("Inga länkar hittades.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel inträffade: {ex.Message}", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug($"Ett fel inträffade: {ex.Message}");
            }
        }
    }
}
