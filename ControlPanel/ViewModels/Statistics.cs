using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlPanel.ViewModels
{
    public class Statistics
    {
        public long AllProducts { get; set; }

        public long AllClients { get; set; }

        public long AllCompanies { get; set; }

        public long AllPayments { get; set; }

        public double AllPaymentsSum { get; set; }

        public long AllShippingRequests { get; set; }

        public long DoneShippingReqeusts { get; set; }

        public long ScheduledShippingRequests { get; set; }

        public long OpenShippingRequests { get; set; }
    }
}