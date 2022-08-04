using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineMovie.Models
{
    public class movie_catagory
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 char are allowed", MinimumLength=3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }

    public class Movie
    {
        public int MovieId { get; set; }
        [Required(ErrorMessage = "movie Name Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 char are allowed", MinimumLength = 3)]
        public string MovieName { get; set; }
        [Required(ErrorMessage = "movie Name Required")]
        [Range(1,50)]
        public Nullable<int> CategoryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Required(ErrorMessage = "Description is Required")]        
        public string Description { get; set; }
        public Nullable<decimal> rating { get; set; }
        public Nullable<decimal> duration { get; set; }
        public string MovieImage { get; set; }
    }
}