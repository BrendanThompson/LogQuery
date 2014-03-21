using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Runtime.Serialization.Json;

namespace LogQuery.Lib
{
    class QueryManipulation
    {
        public static void OutputTextFormat(StringBuilder sb, string source, int eventId, long instanceId, DateTime timeGenerated,
                                       DateTime timeWritten, string userName, string category, string msg)
        {
            sb.AppendLine();
            sb.AppendLine("-------------------------------------------");
            sb.AppendLine();
            sb.AppendLine("Source: " + source);
            sb.AppendLine("Event ID: " + eventId);
            sb.AppendLine("Instance ID: " + instanceId);
            sb.AppendLine("Generated Time: " + timeGenerated.ToString(CultureInfo.InvariantCulture));
            sb.AppendLine("Written Time: " + timeWritten.ToString((CultureInfo.InvariantCulture)));
            sb.AppendLine("Username: " + userName);
            sb.AppendLine("Category: " + category);
            sb.AppendLine("Message: " + msg);
        }

        public static void OutputXmlFormat(XmlWriter xw, string source, string eventId, string instanceId,
                                           string timeGenerated, string timeWritten, string userName,
                                           string category, string msg)
        {
            xw.WriteString("\r\n");
            xw.WriteStartElement("Entry");
            xw.WriteString("\r\n");
            xw.WriteElementString("Source", source);
            xw.WriteString("\r\n");
            xw.WriteElementString("EventId", eventId.ToString(CultureInfo.InvariantCulture));
            xw.WriteString("\r\n");
            xw.WriteElementString("InstanceId", instanceId.ToString(CultureInfo.InvariantCulture));
            xw.WriteString("\r\n");
            xw.WriteElementString("TimeGenerated", timeGenerated.ToString(CultureInfo.InvariantCulture));
            xw.WriteString("\r\n");
            xw.WriteElementString("TimeWritten", timeWritten.ToString(CultureInfo.InvariantCulture));
            xw.WriteString("\r\n");
            xw.WriteElementString("Username", userName);
            xw.WriteString("\r\n");
            xw.WriteElementString("Category", category);
            xw.WriteString("\r\n");
            xw.WriteElementString("Message", msg.ToString(CultureInfo.InvariantCulture));
            xw.WriteString("\r\n");
            xw.WriteEndElement();
            xw.WriteString("\r\n");
        }


