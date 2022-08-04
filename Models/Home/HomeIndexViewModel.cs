using OnlineMovie.DM;
using OnlineMovie.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;


namespace OnlineMovie.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbMyOnlineMovieEntities context = new dbMyOnlineMovieEntities();
        public IPagedList<DM.movie> ListOfMovies { get; set; }       
        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@search",search??(object)DBNull.Value)                
            };
            //List<DM.movie> data = context.Database.SqlQuery<DM.movie>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            IPagedList<DM.movie> data = context.Database.SqlQuery<DM.movie>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListOfMovies = data
            };
        }
    }
}