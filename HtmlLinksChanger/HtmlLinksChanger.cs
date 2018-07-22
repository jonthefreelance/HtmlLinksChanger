using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HtmlLinksChanger
{
    public partial class HtmlLinksChanger : Form
    {
        public HtmlLinksChanger()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Trim().Equals(string.Empty))
            {
                string filePathSource = textBox1.Text.Trim();

                string[] beforeQuotesHref = Properties.Resources.BeforeQuotesHref.Split(',');
                char[] hrefQuotes = Properties.Resources.HrefQuotes.ToCharArray();

                string hrefSubstringToReplace = textBox2.Text.Trim();
                string hrefSubstringReplacement = textBox3.Text.Trim();

                HtmlFile htmlFileSource = new HtmlFile(filePathSource);
                HtmlHref htmlHref = new HtmlHref(beforeQuotesHref, hrefQuotes);

                htmlFileSource.HtmlFileLines = htmlHref.ReplaceHrefStrings(htmlFileSource.HtmlFileLines, hrefSubstringToReplace, hrefSubstringReplacement);

                MessageBox.Show(htmlFileSource.CreateResultFile());
            }

            else
            {
                MessageBox.Show("Please select a valid file.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(textBox1.Text.Trim()) + @"\result\"))
                {
                    Process.Start("explorer.exe", Path.GetDirectoryName(textBox1.Text.Trim()) + @"\result\");
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Not found.");
            }
        }
    }
}
