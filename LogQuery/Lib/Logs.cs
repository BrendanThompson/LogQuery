using System;
using System.IO;
using System.Xml;

namespace LogQuery.Lib
{
    class Logs
    {
        public static string CurrentTime = DateTime.Now.ToString("ddMMyyyyhhmmssss");
        public static string CurrentQuery = String.Format("Query Executed: {0}", StaticVariables.CurrentQuery(false));
        public static string CurrentEntryCount = String.Format("Entries Found: {0}", Program.Z);

        public static void WriteTextLog(string outputDir, string contents)
        {
            using (var sw = new StreamWriter(String.Format(@"{0}\LogQuery-{1}.log", outputDir, CurrentTime)))
            {
                sw.WriteLine(StaticVariables.Logo(false));
                sw.WriteLine(CurrentQuery);
                sw.WriteLine(CurrentEntryCount);
                sw.Write(contents);
                sw.Close();
            } 
        }

        public static XmlWriter CreateXmlLog(string outputDir)
        {
            var xw = XmlWriter.Create(String.Format(@"{0}\LogQuery-{1}.xml", outputDir, CurrentTime));
            return xw;
        }

        public static void WriteXmlLogStart(XmlWriter xw)
        {
            xw.WriteStartDocument();
            xw.WriteStartElement("LogEntries");
            xw.WriteString("\r\n");
            xw.WriteComment(StaticVariables.Logo(false) + "\r\n" + CurrentQuery + "\r\n"); //+ CurrentEntryCount + "\r\n");
        }

        public static void WriteXmlLogEnd(XmlWriter xw)
        {
            xw.WriteString("\r\n");
            xw.WriteEndDocument();
            xw.Close();
        }

        public static void WriteHtmlLog(string outputDir, string contents)
        {
            using (var sw = new StreamWriter(String.Format(@"{0}\LogQuery-{1}.html", outputDir, CurrentTime)))
            {
                sw.Write(contents);
                sw.Close();
            } 
        }

        public static void WriteJsonLog(string outputDir, string contents)
        {
            using (var sw = new StreamWriter(String.Format(@"{0}\LogQuery-{1}.json", outputDir, CurrentTime)))
            {
                sw.Write(contents);
                sw.Close();
            }
        }
    }
}
