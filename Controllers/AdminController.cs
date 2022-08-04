using Newtonsoft.Json;
using OnlineMovie.DM;
using OnlineMovie.Models;
using OnlineMovie.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMovie.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<DM.movie_catagory>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        public List<SelectListItem> GetCustomer(int cust_id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<DM.customer>().GetFirstOrDefault(cust_id);
           
            list.Add(new SelectListItem { Value = cat.cust_id.ToString(), Text = cat.cust_FirstName });
            
            return list;
        }

        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult Categories()
        {
            List<DM.movie_catagory> allcategories = _unitOfWork.GetRepositoryInstance<DM.movie_catagory>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }

        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(DM.movie_catagory mv)
        {
            mv.IsActive = true;
            mv.IsDelete = false;
            _unitOfWork.GetRepositoryInstance<DM.movie_catagory>().Add(mv);
            return RedirectToAction("Categories");
        }

        public ActionResult UpdateCategory(int categoryId)
        {
            Models.movie_catagory cd;
            if(categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<Models.movie_catagory>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<DM.movie_catagory>().GetFirstOrDefault(categoryId)));
            }
            else
            {
                cd = new Models.movie_catagory();
            }
            return View("UpdateCategory", cd);
        }
        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<DM.movie_catagory>().GetFirstOrDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(DM.movie_catagory mc)
        {
            _unitOfWork.GetRepositoryInstance<DM.movie_catagory>().Update(mc);
            return RedirectToAction("Categories");
        }


        public ActionResult Movie()
        {
            return View(_unitOfWork.GetRepositoryInstance<DM.movie>().GetMovie());
        }
        public ActionResult MovieEdit(int movieId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<DM.movie>().GetFirstOrDefault(movieId));
        }
        [HttpPost]
        public ActionResult MovieEdit(movie mv, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/MovieImage/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            mv.MovieImage = file != null ? pic : mv.MovieImage;
            mv.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<DM.movie>().Update(mv); 
            return RedirectToAction("Movie");
        }
        public ActionResult MovieAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult MovieAdd(movie mv, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/MovieImage/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            mv.MovieImage = pic;
            mv.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<DM.movie>().Add(mv);
            return RedirectToAction("Movie");
        }

        public ActionResult Delete(int movie_id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0F6ARH8;Database=dbMyOnlineMovie;Integrated Security=true");
                conn.Open();
                string sqlQ = "Delete from movie" + " where MovieId like " + movie_id + ";";
                SqlCommand sqlcmd = new SqlCommand(sqlQ, conn);
                sqlcmd.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("Movie");
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Movie");

        }


        public ActionResult Booked()
        {
            List<DM.booking> allbooking = _unitOfWork.GetRepositoryInstance<DM.booking>().GetAllRecordsIQueryable().ToList();
            
            return View(allbooking);
        }

        public ActionResult customer()
        {
            List<DM.customer> allbooking = _unitOfWork.GetRepositoryInstance<DM.customer>().GetAllRecordsIQueryable().ToList();

            return View(allbooking);
        }

        public ActionResult seats()
        {
            List<DM.seat> allbooking = _unitOfWork.GetRepositoryInstance<DM.seat>().GetAllRecordsIQueryable().ToList();

            return View(allbooking);
        }

        
    }
}