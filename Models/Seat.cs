using OnlineMovie.DM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineMovie.Models
{
    public class Seat
    {
        public int seat_id { get; set; }
        public string seat_num { get; set; }
        public int cat_id { get; set; }
        public int schedule_id { get; set; }
        public Nullable<decimal> price { get; set; }

        public List<Seat> seatinfo { get; set; }
    }
}