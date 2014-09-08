using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectFromCsv
{
    public class Options
    {
        public Options()
        {
            TemplatePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ProjectPath = Directory.GetCurrentDirectory();
            CommandPath = "";
        }

        [Option('c', "csv", Required = true, HelpText = "Two columned csv file, example: TestName,TextBatchfile.bat")]
        public string CsvFile { get; set; }

        [Option('n', "projectname", Required = true, HelpText = "The name you want to use for the project.")]
        public string ProjectName { get; set; }

        [Option('p', "projectpath", HelpText = "Project path, will create if doesn't exist. Will default to current path with a new folder for the project.")]
        public string ProjectPath { get; set; }

        [Option('m', "commandpath", HelpText = "The path to add onto the test commands in the CSV file. Will default to nothing and you should try using relative paths.")]
        public string CommandPath { get; set; }

        [Option('t', "templatepath", HelpText = "Template file path, will create if doesn't exist. Will default to current path the exe is running from.")]
        public string TemplatePath { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
