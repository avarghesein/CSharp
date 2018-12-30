using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using System.Management.Automation;
using System.Management.Automation.Runspaces;


namespace AV.CSharp.PowershellAutomation
{
    public class PowershellCallableWrapper
    {
        private RunspacePool _threadPool;

        public bool OpenPowershellThreadPool()
        {
            var threadPool = RunspaceFactory.CreateRunspacePool();
            threadPool.ThreadOptions = PSThreadOptions.UseCurrentThread;
            threadPool.Open();
            _threadPool = threadPool;
            return true;
        }

        public dynamic RunPowerShellCommand(string command, params object[] arguments)
        {
            if (_threadPool == null) OpenPowershellThreadPool();

            var shell = PowerShell.Create();
            shell.RunspacePool = _threadPool;
            shell.AddScript(command);
            var result = shell.Invoke();
            bool isError = shell.HadErrors;
            shell.Dispose();

            if (isError)
            {
                throw new ApplicationException("Error invoking script");
            }       
                 
            return result;
        }

        public void Dispose()
        {
            if (_threadPool != null)
            {
                _threadPool.Close();
                _threadPool.Dispose();
                _threadPool = null;
            }
        }
    };
}
