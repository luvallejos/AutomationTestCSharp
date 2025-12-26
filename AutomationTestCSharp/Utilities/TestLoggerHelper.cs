using OpenQA.Selenium;
using System;
using System.IO;
using System.Text;

namespace AutomationTestCSharp.Utilities
{
    public static class TestLoggerHelper
    {
        public static void SaveOnFailure(IWebDriver driver, string rootFolder)
        {
            Directory.CreateDirectory(rootFolder);

            SafeWrite(rootFolder, "timestamp.txt", DateTime.UtcNow.ToString("u"));

            // URL
            SafeWrite(rootFolder, "currentUrl.txt", SafeGet(() => driver.Url));

            // Title
            SafeWrite(rootFolder, "title.txt", SafeGet(() => driver.Title));

            // Page source
            SafeWrite(rootFolder, "pageSource.html", SafeGet(() => driver.PageSource));

            // Console logs (Browser)
            SafeWrite(rootFolder, "browserConsole.log", SafeGet(() =>
            {
                var logs = driver.Manage().Logs.GetLog(LogType.Browser);
                var sb = new StringBuilder();
                foreach (var entry in logs)
                    sb.AppendLine($"{entry.Timestamp:O} [{entry.Level}] {entry.Message}");
                return sb.ToString();
            }));
        }

        private static string SafeGet(Func<string> get)
        {
            try { return get() ?? ""; }
            catch (Exception ex) { return $"<FAILED TO CAPTURE> {ex.GetType().Name}: {ex.Message}"; }
        }

        private static void SafeWrite(string folder, string fileName, string content)
        {
            try
            {
                File.WriteAllText(Path.Combine(folder, fileName), content ?? "");
            }
            catch
            {
               
            }
        }
    }
}
