using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace StalkerPro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Tömmer och lägger till kolumner. Notera att kolumnen "Förnamn" har tagits bort.
            dgvPeople.Columns.Clear();
            dgvPeople.Columns.Add("Personnamn", "Personnamn");
            dgvPeople.Columns.Add("Tilltalsnamn", "Tilltalsnamn");
            dgvPeople.Columns.Add("Efternamn", "Efternamn");
            dgvPeople.Columns.Add("Personnummer", "Personnummer");
            dgvPeople.Columns.Add("URL", "Profil Länk");
        }

        public void Debug(string message)
        {
            txtLog.AppendText(message + Environment.NewLine);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string location = txtLocation.Text.Trim();
            int minAge = (int)numMinAge.Value;
            int maxAge = (int)numMaxAge.Value;

            if (string.IsNullOrWhiteSpace(firstName))
            {
                Debug("Förnamn är obligatoriskt.");
                return;
            }

            try
            {
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                // Skapar en service för ChromeDriver med dolda kommandopromptfönster
                var service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                using (var driver = new ChromeDriver(service, options))
                {
                    string searchUrl = $"https://www.ratsit.se/sok/person?" +
                                       $"fnamn={Uri.EscapeDataString(firstName)}" +
                                       $"&enamn={Uri.EscapeDataString(lastName)}" +
                                       $"&kn={Uri.EscapeDataString(location)}" +
                                       $"&amin={minAge}" +
                                       $"&amax={maxAge}" +
                                       "&fon=1&page=1";

                    Debug($"Söker på URL: {searchUrl}");
                    driver.Navigate().GoToUrl(searchUrl);
                    await Task.Delay(3000);

                    string htmlContent = driver.PageSource;
                    Debug("Lyckades hämta HTML.");

                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);

                    var profileLinks = htmlDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'search-result-list')]//li//a[@href]");
                    if (profileLinks == null || profileLinks.Count == 0)
                    {
                        Debug($"Inga resultat för \"{firstName} {lastName}\".");
                        return;
                    }

                    dgvPeople.Rows.Clear();

                    foreach (var link in profileLinks)
                    {
                        string href = link.Attributes["href"].Value;
                        if (!href.StartsWith("http"))
                        {
                            href = "https://www.ratsit.se" + href;
                        }

                        if (href.Contains("bolagsfakta.se") || href.Contains("ratsit.se/kop/kassa"))
                            continue;

                        // Matcha personnumret (ex. 20071012) från URL:en
                        var match = Regex.Match(href, @"ratsit\.se\/(\d{8})-");
                        if (!match.Success) continue;

                        string personalNumber = match.Groups[1].Value;
                        int age = GetAge(personalNumber);
                        Debug($"Extraherade personnummer: {personalNumber}, Ålder: {age}");

                        if (age < minAge || age > maxAge) continue;

                        var details = ScrapePersonDetails(href);
                        if (details != null)
                        {
                            // Lägger till den extraherade personnumret istället för "Förnamn"
                            Debug($"Lägger till i DataGridView: {details.Personnamn}, {details.Tilltalsnamn}, {details.Efternamn}, {personalNumber}, {details.Url}");
                            dgvPeople.Rows.Add(details.Personnamn, details.Tilltalsnamn, details.Efternamn, personalNumber, details.Url);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel: {ex.Message}", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug($"Fel: {ex.Message}");
            }
        }

        private PersonDetails ScrapePersonDetails(string url)
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            using (var driver = new ChromeDriver(service, options))
            {
                driver.Navigate().GoToUrl(url);
                System.Threading.Thread.Sleep(3000);
                string html = driver.PageSource;

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                Debug("HTML-snutt: " + (html.Length > 300 ? html.Substring(0, 300) : html));

                string personnamn = ExtractValue(doc, "//p[span[contains(text(),'Personnamn:')]]", "Personnamn:");
                // Tar bort förnamnsextraktion
                // string fornamn = ExtractValue(doc, "//p[span[contains(text(),'Förnamn:')]]", "Förnamn:");
                string tilltalsnamn = ExtractValue(doc, "//p[span[contains(text(),'Tilltalsnamn:')]]", "Tilltalsnamn:");
                string efternamn = ExtractValue(doc, "//p[span[contains(text(),'Efternamn:')]]", "Efternamn:");
                // Mellannamn används inte längre

                return new PersonDetails
                {
                    Personnamn = personnamn,
                    // Förnamn tas inte med
                    Tilltalsnamn = tilltalsnamn,
                    Efternamn = efternamn,
                    Url = url
                };
            }
        }

        private string ExtractValue(HtmlDocument doc, string xPath, string labelToRemove)
        {
            var node = doc.DocumentNode.SelectSingleNode(xPath);
            return node == null ? "" : node.InnerText.Replace(labelToRemove, "").Trim();
        }

        private int GetAge(string personalNumber)
        {
            if (personalNumber.Length < 8) return 0;

            int year = int.Parse(personalNumber.Substring(0, 4));
            int month = int.Parse(personalNumber.Substring(4, 2));
            int day = int.Parse(personalNumber.Substring(6, 2));

            DateTime birthDate = new DateTime(year, month, day);
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
    }

    public class PersonDetails
    {
        public string Personnamn { get; set; }
        // Förnamn-egenskapen har tagits bort
        public string Tilltalsnamn { get; set; }
        public string Efternamn { get; set; }
        // Mellannamn används inte längre, vi använder personnummer istället
        public string Url { get; set; }
    }
}
