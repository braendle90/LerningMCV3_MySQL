using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LerningMCV3_MySQL.Services
{
    public class RunProgram
    {
        public string processWithPath { get; set; }

        public RunProgram(string processWithPath,string arguments)
        {
            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = processWithPath;
            process.StartInfo.Arguments = " "+ arguments;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.Start();
            process.WaitForExit();// Waits here for the process to exit.

        }
    }
}
