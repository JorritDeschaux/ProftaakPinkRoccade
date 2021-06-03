using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PinkRoccade.BS.Models
{
    public class IncidentHistoryModel
    {
        [DisplayName("Incident Nummer")]
        public int IncidentID { get; set; }
        [DisplayName("Locatie")]
        public string Location { get; set; }
        [DisplayName("Beschrijving")]
        public string Description { get; set; }
        [DisplayName("Huidige Status")]
        public CurrentStatus currentStatus { get; set; }
        public enum CurrentStatus
        {
            Open = 1,
            Wip = 2,
            Gerepareerd = 3
        }
        public int User_ID { get; set;  }
    }
}
