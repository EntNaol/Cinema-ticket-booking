//using PayPal.Api;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace OnlineMovie
//{
//    public class PaypalConfiguration
//    {
//        public readonly static string clientId;
//        public readonly static string clientSecret;


//        static PaypalConfiguration()
//        {
//            var config = getconfig();
//            clientId = "Ab8hyXtytVe4q_UF_KkbBFpqjwiPK7QRjhv6vG-Cg7P_fIBSYX_HFedvrwmxYzIOzNpkE6oQ4xKOdRgM";
//            clientSecret = "ENVEa3pOIdqIJzQxYpw4StpzpQRvkNvEBZDt32e8mKgPbHInMuNbj5MAU4SWQmobdoiol2B4HEXOC9vn";
//        }

//        private static Dictionary<string, string> getconfig()
//        {
//            return PayPal.Api.ConfigManager.Instance.GetProperties();
//        }

//        private static string GetAccessToken()
//        {
//            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();
//            return accessToken;
//        }
//        public static APIContext GetAPIContext()
//        {
//            APIContext apicontext = new APIContext(GetAccessToken());
//            apicontext.Config = getconfig();
//            return apicontext;
//        }
//    }
//}