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
            // H�mta f�delsedatum fr�n personnummer
            string birthDate = personalNumber.Substring(0, 8);
            int year = int.Parse(birthDate.Substring(0, 4));
            int month = int.Parse(birthDate.Substring(4, 2));
            int day = int.Parse(birthDate.Substring(6, 2));

            // Ber�kna �lder
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
                Debug("F�rnamn �r obligatoriskt.");
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

                    /* DETTA SKALL L�GGAS IN SNART
                    int currentPage = 1;

while (true)
{
    // Bygg URL med aktuell sida
    string searchUrl = $"https://www.ratsit.se/sok/person?" +
                       $"fnamn={Uri.EscapeDataString(firstName)}" +
                       $"&enamn={Uri.EscapeDataString(lastName)}" +
                       $"&kn={Uri.EscapeDataString(location)}" +
                       $"&amin={minAge}&amax={maxAge}&fon=1&page={currentPage}";

    Debug($"S�ker p� URL: {searchUrl}");
    driver.Navigate().GoToUrl(searchUrl);

    // V�nta tills sidan laddas
    System.Threading.Thread.Sleep(2000);

    // H�mta sidans HTML
    string htmlContent = driver.PageSource;

    // Kontrollera om det finns resultat
    HtmlDocument htmlDoc = new HtmlDocument();
    htmlDoc.LoadHtml(htmlContent);

    var profileLinks = htmlDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'search-result-list')]//li//a[@href]");
    if (profileLinks == null || profileLinks.Count == 0)
    {
        Debug("Inga fler resultat. Avslutar s�kningen.");
        break;
    }

    // Processa l�nkarna
    foreach (var link in profileLinks)
    {
        string href = link.Attributes["href"].Value;

        if (!href.StartsWith("http"))
        {
            href = "https://www.ratsit.se" + href;
        }

        Debug($"Hittade l�nk: {href}");
    }

    // �ka sidr�knaren
    currentPage++;
}

                    */

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

                    int i = 0;
                    lstResults.Items.Clear();
                    foreach (var link in profileLinks)
                    {
                        string href = link.Attributes["href"].Value;

                        // Kontrollera om l�nken �r absolut eller relativ
                        if (!href.StartsWith("http"))
                        {
                            href = "https://www.ratsit.se" + href;
                        }

                        if (href.Contains("bolagsfakta.se")) { continue;}
                        if (href.Contains("ratsit.se/kop/kassa")) { continue;}

                        // Innan du l�gger till l�nken i listan:
                        var match = Regex.Match(href, @"ratsit\.se\/(\d{8})-");
                        if (!match.Success)
                        {
                            Debug("Ignorerar f�retagsl�nk: " + href);
                            continue;
                        }

                        // Anv�nd det extraherade personnumret
                        string personalNumber = match.Groups[1].Value;
                        int age = GetAge(personalNumber);
                        Debug($"Extraherade personnummer: {personalNumber}, �lder: {age}");

                        // Kontrollera om �ldern �r inom intervallet
                        if (age < minAge || age > maxAge) {
                            
                            Debug($"Ignorerar personl�nk: {href}, �lder: {age}. Max �lder som anv�nds " + maxAge + " minimum �lder " + minAge);
                            continue; }

                        i++;

                        lstResults.Items.Add(href);
                        Debug($"Hittade personl�nk: {href}");

                        // H�mta personuppgifter
                        person person = new person("", "", "", "", "", href);

                        Debug("person " + i +": " + person.Url);
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
