using System;
using System.IO;

namespace EDGW.Logging
{
    public class Logger
    {
        public Logger(string name, string iD)
        {
            if(Writer==null)Writer = new WhiteWriter();
            Name = name;
            ID = iD;
        }
        public ConsoleColor? ConsoleColor = null;
        public void fatal(string format, Exception e, params object[] values) => Fatal(format, e, values);
        public void Fatal(string format, Exception e, params object[] values) => Fatal(string.Format(format, values), e);
        public void fatal(string value, Exception e) => Fatal(value, e);
        public void fatal(Exception e) => Fatal(e);
        public void Fatal(Exception e) => Fatal("An unexpected exception occured", e);
        public void Fatal(string value, Exception e)
        {
            string log = $"{value}\n{e}";
            Console.ForegroundColor = System.ConsoleColor.Red;
            Log("FATAL", log);
            Console.ResetColor();
        }
        public void fatal(string format, params object[] values) => Fatal(format, values);
        public void Fatal(string format, params object[] values) => Fatal(string.Format(format, values));
        public void fatal(string value) => Fatal(value);
        public void Fatal(string value)
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
            Log("FATAL", value);
            Console.ResetColor();
        }

        public void error(string format, Exception e, params object[] values) => Error(format, e, values);
        public void Error(string format, Exception e, params object[] values) => Error(string.Format(format, values), e);
        public void error(string value, Exception e) => Error(value, e);
        public void error(Exception e) => Error(e);
        public void Error(Exception e) => Error("An unexpected exception occured", e);
        public void Error(string value, Exception e)
        {
            string log = $"{value}\n{e}";
            Console.ForegroundColor=System.ConsoleColor.Red;
            Log("ERROR", log);
            Console.ResetColor();
        }
        public void error(string format, params object[] values) => Error(format, values);
        public void Error(string format, params object[] values) => Error(string.Format(format, values));
        public void error(string value) => Error(value);
        public void Error(string value)
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
            Log("ERROR", value);
            Console.ResetColor();
        }

        public void warn(string format, Exception e, params object[] values) => Warn(format, e,values);
        public void Warn(string format, Exception e, params object[] values) => Warn(string.Format(format, values),e);
        public void warn(string value, Exception e) => Warn(value,e);
        public void warn(Exception e) => Warn(e);
        public void Warn(Exception e) => Warn("An unexpected exception occured", e);
        public void Warn(string value, Exception e)
        {
            string log = $"{value}\n{e}";
            Log("WARN", log);
        }
        public void warn(string format, params object[] values) => Warn(format, values);
        public void Warn(string format, params object[] values) => Warn(string.Format(format, values));
        public void warn(string value) => Warn(value);
        public void Warn(string value) => Log("WARN", value);
        public void info(string format, params object[] values) => Info(format, values);
        public void Info(string format, params object[] values) => Info(string.Format(format, values));
        public void info(string value) => Info(value);
        public void Info(string value)
        {
            if (ConsoleColor != null)
            {
                Console.ForegroundColor = ConsoleColor.Value;
                Log("INFO", value);
                Console.ResetColor();
            }
            else
            {
                Log("INFO", value);
            }
        }
        public void Log(string type,string value)
        {
            foreach(var line in value.Split('\n','\r'))
            {
                if (line != "")
                    LogLine(type, line);
            }
        }
        public void LogLine(string type,string value)
        {
            string log = $"[{DateTime.Now.ToString("HH:mm:ss.ffff")} /{type}] ({Name}) {value}";
                Console.WriteLine(log);
            Writer.WriteLine(log);
            Writer.Flush();
        }
        public static void SetWriter(TextWriter writer)
        {
            Writer = writer;
        }
        public string Name { get; set; }
        public string ID { get; set; }
        private static TextWriter Writer { get; set; }
    }
}