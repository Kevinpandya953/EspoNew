using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("contact_meeting", Schema = "Contacts")]
    public partial class ContactsContactMeeting
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
            get;
            set;
        }

        [Key]
        [Required]
        public string contact_meeting_id { get; set; }

        [ConcurrencyCheck]
        public string contact_id { get; set; }

        public ContactsContact contact { get; set; }

        [ConcurrencyCheck]
        public string meeting_id { get; set; }

        public MeetingsMeeting meeting { get; set; }

        [ConcurrencyCheck]
        public string status { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }
    }
}