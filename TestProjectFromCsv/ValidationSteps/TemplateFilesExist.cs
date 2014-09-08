using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectFromCsv.ValidationSteps
{
    public class TemplateFilesExist : OptionsValidationStep
    {
        public TemplateFilesExist(Options options) : base(options) { }

        private string _failedText;

        public override string FailedStepText
        {
            get { return _failedText; }
        }

        public override bool IsValid()
        {
            if (!checkFile("AssemblyInfo.cs.tmp", "AssemblyInfo.cs")) return false;
            if (!checkFile("ProjectTemplate.proj.tmp", "Project.proj")) return false;
            if (!checkFile("Test.tmp", "Test.test")) return false;
            return true;
        }

        private bool checkFile(string filename, string filetype)
        {
            if (!File.Exists(Path.Combine(Options.TemplatePath, filename)))
            {
                _failedText += string.Format("Missing {0} tempelte file '{1}'. \n", filetype, Path.Combine(Options.TemplatePath, filename));
                return false;
            }
            return true;
        }
    }
}
