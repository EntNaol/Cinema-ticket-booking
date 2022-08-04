using OnlineMovie.DM;
using OnlineMovie.Models.Home;
using OnlineMovie.Models;
using OnlineMovie.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMovie.Controllers
{
    public class HomeController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbMyOnlineMovieEntities ctx = new dbMyOnlineMovieEntities(); 
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 10, page));
        }
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<DM.seatCategory>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.cat_id.ToString(), Text = item.cat_name });
            }
            return list;
        }
        
        public List<SelectListItem> GetSchedule()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<DM.schedule>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.schedule_id.ToString(), Text = item.date_time.ToString() });
            }
            return list;
        }

        public List<SelectListItem> GetMovie()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<DM.movie>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.MovieId.ToString(), Text = item.MovieName });
            }
            return list;
        }


        public ActionResult CheckoutDetails(int movieId)
        {            
            return View(_unitOfWork.GetRepositoryInstance<DM.movie>().GetFirstOrDefault(movieId));
        }


        public ActionResult getBook()
        {
            List<DM.seat> allcategories = _unitOfWork.GetRepositoryInstance<DM.seat>().GetAllRecordsIQueryable().ToList();
            return View(allcategories);
        }

        public ActionResult Book()
        {
            ViewBag.CategoryList = GetCategory();
            ViewBag.ScheduleList = GetSchedule();            
            var cat = _unitOfWork.GetRepositoryInstance<DM.seat>().GetAllRecords();
            var cat2 = _unitOfWork.GetRepositoryInstance<DM.schedule>().GetAllRecords();

            ViewBag.seat_num = cat;
            ViewBag.schedule = cat2;

            return View();
        }

        [HttpPost]
        public ActionResult Book(seat seat, booking booking, int cust_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-0F6ARH8;Database=dbMyOnlineMovie;Integrated Security=true";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("select count(*) from seat where seat_num like '" + seat.seat_num + "' and schedule_id like '" + seat.schedule_id + "'", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                ViewBag.img = "../Images/standard.jpg";
                ViewBag.img2 = "../Images/vip.jpg";
                if (count == 0)
                {
                    if (seat.cat_id == 1)
                    {
                        seat.price = 120;
                    }
                    else if (seat.cat_id == 2)
                    {
                        seat.price = 150;
                    }
                    
                    var cust = _unitOfWork.GetRepositoryInstance<DM.customer>().GetFirstOrDefault(cust_id);
                    _unitOfWork.GetRepositoryInstance<DM.seat>().Add(seat);

                    //var cust = _unitOfWork.GetRepositoryInstance<DM.customer>().GetFirstOrDefault(cust_id);            

                    booking.amount_paid = seat.price;
                    booking.cust_id = cust.cust_id;
                    booking.seat_id = seat.seat_id;
                    _unitOfWork.GetRepositoryInstance<DM.booking>().Add(booking);

                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("BookErr", new { cust_id = cust_id });
        }

        public ActionResult BookErr()
        {
            ViewBag.CategoryList = GetCategory();
            ViewBag.ScheduleList = GetSchedule();
            var cat = _unitOfWork.GetRepositoryInstance<DM.seat>().GetAllRecords();
            var cat2 = _unitOfWork.GetRepositoryInstance<DM.schedule>().GetAllRecords();

            ViewBag.seat_num = cat;
            ViewBag.schedule = cat2;
            return View();
        }

        [HttpPost]
        public ActionResult BookErr(seat seat, booking booking, int cust_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-0F6ARH8;Database=dbMyOnlineMovie;Integrated Security=true";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("select count(*) from seat where seat_num like '" + seat.seat_num + "' and schedule_id like '" + seat.schedule_id + "'", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                if (count == 0)
                {
                    if (seat.cat_id == 1)
                    {
                        seat.price = 120;
                    }
                    else if (seat.cat_id == 2)
                    {
                        seat.price = 150;
                    }
                    var cust = _unitOfWork.GetRepositoryInstance<DM.customer>().GetFirstOrDefault(cust_id);
                    _unitOfWork.GetRepositoryInstance<DM.seat>().Add(seat);

                    //var cust = _unitOfWork.GetRepositoryInstance<DM.customer>().GetFirstOrDefault(cust_id);            

                    booking.amount_paid = seat.price;
                    booking.cust_id = cust.cust_id;
                    booking.seat_id = seat.seat_id;
                    _unitOfWork.GetRepositoryInstance<DM.booking>().Add(booking);

                    return RedirectToAction("Index", "Home");
                }                
            }
            return RedirectToAction("BookErr", new { cust_id = cust_id });
        }

        public ActionResult customer()
        {
            ViewBag.movielist = GetMovie();
            return View();
        }
        [HttpPost]
        public ActionResult customer(customer customer, int movieId)
        {
            var mv = _unitOfWork.GetRepositoryInstance<DM.movie>().GetFirstOrDefault(movieId);
            customer.movie_id = mv.MovieId;
            customer.createOn = DateTime.Now;

            _unitOfWork.GetRepositoryInstance<DM.customer>().Add(customer);
            return RedirectToAction("Book", new { cust_id = customer.cust_id });
        }

    
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}