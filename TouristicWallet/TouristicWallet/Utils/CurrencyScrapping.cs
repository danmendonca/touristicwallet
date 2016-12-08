using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TouristicWallet.utils
{
    class CurrencyScrapping
    {
        public static void GetAllCurrencies()
        {
            WebService.CallWebAsync("http://www.xe.com/iso4217.php", SaveCurrencies);
        }

        private static void SaveCurrencies(String responseHtml)
        {
            //parse html 
            String source = WebUtility.HtmlDecode(responseHtml);
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(source);

            HtmlNode currencyTable = html.DocumentNode.Descendants().First(
                x => (x.Id.Equals("currencyTable")));

            HtmlNode tableBody = currencyTable.Descendants().First(
                x => (x.Name.Equals("tbody")));

            List<HtmlNode> tableRows = tableBody.Descendants().Where(
                x => (x.Name.Equals("tr"))).ToList();

            List<HtmlNode> tds;
            foreach (HtmlNode tr in tableRows)
            {
                tds = tr.Descendants().Where(x => (x.Name.Equals("td"))).ToList();
                if (tds.Count() == 2)
                {
                    String name = tds.ElementAt(1).InnerText;
                    String initials = tds.ElementAt(0).Descendants().First(x => (x.Name.Equals("a"))).InnerText;
                    //System.Diagnostics.Debug.WriteLine(initials+": "+name);
                }
            }

            //save currencies in DB
        }
    }
}
