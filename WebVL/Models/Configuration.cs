using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVL.Models
{
    public class Configuration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        // Biến lưu key và id trong web config
        static Configuration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            // lấy thuộc tính trong web config
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            // Lấy mã đăng nhập 
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }


        public static APIContext GetAPIContext()
        {
            // thông qua mã đăng nhập chúng ta lấy được apicontext
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();

            return apiContext;
        }
    }
}