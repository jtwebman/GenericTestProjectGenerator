using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestProjectFromCsv.ValidationSteps;

namespace TestProjectFromCsv
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var options = new Options();
                if (CommandLine.Parser.Default.ParseArguments(args, options))
                {
                    var validation = new Validation(
                        new ValidationStep[] { 
                            new CsvFileExists(options),
                            new TemplateFilesExist(options)
                        });
                    if (!validation.IsValid())
                    {
                        foreach (string issue in validation.Issues)
                        {
                            Console.WriteLine(issue);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Path.Combine(options.ProjectPath, options.ProjectName));
                        CsvFile csvFile = new CsvFile(options.CsvFile);
                        IList<CsvRecord> testRecords = csvFile.Parse();
                        StringBuilder allTestProjects = new StringBuilder(100 + testRecords.Count * 100);
                        TemplateFileGenerator testTemplateGen = new TemplateFileGenerator(Path.Combine(options.TemplatePath, "Test.tmp"));

                        foreach (CsvRecord record in testRecords)
                        {
                            string filename = Path.Combine(
                                options.ProjectPath,
                                options.ProjectName,
                                string.Format("{0}.GenericTest", record.TestName));
                            try
                            {
                                

                                allTestProjects.AppendLine(string.Format("\t<None Include=\"{0}.GenericTest\">", record.TestName));
                                allTestProjects.AppendLine("\t\t<CopyToOutputDirectory>Always</CopyToOutputDirectory>");
                                allTestProjects.AppendLine("\t</None>");

                                testTemplateGen.GenerateFile(filename, new Dictionary<string, string>()
                                    {
                                        { "[[TestName]]" , record.TestName } ,
                                        { "[[FullFilePath]]", filename } ,
                                        { "[[TestGuid]]", Guid.NewGuid().ToString("D") } ,
                                        { "[[CommandFullFilePath]]", Path.Combine(options.CommandPath, record.BatchFile)}
                                    });
                            } 
                            catch (Exception ex)
                            {
                                Console.WriteLine(string.Format("Error building file '{0}' : {1}", filename, ex.Message));
                            }
                        }


                        string assemblyInfoFilename = Path.Combine(options.ProjectPath, options.ProjectName, "Properties", "AssemblyInfo.cs");
                        try
                        {
                            Directory.CreateDirectory(Path.Combine(options.ProjectPath, options.ProjectName, "Properties"));
                            TemplateFileGenerator assemblyInfoGen = new TemplateFileGenerator(Path.Combine(options.TemplatePath, "AssemblyInfo.cs.tmp"));

                            assemblyInfoGen.GenerateFile(assemblyInfoFilename, new Dictionary<string, string>()
                                    {
                                        { "[[ProjectName]]" , options.ProjectName } ,
                                        { "[[AssemblyGuid]]", Guid.NewGuid().ToString("D") } 
                                    });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(string.Format("Error building file '{0}' : {1}", assemblyInfoFilename, ex.Message));
                        }


                        string projectFileName = Path.Combine(options.ProjectPath, options.ProjectName, string.Format("{0}.csproj", options.ProjectName));
                        try
                        {
                        TemplateFileGenerator projectFileGen = new TemplateFileGenerator(Path.Combine(options.TemplatePath, "ProjectTemplate.proj.tmp"));

                        projectFileGen.GenerateFile(projectFileName, new Dictionary<string, string>()
                                    {
                                        { "[[ProjectGuid]]", Guid.NewGuid().ToString("B") } ,
                                        { "[[ProjectName]]" , options.ProjectName } ,
                                        { "[[Tests]]" , allTestProjects.ToString() }
                                    });
                        } 
                        catch (Exception ex)
                        {
                            Console.WriteLine(string.Format("Error building file '{0}' : {1}", projectFileName, ex.Message));
                        }

                        Console.WriteLine("Done.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
