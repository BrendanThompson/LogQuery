using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LogQuery.Lib
{
    class Filters
    {
        public static EventLogEntryType ReturnEventLevel(string level)
        {
            EventLogEntryType eventLevel;

            switch (level)
            {
                case "error":
                case "Error":
                    eventLevel = EventLogEntryType.Error;
                    return eventLevel;
                case "Warning":
                case "warning":
                    eventLevel = EventLogEntryType.Warning;
                    return eventLevel;
                case "Information":
                case "information":
                case "info":
                case "Info":
                    eventLevel = EventLogEntryType.Information;
                    return eventLevel;
                case "Success":
                case "success":
                    eventLevel = EventLogEntryType.SuccessAudit;
                    return eventLevel;
                case "Failure":
                case "failure":
                case "Fail":
                case "fail":
                    eventLevel = EventLogEntryType.FailureAudit;
                    return eventLevel;
            }
            return 0;
        }

        public static List<EventLogEntry> NoFilter(List<EventLogEntry> elc)
        {
            return elc.ToList();
        }

        public static List<EventLogEntry> EventIdFilter(List<EventLogEntry> elc, long parsedEventId)
        {
            return elc.Where(log => log.InstanceId.Equals(parsedEventId)).ToList();
        }

        public static List<EventLogEntry> MessageQueryFilter(List<EventLogEntry> elc, string query)
        {
            return elc.Where(log => log.Message.ToLower().Contains(query.ToLower())).ToList();
        }

        public static List<EventLogEntry> EventTypeFilter(List<EventLogEntry> elc, EventLogEntryType parsedEventType)
        {
            return elc.Where(log => log.EntryType == parsedEventType).ToList();
        }

        public static List<EventLogEntry> EventSourceFilter(List<EventLogEntry> elc, string parsedEventSource)
        {
            return elc.Where(log => log.Source.ToLower().Contains(parsedEventSource.ToLower())).ToList();
        }
        public static List<EventLogEntry> EventCategoryFilter(List<EventLogEntry> elc, string parsedCategory)
        {
            return elc.Where(log => log.Category.ToLower() == parsedCategory.ToLower()).ToList();
        }

        public static List<EventLogEntry> EventLogDateRangeFilter(List<EventLogEntry> elc, string parsedStartDate,
                                                                  string parsedEndDate)
        {
            return elc.Where(log => log.TimeGenerated > DateTime.Parse(parsedStartDate) && log.TimeGenerated < DateTime.Parse(parsedEndDate)).ToList();
        }
    }
}
