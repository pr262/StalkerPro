using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
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
        private CancellationTokenSource _cts;

        public Form1()
        {
            InitializeComponent();
            dgvPeople.Columns.Clear();
            dgvPeople.Columns.Add("Personnamn", "Personnamn");
            dgvPeople.Columns.Add("Tilltalsnamn", "Tilltalsnamn");
            dgvPeople.Columns.Add("Efternamn", "Efternamn");
            dgvPeople.Columns.Add("Personnummer", "Personnummer");
            dgvPeople.Columns.Add("URL", "Profil Länk");
        }

        public void Debug(string message)
        {
            if (txtLog.InvokeRequired)
                txtLog.Invoke((Action)(() => txtLog.AppendText(message + Environment.NewLine)));
            else
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

            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            btnSearch.Enabled = false;
            dgvPeople.Rows.Clear();

            // Start indeterminate progress
            progressBar.Invoke((Action)(() =>
            {
                progressBar.MarqueeAnimationSpeed = 30;
                progressBar.Visible = true;
            }));

            var progress = new Progress<string>(Debug);

            try
            {
                var people = await Task.Run(() => SearchPeople(firstName, lastName, location, minAge, maxAge, progress, token), token);

                foreach (var p in people)
                {
                    if (token.IsCancellationRequested) break;
                    dgvPeople.Invoke((Action)(() =>
                        dgvPeople.Rows.Add(p.Personnamn, p.Tilltalsnamn, p.Efternamn, p.Personnummer, p.Url)));
                }
            }
            catch (OperationCanceledException)
            {
                Debug("Sökning avbröts av användaren.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel: {ex.Message}", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug($"Fel: {ex.Message}");
            }
            finally
            {
                // Stop progress
                progressBar.Invoke((Action)(() =>
                {
                    progressBar.MarqueeAnimationSpeed = 0;
                    progressBar.Visible = false;
                }));
                btnSearch.Enabled = true;
            }
        }

        private List<PersonDetails> SearchPeople(
            string firstName,
            string lastName,
            string location,
            int minAge,
            int maxAge,
            IProgress<string> progress,
            CancellationToken token)
        {
            var results = new List<PersonDetails>();

            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            using (var driver = new ChromeDriver(service, options))
            {
                string searchUrl = $"https://www.ratsit.se/sok/person?fnamn={Uri.EscapeDataString(firstName)}&enamn={Uri.EscapeDataString(lastName)}&kn={Uri.EscapeDataString(location)}&amin={minAge}&amax={maxAge}&fon=1&page=1";
                progress.Report($"Söker på URL: {searchUrl}");
                driver.Navigate().GoToUrl(searchUrl);
                Thread.Sleep(3000);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(driver.PageSource);

                var profileLinks = htmlDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'search-result-list')]//li//a[@href]");
                if (profileLinks == null) return results;

                foreach (var link in profileLinks)
                {
                    token.ThrowIfCancellationRequested();

                    string href = link.Attributes["href"].Value;
                    if (!href.StartsWith("http")) href = "https://www.ratsit.se" + href;
                    if (href.Contains("bolagsfakta.se") || href.Contains("ratsit.se/kop/kassa")) continue;

                    var match = Regex.Match(href, @"ratsit\.se\/(\d{8})-");
                    if (!match.Success) continue;

                    string personalNumber = match.Groups[1].Value;
                    int age = GetAge(personalNumber);
                    progress.Report($"{personalNumber} -> Ålder: {age}");

                    if (age < minAge || age > maxAge) continue;

                    var details = ScrapePersonDetails(href, progress, token);
                    if (details != null)
                    {
                        details.Personnummer = personalNumber;
                        results.Add(details);
                        progress.Report($"Tillagd: {details.Tilltalsnamn} {details.Efternamn}");
                    }
                }
            }

            return results;
        }


        private PersonDetails ScrapePersonDetails(string url, IProgress<string> progress, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            using (var driver = new ChromeDriver(service, options))
            {
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(3000);

                var doc = new HtmlDocument();
                doc.LoadHtml(driver.PageSource);

                progress.Report("Parsing profil: " + url);
                string personnamn = ExtractValue(doc, "//p[span[contains(text(),'Personnamn:')]]", "Personnamn:");
                string tilltalsnamn = ExtractValue(doc, "//p[span[contains(text(),'Tilltalsnamn:')]]", "Tilltalsnamn:");
                string efternamn = ExtractValue(doc, "//p[span[contains(text(),'Efternamn:')]]", "Efternamn:");

                return new PersonDetails
                {
                    Personnamn = personnamn,
                    Tilltalsnamn = tilltalsnamn,
                    Efternamn = efternamn,
                    Url = url
                };
            }
        }

        private string ExtractValue(HtmlDocument doc, string xPath, string labelToRemove)
        {
            var node = doc.DocumentNode.SelectSingleNode(xPath);
            return node == null ? string.Empty : node.InnerText.Replace(labelToRemove, string.Empty).Trim();
        }

        private int GetAge(string personalNumber)
        {
            if (personalNumber.Length < 8) return 0;
            int year = int.Parse(personalNumber.Substring(0, 4));
            int month = int.Parse(personalNumber.Substring(4, 2));
            int day = int.Parse(personalNumber.Substring(6, 2));
            var birthDate = new DateTime(year, month, day);
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
    }

    public class PersonDetails
    {
        public string Personnamn { get; set; }
        public string Tilltalsnamn { get; set; }
        public string Efternamn { get; set; }
        public string Personnummer { get; set; }
        public string Url { get; set; }
    }
}
