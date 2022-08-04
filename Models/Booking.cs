using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMovie.Models
{
    public class Booking
    {
        public int booking_id { get; set; }
        public Nullable<decimal> amount_paid { get; set; }
        public string payment_Type { get; set; }
        public Nullable<int> cust_id { get; set; }
        public Nullable<int> seat_id { get; set; }

        //public virtual customer customer { get; set; }
        //public virtual seat seat { get; set; }
    }
}