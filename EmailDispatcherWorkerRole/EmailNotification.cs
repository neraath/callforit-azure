using System.Runtime.Serialization;

namespace EmailDispatcherWorkerRole
{
    [DataContract(Namespace = "http://improvingwa.com")]
    public class EmailNotification
    {
        [DataMember]
        public string ToEmailAddress { get; set; }

        [DataMember]
        public string FromEmailAddress { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }
    }
}