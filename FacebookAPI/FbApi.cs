using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using System.Text.RegularExpressions;

//----------------------------------
//Developed by Tuan6956
//----------------------------------

namespace FacebookAPI
{
    public class FbApi
    {
        #region Fields
        private FacebookClient _fbc;
        private string _AccessToken = null;
        private string _Cookies = null;
        #endregion
        #region Constructor
        public FbApi(string token)
        {
            _AccessToken = token;
            _fbc = new FacebookClient(_AccessToken);
            _fbc.Version = "v2.3";
        }
        public FbApi(string token, string cookies)
        {
            _Cookies = cookies;
            _AccessToken = token;
            _fbc = new FacebookClient(_AccessToken);
            _fbc.Version = "v2.3";
        }
        #endregion
        #region Public Methos
        public bool Follow(string UID)
        {
            try
            {
                Dictionary<string, object> fbParams = new Dictionary<string, object>();
                fbParams[""] = "";
                string html = _fbc.Post($"/{UID}/subscribers", fbParams).ToString();
                if (html.Contains("\"success\":true"))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }

        }
        public bool AddFriend(string UID)
        {
            try
            {
                Dictionary<string, object> fbParams = new Dictionary<string, object>();
                fbParams[""] = "";
                string html = _fbc.Post($"/me/friends/{UID}", fbParams).ToString();
                if (html.Contains("rue"))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }

        }
        public bool Comment(string ID_Post, string Message)
        {
            try
            {
                Dictionary<string, object> fbParams = new Dictionary<string, object>();
                fbParams["message"] = Message;
                string html = _fbc.Post($"/{ID_Post}/comments", fbParams).ToString();
                if (html.Contains("id"))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }

        }
        public bool Like(string ID_Post)
        {
            try
            {
                Dictionary<string, object> fbParams = new Dictionary<string, object>();
                fbParams[""] = "";
                string html = _fbc.Post($"/{ID_Post}/likes", fbParams).ToString();
                if (html.Contains("rue"))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }

        }
        public bool UpdateStatus(string Status,out string IdPost)
        {
            try
            {
                Dictionary<string, object> fbParams = new Dictionary<string, object>();
                fbParams["message"] = Status;
                fbParams["privacy"] = "{'value':'EVERYONE'}";
                string html = _fbc.Post($"/me/feed", fbParams).ToString();
                IdPost = "";
                if (html.Contains("id"))
                {
                    IdPost = Regex.Match(html, "([\\d_]+)").Value;
                    return true;
                }
                return false;
            }
            catch
            {
                IdPost = "";
                return false;
            }


        }
        public bool UpdateStatus(string Status, string Link, out string IdPost)
        {
            try
            {
                Dictionary<string, object> fbParams = new Dictionary<string, object>();
                fbParams["message"] = Status;
                fbParams["link"] = Link;
                fbParams["privacy"] = "{'value':'EVERYONE'}";
                string html = _fbc.Post($"/me/feed", fbParams).ToString();
                IdPost = Regex.Match(html, "([\\d_]+)").Value;
                return true;
            }
            catch
            {
                IdPost = null;
                return false;
            }


        }
        public bool UploadPhoto(string Status, string UrlPicture, out string IdPost)
        {
            IdPost = null;
            try
            {
                Dictionary<string, object> fbParams = new Dictionary<string, object>();
                fbParams["message"] = Status;
                fbParams["url"] = UrlPicture;
                string html = _fbc.Post($"/me/photos", fbParams).ToString();
                if (html.Contains("post_id"))
                    IdPost = Regex.Match(html, "post_id\".\"([\\d_]+)").Groups[1].Value;
                return false;
                //return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Share(string IDShare,string IdPost)
        {
            IdPost = null;
            try
            {
                Dictionary<string, object> fbParams = new Dictionary<string, object>();
                fbParams[""] = "";
                string html = _fbc.Post($"/{IDShare}/sharedposts", fbParams).ToString();
                if (html.Contains("id"))
                    IdPost = Regex.Match(html, "([\\d_]+)").Value;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string[] GetFriendRequest()
        {
            try
            {
                List<string> friend = new List<string>();
                _fbc.Version = "v1.0";
                string html = _fbc.Get("/me/friendrequests?fields=feed{id}").ToString();
                var value = Regex.Matches(html, "from([^}]+)");
                foreach (Match item in value)
                {
                    friend.Add(Regex.Match(item.ToString(), "(\\d+)").Value);
                }
                string[] id = friend.ToArray();
                return id;
            }
            catch
            {
                return null;
            }

        }
        public bool CheckLive()
        {
            try
            {
                string html = _fbc.Get("/me").ToString();
                if (html.Contains("id"))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
