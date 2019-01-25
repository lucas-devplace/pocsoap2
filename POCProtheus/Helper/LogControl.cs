using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace POCProtheus.Helper
{
    public class LogControl
    {
        private static string _Path = string.Empty;
        private static bool DEBUG = false;

        public void Write(string msg)
        {
            //_Path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            _Path = HttpContext.Current.Server.MapPath("~");
            _Path = _Path.Substring(0, _Path.Length - 1);
            _Path = _Path.Substring(0, _Path.LastIndexOf("\\"));
            _Path = _Path + "\\Files\\Log";

            try
            {
                using (StreamWriter w = File.AppendText(Path.Combine(_Path, "log.txt")))
                {
                    Log(msg, w);
                }
                if (DEBUG)
                    Console.WriteLine(msg);
            }
            catch (Exception e)
            {
                //implementar erro ao escrever
            }
        }

        static private void Log(string msg, TextWriter w)
        {
            try
            {
                w.Write(Environment.NewLine);
                w.Write("[{0} {1}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.Write("\t");
                w.WriteLine(" {0}", msg);
                w.WriteLine("-----------------------");
            }
            catch (Exception e)
            {
                //implementar erro ao escrever
            }
        }
    }
}