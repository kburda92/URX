using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    public class Logger
    {
        private string name;
        private string file_path;

        enum messageType
        {
            Debug, Info, Warning, Error
        }

        public Logger(string name)
        {
            this.name = name;
            file_path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), name + ".txt");
        }

        public void debug(string message)
        {
        #if DEBUG
            WriteMessage(messageType.Debug, message);
        #endif
        }

        public void info(string message)
        {
            WriteMessage(messageType.Info, message);
        }

        public void warning(string message)
        {
            WriteMessage(messageType.Warning, message);
        }

        public void error(string message)
        {
            WriteMessage(messageType.Error, message);
        }

        private void WriteMessage(messageType type, string message)
        {
            File.AppendAllText(file_path, message);
        }
    }
}
