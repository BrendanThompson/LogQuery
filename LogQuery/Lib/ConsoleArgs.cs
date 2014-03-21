using System;
using LogQuery.PowerArgs;

namespace LogQuery.Lib
{
    public class ConsoleArgs
    {
        [ArgShortcut("el")]
        public string EventLog { get; set; }

        [ArgShortcut("id")]
        public long EventId { get; set; }

        [ArgShortcut("qry")]
        public string MessageQuery { get; set; }

        public bool Log { get; set; }

        [ArgShortcut("dst")]
        public string OutputDestination { get; set; }

        [ArgShortcut("show")]
        public bool ShowOutput { get; set; }

        [ArgShortcut("h")]
        public bool Help { get; set; }

        [ArgShortcut("lic")]
        public bool License { get; set; }

        [ArgShortcut("lvl")]
        public string EventLevel { get; set; }

        [ArgShortcut("src")]
        public string EventSource { get; set; }

        [ArgShortcut("cat")]
        public string Category { get; set; }

        [ArgShortcut("dr")]
        public bool DateRange { get; set; }

        [ArgShortcut("sd")]
        public string StartDate { get; set; }

        [ArgShortcut("ed")]
        public string EndDate { get; set; }

        [ArgShortcut("text")]
        public bool OutputText { get; set; }

        [ArgShortcut("xml")]
        public bool OutputXml { get; set; }

        [ArgShortcut("html")]
        public bool OutputHtml { get; set; }

        [ArgShortcut("json")]
        public bool OutputJson { get; set; }

        public bool Update { get; set; }
    }
}
