using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
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

        public int GetAge(string personalNumber)
        {
            // Hämta födelsedatum från personnummer
            string birthDate = personalNumber.Substring(0, 8);
            int year = int.Parse(birthDate.Substring(0, 4));
            int month = int.Parse(birthDate.Substring(4, 2));
            int day = int.Parse(birthDate.Substring(6, 2));

            // Beräkna ålder
            DateTime birth = new DateTime(year, month, day);
            DateTime today = DateTime.Today;
            int age = today.Year - birth.Year;
            if (birth > today.AddYears(-age)) age--;

            return age;
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
                options.AddArgument("--headless"); // Kör utan GUI
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

                    /* DETTA SKALL LÄGGAS IN SNART
                    int currentPage = 1;

while (true)
{
    // Bygg URL med aktuell sida
    string searchUrl = $"https://www.ratsit.se/sok/person?" +
                       $"fnamn={Uri.EscapeDataString(firstName)}" +
                       $"&enamn={Uri.EscapeDataString(lastName)}" +
                       $"&kn={Uri.EscapeDataString(location)}" +
                       $"&amin={minAge}&amax={maxAge}&fon=1&page={currentPage}";

    Debug($"Söker på URL: {searchUrl}");
    driver.Navigate().GoToUrl(searchUrl);

    // Vänta tills sidan laddas
    System.Threading.Thread.Sleep(2000);

    // Hämta sidans HTML
    string htmlContent = driver.PageSource;

    // Kontrollera om det finns resultat
    HtmlDocument htmlDoc = new HtmlDocument();
    htmlDoc.LoadHtml(htmlContent);

    var profileLinks = htmlDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'search-result-list')]//li//a[@href]");
    if (profileLinks == null || profileLinks.Count == 0)
    {
        Debug("Inga fler resultat. Avslutar sökningen.");
        break;
    }

    // Processa länkarna
    foreach (var link in profileLinks)
    {
        string href = link.Attributes["href"].Value;

        if (!href.StartsWith("http"))
        {
            href = "https://www.ratsit.se" + href;
        }

        Debug($"Hittade länk: {href}");
    }

    // Öka sidräknaren
    currentPage++;
}

                    */

                    Debug($"Söker på URL: {searchUrl}");
                    driver.Navigate().GoToUrl(searchUrl);

                    // Hitta och klicka på knappen för att godkänna cookies
                    try
                    {
                        var acceptButton = driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
                        acceptButton.Click();
                        Debug("Godkände cookies.");
                    }
                    catch (NoSuchElementException)
                    {
                        Debug("Hittade ingen Cookie Consent-knapp. Fortsätter ändå...");
                    }

                    // Vänta tills sidan laddas
                    System.Threading.Thread.Sleep(2000);

                    // Hämta sidans HTML
                    string htmlContent = driver.PageSource;
                    Debug("Lyckades hämta HTML efter cookie-godkännande.");

                    // Bearbeta HTML med HtmlAgilityPack
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);

                    // Hämta länkar till personprofiler
                    var profileLinks = htmlDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'search-result-list')]//li//a[@href]");
                    if (profileLinks == null || profileLinks.Count == 0)
                    {
                        Debug($"Inga relevanta länkar hittades för \"{firstName} {lastName}\".");
                        return;
                    }

                    int i = 0;
                    lstResults.Items.Clear();
                    foreach (var link in profileLinks)
                    {
                        string href = link.Attributes["href"].Value;

                        // Kontrollera om länken är absolut eller relativ
                        if (!href.StartsWith("http"))
                        {
                            href = "https://www.ratsit.se" + href;
                        }

                        if (href.Contains("bolagsfakta.se")) { continue;}
                        if (href.Contains("ratsit.se/kop/kassa")) { continue;}

                        // Innan du lägger till länken i listan:
                        var match = Regex.Match(href, @"ratsit\.se\/(\d{8})-");
                        if (!match.Success)
                        {
                            Debug("Ignorerar företagslänk: " + href);
                            continue;
                        }

                        // Använd det extraherade personnumret
                        string personalNumber = match.Groups[1].Value;
                        int age = GetAge(personalNumber);
                        Debug($"Extraherade personnummer: {personalNumber}, Ålder: {age}");

                        // Kontrollera om åldern är inom intervallet
                        if (age < minAge || age > maxAge) {
                            
                            Debug($"Ignorerar personlänk: {href}, Ålder: {age}. Max ålder som används " + maxAge + " minimum ålder " + minAge);
                            continue; }

                        i++;

                        lstResults.Items.Add(href);
                        Debug($"Hittade personlänk: {href}");

                        // Hämta personuppgifter
                        person person = new person("", "", "", "", "", href);

                        Debug("person " + i +": " + person.Url);
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

    public class person
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }

        public person (string name, string address, string phone, string age, string location, string url)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Age = age;
            Location = location;
            Url = url;
        }
    }
}
