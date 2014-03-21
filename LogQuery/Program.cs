using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using LogQuery.Lib;
using LogQuery.PowerArgs;
using System.Xml;

namespace LogQuery
{
    class Program
    {
        public static int Z = 0;
        public static string MachineName = Environment.MachineName;

        static void Main(string[] args)
        {
            var eventLog = new EventLog();
            var textStringBuilder = new StringBuilder();
            var htmlStringBuilder = new StringBuilder();
            var jsonStringBuilder = new StringBuilder();
            XmlWriter xw = null;

            StaticVariables.Logo(true);
            AutoUpdate.CleanupDependencies();

            try
            {
                var parsed = Args.Parse<ConsoleArgs>(args);
                StaticVariables.CurrentQuery(true);
                
                if (parsed.Log.Equals(true) && parsed.OutputXml.Equals(true))
                    xw = Logs.CreateXmlLog(parsed.OutputDestination);
                
                if (parsed.Help.Equals(true))
                {
                    StaticVariables.Help();
                }
                else if (parsed.License.Equals(true))
                {
                    StaticVariables.License();
                }
                else if (parsed.Update.Equals(true))
                {
                    AutoUpdate.ResolveUpdateDependencies();
                }
                else if (args.Length < 1)
                {
                    ConsoleOptions.WarningMessage("You have not entered any options!");
                    StaticVariables.Help(); 
                }

                eventLog.MachineName = MachineName;
                eventLog.Log = parsed.EventLog;
                var elc = new List<EventLogEntry>(eventLog.Entries.Cast<EventLogEntry>());

                if (parsed.EventId > 0)
                   elc = Filters.EventIdFilter(elc, parsed.EventId);
                if (parsed.MessageQuery != null)
                    elc = Filters.MessageQueryFilter(elc, parsed.MessageQuery);
                if (parsed.EventLevel != null)
                    elc = Filters.EventTypeFilter(elc, Filters.ReturnEventLevel(parsed.EventLevel));
                if (parsed.EventSource != null)
                    elc = Filters.EventSourceFilter(elc, parsed.EventSource);
                if (parsed.EventId < 0 && parsed.MessageQuery == null && parsed.EventLevel == null &&
                    parsed.EventSource == null)
                    elc = Filters.NoFilter(elc);
                if (parsed.Category != null)
                    elc = Filters.EventCategoryFilter(elc, parsed.Category);
                if (parsed.DateRange.Equals(true))
                    elc = Filters.EventLogDateRangeFilter(elc, parsed.StartDate, parsed.EndDate);

                if (parsed.Log.Equals(true) && parsed.OutputDestination != null)
                    CheckRoutines.CheckDestinationExists(parsed.OutputDestination);
                if (parsed.Log.Equals(true) && parsed.OutputXml.Equals(true))
                    Logs.WriteXmlLogStart(xw);
                if(parsed.Log.Equals(true) && parsed.OutputHtml.Equals(true))
                    QueryManipulation.OutputHeaderAndFormattingHtml(htmlStringBuilder);

                foreach (var eventLogEntry in elc)
                {
#pragma warning disable 612,618
                    Z++;
                    if (parsed.Log.Equals(true) && parsed.OutputText.Equals(true))
                    {
                        QueryManipulation.OutputTextFormat(textStringBuilder, eventLogEntry.Source, eventLogEntry.EventID, eventLogEntry.InstanceId, eventLogEntry.TimeGenerated, eventLogEntry.TimeWritten, eventLogEntry.UserName, eventLogEntry.Category, eventLogEntry.Message);
                    }
                    if (parsed.Log.Equals(true) && parsed.OutputXml.Equals(true))
                    {
                        QueryManipulation.OutputXmlFormat(xw, eventLogEntry.Source, eventLogEntry.EventID.ToString(CultureInfo.InvariantCulture), eventLogEntry.InstanceId.ToString(CultureInfo.InvariantCulture), eventLogEntry.TimeGenerated.ToString(CultureInfo.InvariantCulture), eventLogEntry.TimeWritten.ToString(CultureInfo.InvariantCulture), eventLogEntry.UserName, eventLogEntry.Category, eventLogEntry.Message);
                    }
 
                    if (parsed.Log.Equals(true) && parsed.OutputHtml.Equals(true))
                    {
                        QueryManipulation.OutputBodyHtml(htmlStringBuilder, eventLogEntry.Source, eventLogEntry.EventID.ToString(CultureInfo.InvariantCulture), eventLogEntry.InstanceId.ToString(CultureInfo.InvariantCulture), eventLogEntry.TimeGenerated.ToString(CultureInfo.InvariantCulture), eventLogEntry.TimeWritten.ToString(CultureInfo.InvariantCulture), eventLogEntry.UserName, eventLogEntry.Category, eventLogEntry.Message);
                    }
                    if (parsed.Log.Equals(true) && parsed.OutputJson.Equals(true))
                    {

                        QueryManipulation.OutputJsonFormat(jsonStringBuilder, eventLogEntry.Source, eventLogEntry.EventID.ToString(CultureInfo.InvariantCulture), eventLogEntry.InstanceId.ToString(CultureInfo.InvariantCulture), eventLogEntry.TimeGenerated.ToString(CultureInfo.InvariantCulture), eventLogEntry.TimeWritten.ToString(CultureInfo.InvariantCulture), eventLogEntry.UserName, eventLogEntry.Category, eventLogEntry.Message);
#pragma warning restore 612,618
                    }
                }

                if (parsed.Log.Equals(true))
                    if (parsed.OutputText.Equals(true))
                    {
                        Logs.WriteTextLog(parsed.OutputDestination, textStringBuilder.ToString());
                    }
                    if (parsed.OutputXml.Equals(true))
                    {
                        Logs.WriteXmlLogEnd(xw);
                    }
                    if (parsed.OutputHtml.Equals(true))
                    {
                        QueryManipulation.OutputFooterHtml(htmlStringBuilder);
                        Logs.WriteHtmlLog(parsed.OutputDestination, htmlStringBuilder.ToString());
                    }
                    if (parsed.OutputJson.Equals(true))
                    {
                        Logs.WriteJsonLog(parsed.OutputDestination, jsonStringBuilder.ToString());
                    }

                    Console.Write("Entries Found: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Z);
                    Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception ex)
            {
                ConsoleOptions.ErrorMessage("Please check your command Syntax");
                //StaticVariables.Help();
                Console.WriteLine(ex);
            }
        }   
    }
}
