GenericTestProjectGenerator
===========================

Will take a CSV file with test name, and command to run, and generate a generic c# test project calling the command.

Command line options:

Option | Description
-------|------------
-c, --csv | Required. Two columned csv file, example: TestName,TextBatchfile.bat
-n, --projectname | Required. The name you want to use for the project.
-p, --projectpath | Project path, will create if doesn't exist. Will default to current path with a new folder for the project.
-m, --commandpath | The path to add onto the test commands in the CSV file. Will default to nothing and you should try using relative paths.
-t, --templatepath | Template file path, will create if doesn't exist. Will default to current path the exe is running from. 
--help | Display this help screen.

The CSV is a very basic format with the headers:

|Test Name | Command To Run |
|----------|----------------|
|Test1 | Test1.bat |
|Test2 | Test2.bat |
