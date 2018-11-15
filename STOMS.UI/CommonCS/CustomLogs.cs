using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace STOMS.UI.CommonCS
{
    public  class CustomLogs:IDisposable
    {
        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public void UpdateToLog(string fileName,string Message)
        {
            string curFile = fileName;
            
            if(!File.Exists(curFile))
            {
               FileStream fs = File.Create(curFile);
                fs.Close();
               File.AppendAllText(curFile, Message);
            }
            else
            {
                File.AppendAllText(curFile, Message + Environment.NewLine);
            }
        }
    }
}