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
        public string Description { get; set; }
        [DisplayName("Huidige Status")]
        public CurrentStatus currentStatus { get; set; }
        public enum CurrentStatus
        {
            Open = 1,
            Gerepareerd = 2
        }
        public int User_ID { get; set;  }
    }
}
