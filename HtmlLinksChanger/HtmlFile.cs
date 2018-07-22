using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlLinksChanger
{
    public class HtmlFile
    {
        public string FilePath { get; set; }
        public List<string> HtmlFileLines { get; set; }

        public HtmlFile(string filePath)
        {
            FilePath = filePath.Trim();
            HtmlFileLines = new List<string>();
            _LoadFileLines();
        }

        private void _LoadFileLines()
        {
            using (StreamReader sr = File.OpenText(FilePath))
            {
                string s = string.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    HtmlFileLines.Add(s);
                }
            }
        }

        public string CreateResultFile()
        {
            string resultMessage = string.Empty;

            if (HtmlFileLines.Count > 0)
            {
                string resultFolder = @"result\";
                string resultFolderPath = Path.GetDirectoryName(FilePath) + @"\" + resultFolder;
                string resultFilePath = resultFolderPath + Path.GetFileName(FilePath);
                string message = string.Empty;

                if (!Directory.Exists(resultFolderPath))
                {
                    Directory.CreateDirectory(resultFolderPath);
                }

                if (File.Exists(resultFilePath))
                {
                    File.Delete(resultFilePath);
                }

                try
                {
                    using (StreamWriter sw = new StreamWriter(resultFilePath))
                    {
                        HtmlFileLines.ForEach(l =>
                        {
                            sw.WriteLine(l);
                        });
                    }

                    resultMessage = "Successfully created file.";
                }

                catch (Exception)
                {
                    resultMessage = "Error.";
                }
            }

            return resultMessage;
        }
    }
}
