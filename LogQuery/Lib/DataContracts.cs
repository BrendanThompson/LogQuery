using System;
using System.Runtime.Serialization;

namespace LogQuery.Lib
{
    [DataContract]
    [Serializable]
    public class EventSeries
    {
        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string EventId { get; set; }

        [DataMember]
        public string IntanceId { get; set; }

        [DataMember]
        public string TimeGenerated { get; set; }

        [DataMember]
        public string TimeWritten { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
