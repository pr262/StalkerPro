using System;
using System.Net.Http;
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

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                Debug("F�rnamn och efternamn �r obligatoriska.");
                return;
            }

            try
            {
                var options = new ChromeOptions();
                options.AddArgument("--headless"); // K�r utan GUI
                options.AddArgument("--disable-gpu");
                options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                using (var driver = new ChromeDriver(options))
                {
                    // Build the URL dynamically, including empty parameters
                    string searchUrl = $"https://www.ratsit.se/sok/person?" +
                                       $"fnamn={Uri.EscapeDataString(firstName)}" +
                                       $"&enamn={Uri.EscapeDataString(lastName)}" +
                                       $"&gata=" + // Empty parameter
                                       $"&postnr=" + // Empty parameter
                                       $"&ort=" + // Empty parameter
                                       $"&kn={Uri.EscapeDataString(location)}" +
                                       $"&pnr=" + // Empty parameter
                                       $"&tfn=" + // Empty parameter
                                       $"&m=0" +  // Default parameter
                                       $"&k=0" +  // Default parameter
                                       $"&r=0" +  // Default parameter
                                       $"&er=0" + // Default parameter
                                       $"&b=0" +  // Default parameter
                                       $"&eb=0" + // Default parameter
                                       $"&amin={minAge}" +
                                       $"&amax={maxAge}" +
                                       $"&fon=1" + // Default parameter
                                       $"&page=1"; // Default pagination

                    Debug($"S�ker p� URL: {searchUrl}");
                    driver.Navigate().GoToUrl(searchUrl);

                    // Hitta och klicka p� knappen f�r att godk�nna cookies
                    try
                    {
                        var acceptButton = driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
                        acceptButton.Click();
                        Debug("Godk�nde cookies.");
                    }
                    catch (NoSuchElementException)
                    {
                        Debug("Hittade ingen Cookie Consent-knapp. Forts�tter �nd�...");
                    }

                    // V�nta tills sidan laddas
                    System.Threading.Thread.Sleep(2000);

                    // H�mta sidans HTML
                    string htmlContent = driver.PageSource;
                    Debug("Lyckades h�mta HTML efter cookie-godk�nnande.");

                    // Bearbeta HTML med HtmlAgilityPack
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);

                    // H�mta l�nkar till personprofiler
                    var profileLinks = htmlDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'search-result-list')]//li//a[@href]");
                    if (profileLinks == null || profileLinks.Count == 0)
                    {
                        Debug($"Inga relevanta l�nkar hittades f�r \"{firstName} {lastName}\".");
                        return;
                    }

                    lstResults.Items.Clear();
                    foreach (var link in profileLinks)
                    {
                        string href = link.Attributes["href"].Value;

                        // Kontrollera om l�nken �r absolut eller relativ
                        if (!href.StartsWith("http"))
                        {
                            href = "https://www.ratsit.se" + href;
                        }

                        lstResults.Items.Add(href);
                        Debug($"Hittade personl�nk: {href}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel intr�ffade: {ex.Message}", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug($"Ett fel intr�ffade: {ex.Message}");
            }
        }

    }
}