        public static void OutputHeaderAndFormattingHtml(StringBuilder htmlStringBuilder)
        {
            htmlStringBuilder.AppendLine("<html>");
            htmlStringBuilder.AppendLine("<head>");
            htmlStringBuilder.AppendLine("<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>");
            htmlStringBuilder.AppendLine("<title>LogQuery</title>");
            htmlStringBuilder.AppendLine("<STYLE TYPE='text/css'>");
            htmlStringBuilder.AppendLine("<!--");
            htmlStringBuilder.AppendLine("td {");
            htmlStringBuilder.AppendLine("font-family: Tahoma;");
            htmlStringBuilder.AppendLine("font-size: 11px;");
            htmlStringBuilder.AppendLine("border-top: 1px solid #999999;");
            htmlStringBuilder.AppendLine("border-right: 1px solid #999999;");
            htmlStringBuilder.AppendLine("border-bottom: 1px solid #999999;");
            htmlStringBuilder.AppendLine("border-left: 1px solid #999999;");
            htmlStringBuilder.AppendLine("padding-top: 0px;");
            htmlStringBuilder.AppendLine("padding-right: 0px;");
            htmlStringBuilder.AppendLine("padding-bottom: 0px;");
            htmlStringBuilder.AppendLine("padding-left: 0px;");
            htmlStringBuilder.AppendLine("}");
            htmlStringBuilder.AppendLine(".Headings {");
            htmlStringBuilder.AppendLine("font-family: Tahoma;");
            htmlStringBuilder.AppendLine("font-size: 14px;");
            htmlStringBuilder.AppendLine("font-weight: bold;");
            htmlStringBuilder.AppendLine("}");
            htmlStringBuilder.AppendLine("body {");
            htmlStringBuilder.AppendLine("margin-left: 5px;");
            htmlStringBuilder.AppendLine("margin-top: 5px;");
            htmlStringBuilder.AppendLine("margin-right: 0px;");
            htmlStringBuilder.AppendLine("margin-bottom: 10px;");
            htmlStringBuilder.AppendLine("");
            htmlStringBuilder.AppendLine("table {");
            htmlStringBuilder.AppendLine("border: thin solid #000000;");
            htmlStringBuilder.AppendLine("}");
            htmlStringBuilder.AppendLine("-->");
            htmlStringBuilder.AppendLine("</style>");
            htmlStringBuilder.AppendLine("</head>");
            htmlStringBuilder.AppendLine("<body>");
            htmlStringBuilder.AppendLine("<table width='100%'>");
            htmlStringBuilder.AppendLine("<tr bgcolor='#3366FF'>");
            htmlStringBuilder.AppendLine("<td colspan='7' height='25' align='left'>");
            htmlStringBuilder.AppendLine(String.Format("<font face='tahoma' color='#FFFFFF' size='4'><strong>LogQuery Executed on {0}</strong></font>", Program.MachineName));
            htmlStringBuilder.AppendLine("</td>");
            htmlStringBuilder.AppendLine("</tr>");
            htmlStringBuilder.AppendLine("</table>");

            // Logo Section

            htmlStringBuilder.AppendLine("<br/>");
            htmlStringBuilder.AppendLine("<table width='100%'>");
            htmlStringBuilder.AppendLine("<tr bgcolor='#3366FF'>");
            htmlStringBuilder.AppendLine("<td colspan='7' height='25' align='left'>");
            htmlStringBuilder.AppendLine(String.Format("<font face='tahoma' color='#FFFFFF' size='4'><strong>BT Systems LogQuery {0}</strong></font><br/>", Assembly.GetExecutingAssembly().GetName().Version));
            htmlStringBuilder.AppendLine(String.Format("<font face='tahoma' color='#FFFFFF' size='4'><strong>Copyright (c) Brendan Thompson, BT Systems 2013. All rights reserved.</strong></font><br/>"));
            htmlStringBuilder.AppendLine(String.Format("<font face='tahoma' color='#FFFFFF' size='4'><strong>LogQuery command line event log query utility</strong></font><br/>"));
            htmlStringBuilder.AppendLine(String.Format("<font face='tahoma' color='#FFFFFF' size='4'><strong>-------------------------------------------------------------------------------</strong></font>"));
            htmlStringBuilder.AppendLine("</td>");
            htmlStringBuilder.AppendLine("</tr>");
            htmlStringBuilder.AppendLine("</table>");

            // Start of the body

            htmlStringBuilder.AppendLine("<p class=headings>Query Information:</p>");
            htmlStringBuilder.AppendLine("<table>");
            htmlStringBuilder.AppendLine("<tr>");
            htmlStringBuilder.AppendLine("<td><strong>Executed Query</strong></td>");
            htmlStringBuilder.AppendLine("</tr>");

            htmlStringBuilder.AppendLine("<tr>");
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", StaticVariables.CurrentQuery(false)));
            htmlStringBuilder.AppendLine("</tr>");
            htmlStringBuilder.AppendLine("</table>");


            htmlStringBuilder.AppendLine("<p class=headings>Event Log Entries:</p>");
            htmlStringBuilder.AppendLine("<table>");
            htmlStringBuilder.AppendLine("<tr>");
            htmlStringBuilder.AppendLine("<td><strong>Source</strong></td>");
            htmlStringBuilder.AppendLine("<td><strong>Event ID</strong></td>");
            htmlStringBuilder.AppendLine("<td><strong>Instance ID</strong></td>");
            htmlStringBuilder.AppendLine("<td><strong>Time Generated</strong></td>");
            htmlStringBuilder.AppendLine("<td><strong>Time Written</strong></td>");
            htmlStringBuilder.AppendLine("<td><strong>Username</strong></td>");
            htmlStringBuilder.AppendLine("<td><strong>Category</strong></td>");
            htmlStringBuilder.AppendLine("<td><strong>Message</strong></td>");
            htmlStringBuilder.AppendLine("</tr>");
        }

        public static void OutputBodyHtml(StringBuilder htmlStringBuilder, string source, string eventId, string instanceId, string timeGenerated, string timeWritten, string userName, string category, string msg)
        {
            htmlStringBuilder.AppendLine("<tr>");
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", source));
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", eventId));
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", instanceId));
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", timeGenerated));
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", timeWritten));
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", userName));
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", category));
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", msg));
            htmlStringBuilder.AppendLine("</tr>");   
        }

        public static void OutputFooterHtml(StringBuilder htmlStringBuilder)
        {
            htmlStringBuilder.AppendLine("</table>");
            htmlStringBuilder.AppendLine("<br/>");

            // Query Information

            htmlStringBuilder.AppendLine("<table>");
            htmlStringBuilder.AppendLine("<tr>");
            htmlStringBuilder.AppendLine("<td><strong>Entries Found</strong></td>");
            htmlStringBuilder.AppendLine("</tr>");

            htmlStringBuilder.AppendLine("<tr>");
            htmlStringBuilder.AppendLine(String.Format("<td>{0}</td>", Program.Z));
            htmlStringBuilder.AppendLine("</tr>");
            htmlStringBuilder.AppendLine("</table>");
        }

        public static void OutputJsonFormat(StringBuilder jsonStringBuilder, string source, string eventId, string instanceId, string timeGenerated, string timeWritten, string userName, string category, string msg)
        {
            var obj = new EventSeries
                {
                    Source = source,
                    EventId = eventId,
                    IntanceId = instanceId,
                    TimeGenerated = timeGenerated,
                    TimeWritten = timeWritten,
                    UserName = userName,
                    Category = category,
                    Message = msg
                };

            var serializer = new DataContractJsonSerializer(typeof (EventSeries));
            var ms = new MemoryStream();

            serializer.WriteObject(ms,obj);

            jsonStringBuilder.AppendLine(Encoding.UTF8.GetString(ms.ToArray()));
            jsonStringBuilder.AppendLine();
            jsonStringBuilder.AppendLine();
        }
    }
}
