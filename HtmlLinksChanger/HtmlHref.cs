using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlLinksChanger
{
    public class HtmlHref
    {
        private List<string> _BeforeQuotesHrefs { get; set; }
        private List<char> _HrefQuotes { get; set; }
        private string _HrefSubstringToReplace { get; set; }
        private string _HrefSubstringReplacement { get; set; }

        public string HrefString { get; set; }

        public HtmlHref(string[] beforeQuotesHref, char[] hrefQuotes)
        {
            _HrefQuotes = new List<char>();
            _BeforeQuotesHrefs = new List<string>();

            AddBeforeQuotesHrefs(beforeQuotesHref);
            AddHrefQuotes(hrefQuotes);
        }

        public void AddBeforeQuotesHrefs(string[] additionalBeforeQuotesHrefs)
        {
            _BeforeQuotesHrefs.AddRange(additionalBeforeQuotesHrefs);
        }

        public void AddHrefQuotes(char[] additionalHrefQuotes)
        {
            _HrefQuotes.AddRange(additionalHrefQuotes);
        }

        public List<string> ReplaceHrefStrings(List<string> linesForReplacement, string hrefSubstringToReplace, string hrefSubstringReplacement)
        {
            List<string> resultFileLines = new List<string>();

            linesForReplacement.ForEach(l => {
                _BeforeQuotesHrefs.ForEach(h => {
                    _HrefQuotes.ForEach(q => {
                        string substringToReplace = h + q + hrefSubstringToReplace;

                        if (l.Contains(substringToReplace))
                        {
                            string substringReplacement = h + q + hrefSubstringReplacement;
                            l = l.Replace(substringToReplace, substringReplacement);
                        }
                    });
                });

                resultFileLines.Add(l);
            });

            return resultFileLines;
        }
    }
}
