using AV.CSharp.PowershellAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.CSharp.Test.PowerShell.Automation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Make sure you set execution policy, so that you can run scripts:
            //i.e Set-ExecutionPolicy Unrestricted
            //You may need to do this for both x64 and x86 version of powershell consoles
            PowershellCallableWrapper shell = new PowershellCallableWrapper();

            //Using one of my powershell commmand
            //Download powershell modules and setup the module path accordingly
            //https://github.com/avarghesein/Powershells/blob/master/README.md
            dynamic results = shell.RunPowerShellCommand(
                @"Import-Module -Name ""VA.Registry.Utility"" -Force;" + 
                @"Search-Registry -tokenToSearch ""NGen"" -tokenType KeyName -pathsToSearch @(""HKLM\SOFTWARE\Microsoft"")");

            foreach(dynamic psObject in results)
            {
                Console.WriteLine("KeyPath= {0}, Name = {1}, Value = {2}", psObject.KeyPath, psObject.ValueName, psObject.Value);
            }

            shell.Dispose();

            Console.ReadKey();
        }
    }
}
