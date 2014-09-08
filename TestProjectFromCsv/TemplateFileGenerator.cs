using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectFromCsv
{
    public class TemplateFileGenerator
    {
        private string _templateFile;

        public TemplateFileGenerator(string templateFile)
        {
            _templateFile = templateFile;
        }

        public virtual void GenerateFile(string outputFile, IDictionary<string, string> templateTokens)
        {
            /* Read Template in */
            string fileTemplate = "";
            using (FileStream stream = new FileStream(_templateFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader reader = new StreamReader(stream))
            {
                fileTemplate = reader.ReadToEnd();
            }

            /* Repalce Tokens */
            foreach (string key in templateTokens.Keys)
            {
                fileTemplate = fileTemplate.Replace(key, templateTokens[key]);
            }

            /* Write File */
            using (FileStream stream = new FileStream(outputFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(fileTemplate);
            }
        }

    }
}
