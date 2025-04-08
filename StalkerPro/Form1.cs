using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace StalkerPro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Set up DataGridView columns.
            dgvPeople.Columns.Clear();
            dgvPeople.Columns.Add("Personnamn", "Personnamn");
            dgvPeople.Columns.Add("Tilltalsnamn", "Tilltalsnamn");
            dgvPeople.Columns.Add("Efternamn", "Efternamn");
            dgvPeople.Columns.Add("Personnummer", "Personnummer");
            dgvPeople.Columns.Add("URL", "Profil Länk");

            // Initialize the progress bar.
            progressBar.Visible = false;
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
        }

        // Thread-safe Debug method.
        public void Debug(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Debug(message)));
                return;
            }
            txtLog.AppendText(message + Environment.NewLine);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
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

            progressBar.Visible = true;
            progressBar.Value = 0;

            try
            {
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                var service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                using (var driver = new ChromeDriver(service, options))
                {
                    // STEP 1: Navigate to search URL.
                    string searchUrl = $"https://www.ratsit.se/sok/person?" +
                                       $"fnamn={Uri.EscapeDataString(firstName)}" +
                                       $"&enamn={Uri.EscapeDataString(lastName)}" +
                                       $"&kn={Uri.EscapeDataString(location)}" +
                                       $"&amin={minAge}" +
                                       $"&amax={maxAge}" +
                                       "&fon=1&page=1";

                    Debug($"Söker på URL: {searchUrl}");
                    driver.Navigate().GoToUrl(searchUrl);
                    await Task.Delay(3000);  // Wait for page load.
                    progressBar.Value = 10; // Update to 10% after navigation.

                    // STEP 2: Retrieve and parse HTML.
                    string htmlContent = driver.PageSource;
                    Debug("Lyckades hämta HTML.");
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);
                    progressBar.Value = 20; // Update to 20% after parsing HTML.

                    var profileLinks = htmlDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'search-result-list')]//li//a[@href]");
                    if (profileLinks == null || profileLinks.Count == 0)
                    {
                        Debug($"Inga resultat för \"{firstName} {lastName}\".");
                        progressBar.Value = 100;
                        return;
                    }
                    dgvPeople.Rows.Clear();

                    // STEP 3: Process profile links.
                    int totalLinks = profileLinks.Count;
                    // Allocate progress from 20% (done with initialization) to 90% for processing all links.
                    double progressPerLink = totalLinks > 0 ? (70.0 / totalLinks) : 0;
                    int linkIndex = 0;

                    foreach (var link in profileLinks)
                    {
                        PersonDetails details = await Task.Run(() => ProcessProfileLink(link, minAge, maxAge));
                        linkIndex++;
                        // Calculate new progress value.
                        int progressValue = 20 + (int)(progressPerLink * linkIndex);
                        if (progressValue > 90)
                            progressValue = 90;
                        progressBar.Value = progressValue;

                        if (details != null)
                        {
                            // Use Invoke to safely update the DataGridView on the UI thread.
                            this.Invoke(new Action(() =>
                            {
                                dgvPeople.Rows.Add(details.Personnamn, details.Tilltalsnamn, details.Efternamn, details.Personnummer, details.Url);
                            }));
                            Debug($"Lägger till i DataGridView: {details.Personnamn}, {details.Tilltalsnamn}, {details.Efternamn}, {details.Personnummer}, {details.Url}");
                        }
                        await Task.Yield(); // Let the UI update.
                    }
                    // STEP 4: Finalizing.
                    progressBar.Value = 100;
                    await Task.Delay(500);
                    progressBar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel: {ex.Message}", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug($"Fel: {ex.Message}");
            }
        }

        /// <summary>
        /// Processes a profile link on a background thread.
        /// </summary>
        private PersonDetails ProcessProfileLink(HtmlAgilityPack.HtmlNode link, int minAge, int maxAge)
        {
            try
            {
                string href = link.Attributes["href"].Value;
                if (!href.StartsWith("http"))
                    href = "https://www.ratsit.se" + href;
                if (href.Contains("bolagsfakta.se") || href.Contains("ratsit.se/kop/kassa"))
                    return null;

                // Extract the personal number (e.g., 20071012) from the URL.
                var match = Regex.Match(href, @"ratsit\.se\/(\d{8})-");
                if (!match.Success) return null;
                string personalNumber = match.Groups[1].Value;
                int age = GetAge(personalNumber);
                Debug($"Extraherade personnummer: {personalNumber}, Ålder: {age}");
                if (age < minAge || age > maxAge)
                    return null;

                PersonDetails details = ScrapePersonDetails(href);
                if (details != null)
                    details.Personnummer = personalNumber;

                return details;
            }
            catch (Exception ex)
            {
                Debug("Fel vid bearbetning av länk: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Scrapes person details from the given URL using Selenium.
        /// </summary>
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
                System.Threading.Thread.Sleep(3000);  // Wait for page load.
                string html = driver.PageSource;

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                Debug("HTML-snutt: " + (html.Length > 300 ? html.Substring(0, 300) : html));

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
            return node == null ? "" : node.InnerText.Replace(labelToRemove, "").Trim();
        }

        private int GetAge(string personalNumber)
        {
            if (personalNumber.Length < 8)
                return 0;
            int year = int.Parse(personalNumber.Substring(0, 4));
            int month = int.Parse(personalNumber.Substring(4, 2));
            int day = int.Parse(personalNumber.Substring(6, 2));
            DateTime birthDate = new DateTime(year, month, day);
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age))
                age--;
            return age;
        }
    }

    public class PersonDetails
    {
        public string Personnamn { get; set; }
        public string Tilltalsnamn { get; set; }
        public string Efternamn { get; set; }
        public string Url { get; set; }
        public string Personnummer { get; set; }
    }
}
