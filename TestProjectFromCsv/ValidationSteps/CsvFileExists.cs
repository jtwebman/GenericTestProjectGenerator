using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectFromCsv.ValidationSteps
{
    public class CsvFileExists : OptionsValidationStep 
    {

        public CsvFileExists(Options options) : base(options) { }

        public override string FailedStepText
        {
            get { return string.Format("Cannot find csv file located here '{0}'.", Options.CsvFile); }
        }

        public override bool IsValid()
        {
            return File.Exists(Options.CsvFile);
        }
    }
}
