using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectFromCsv
{
    public class CsvFile
    {
        private string _csvFileFullPath;

        public CsvFile(string csvFileFullPath)
        {
            _csvFileFullPath = csvFileFullPath;
        }

        public IList<CsvRecord> Parse()
        {
            IList<CsvRecord> records = new List<CsvRecord>();

            using (FileStream stream = new FileStream(_csvFileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader reader = new StreamReader(stream))
            {
                string line = reader.ReadLine();
                int lineCount = 1;

                while (line != null)
                {
                    if (!string.IsNullOrWhiteSpace(line)) //skip empty lines
                    {
                        string[] values = line.Split(',');
                        if (values.Length != 2)
                        {
                            throw new Exception(
                                string.Format("Error parsing csv file {0} line #{1} to get the test name and batch file. Line looks like this '{2}'.",
                                _csvFileFullPath, lineCount, line));
                        }
                        else
                        {
                            records.Add(new CsvRecord()
                            {
                                TestName = values[0].Trim(),
                                BatchFile = values[1].Trim()
                            });
                        }
                    }
                    line = reader.ReadLine();
                    lineCount++;
                }
            }

            return records;
        }
    }
}
