using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace App1
{
    public class HttpManager
    {
        //
        private static string _strResult;
        //OAuth2.0驗證器
        //private static OAuth2Authenticator _auth;
        // 委託事件 - 用戶信箱帳號
        public delegate void DelegateCB(string email);
        // 委託物件 - 用戶信箱帳號
        public static DelegateCB onCompleteCB;
       // public static void googleLogin(DelegateCB cbfc)
       //{
       //    onCompleteCB = (DelegateCB)cbfc;
       //    //clientId 和 clientSecret要打你在以下網址申請到的googleAPP頁面取得
       //    //https://console.developers.google.com/project/1011889472812/apiui/credential?authuser=0
       //    //scope的部分是你要向google的服務器多要什麼資料，當然這需要使用者授權
       //    //authorizeUrl這個則是服務器的API位置
       //    //redirectUrl由於獲取token的前必須先給他一個轉址用的網站拿來取得code
       //    //在用code去要token 自然的accessTokenUrl就是要token的API位置了!
       //    _auth = new OAuth2Authenticator(
       //         clientId: GlobalData.GOOGLECLIENTID,
       //         clientSecret:GlobalData.GOOGLECLIENTSECRET,
       //         scope: "email",
       //         authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/auth"),
       //         redirectUrl: new Uri(GlobalData.GOOGLEREDIRECTURL),
       //         accessTokenUrl: new Uri("https://accounts.google.com/o/oauth2/token"),
       //         getUsernameAsync: null
       //     );
       //    //這行主要是叫出登入頁面和授權畫面的!
       //    GlobalData.ACTIVITY.StartActivity(_auth.GetUI(GlobalData.ACTIVITY));
       //    //以下是取完token後要做的事情，沒錯就是拿token去要資料了!
       //    _auth.Completed += (s, e) => { {
       //        //先檢查是否有接到回傳的事件
       //        if (e.IsAuthenticated)
       //        {   //首先先拿個東西來佔存偷啃
       //            string access_token = "沒有偷啃";
       //            //是這樣的要token他會回傳一些資料給你像是偷啃跟這把偷啃的有效期限，而有效期限通常是1小時XDD
       //            access_token = e.Account.Properties["access_token"];
       //            //既然拿到偷啃了‵那便去要我們要的資料巴
       //            _strResult = GetWebRequest(string.Concat("https://www.googleapis.com/oauth2/v1/userinfo?access_token=", access_token));
       //            //一樣回的時候他便會回傳一個json string 也是要把它轉型為Jobject
       //            JObject returnJob = JsonConvert.DeserializeObject<JObject>(_strResult);
       //            //先檢查一下看有沒有我們要的資料XD
       //            if (returnJob != null && returnJob["email"] != null && returnJob["email"].ToString() != "")
       //            {   //既然進來了表示有，便把這資料存回email這個變數裡XD
       //                GlobalData.USEREMAIL = returnJob["email"].ToString();
       //                onCompleteCB.Invoke(GlobalData.USEREMAIL);
       //              //  Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< 此人信箱為：{0} ID為: {1} >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>", returnJob["email"].ToString(), returnJob["id"].ToString());
       //            }
       //        }
       //    }};
       //}
       // public static void facebookLogin(DelegateCB cbfc)
       //{
       //    onCompleteCB = (DelegateCB)cbfc;
       //    //clientId這些可以在以下網址註冊一個APP來取得
       //    //https://developers.facebook.com/
       //    //scope是指我們要跟facebook服務器多要的資料XD
       //    //authorizeUrl這是facebook的服務器位置XD
       //    //redirectUrl這部分也是要指給facebook的服務器XD           
       //    //由於facebook服務器便會將token回傳給你
       //    _auth = new OAuth2Authenticator (
       //    clientId: GlobalData.FACEBOOKCLIENTID,
       //    scope: "email,publish_actions",
       //    authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
       //    redirectUrl: new Uri(GlobalData.FACEBOOKREDIRECTURL));
       //    //叫出登入授權畫面
       //    GlobalData.ACTIVITY.StartActivity(_auth.GetUI(GlobalData.ACTIVITY));
       //    //如果token回來了便會進到這個小funtion
       //    _auth.Completed += (s, e) =>
       //    {
       //        {   //先檢查看看是否真的有事件被觸發
       //            if (e.IsAuthenticated)
       //            {   //宣告一個變數來存偷啃
       //                string access_token = "沒有偷啃";
       //                //把偷啃丟給剛宣告的變數
       //                access_token = e.Account.Properties["access_token"];
       //                GlobalData.FACEBOOKTOKEN = access_token;
       //                //拿偷啃去要我們要的資料拉
       //                _strResult = GetWebRequest(string.Concat("https://graph.facebook.com/me?access_token=", access_token));
       //                //一樣~回傳的是個json string所以要把他轉Jobject
       //                JObject returnJob = JsonConvert.DeserializeObject<JObject>(_strResult);
       //                //先檢查有沒有我們要的東西
       //                if (returnJob != null && returnJob["email"] != null && returnJob["email"].ToString() != "")
       //                {
       //                    //把email丟給回傳用的變數
       //                    GlobalData.USEREMAIL = returnJob["email"].ToString();
       //                    onCompleteCB.Invoke(GlobalData.USEREMAIL);
       //                   // Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< 此人信箱為：{0} ID為: {1} >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>", returnJob["email"].ToString(), returnJob["id"].ToString());
       //                }
       //            }
       //        }
       //    };
       //}
        //public static string GetWebRequest(string URL) {
        //        Console.WriteLine(URL);
        //        WebRequest MyRequest = WebRequest.Create(URL);                  
        //        MyRequest.Method = "GET";
        //        MyRequest.ContentType = "application/json";
        //        WebResponse MyResponse = MyRequest.GetResponse();
        //        StreamReader sr = new StreamReader(MyResponse.GetResponseStream());
        //        string result = sr.ReadToEnd();
                
        //        sr.Close();
        //        MyResponse.Close();
        //        return result;
        //}
        /// <summary>
        /// <param name="requestJobj"></param>
        ///  JsonObj裡面應該要有 timout(等待延遲的時間)，contexttyp (回傳格式由於我們是用json，所以這邊預設會是用json )
        ///  headers 則是因為有可能會有多筆，所以要用個Jarry去包，長得像下面這樣
        /// {
        ///     "contenttype": "application/json",
        ///     "url": "id=3636"
        ///     "requesttext": "id=3636"
        ///     "timeout": 5000,
        ///     "headers": [
        ///         {
        ///             "x-auth-token": "sphinx:1a97b014338aba192df9c85900fcb8f247319dbf",
        ///             "x-service-id": "rIDUJ45MgU0aqOxOZyZU8CpLqlKxhXB8okNQoZKoyahN2NOfc2GRlwYcHiSBeWcD"
        ///         }
        ///     ]
        /// } 
        /// 他會直接回給你一個JsonObj
        /// </summary>       
        public static JObject GetRequest(JObject requestJobj)
        {
            string result = "";
            if (requestJobj != null)
            {
                HttpWebRequest link;
                Debug.LogError(requestJobj["url"]);
                if (requestJobj["url"] != null && requestJobj["url"].ToString() != "")
                {
                    if (requestJobj["requesttext"] != null && requestJobj["requesttext"].ToString() != "")
                    {
                        string url = requestJobj["url"].ToString().Substring(requestJobj["url"].ToString().Length-1) != "?" ? string.Concat(requestJobj["url"].ToString(),"?") : requestJobj["url"].ToString();
                        link = HttpWebRequest.Create(string.Concat(url, requestJobj["requesttext"])) as HttpWebRequest;
                    }
                    else
                    {
                        link = HttpWebRequest.Create(string.Concat(requestJobj["url"].ToString())) as HttpWebRequest;
                    }
                    //對API request的方式，看到funtion name了嗎，所以用GET
                    link.Method = "GET";
                    //GET的方式如果有需要帶一些欄位資料，則是用 ? id=?????? & name=XXXXXXX的方式  ，
                    //由於我們在接收jsonobj的時候把url 跟給的欄位資料分開，我們必須要去try看看url最
                    //後是否有加個 "?" 的符號
                    link.ContentType = requestJobj["content"] != null && requestJobj["content"].ToString() != "" ? requestJobj["content"].ToString() : "application/json";
                    //link.Timeout = Convert.ToInt32(requestJobj["timeout"].ToString());
                    link.KeepAlive = true;
                    
                    if (requestJobj["headers"] != null)
                    {
                        JObject headersJarry = (JObject)requestJobj["headers"];
                        if (headersJarry.Count > 0)
                        {
                            foreach (KeyValuePair<string, JToken> item in headersJarry)
                            {
                                string key = item.Key;
                                JToken jToken = item.Value;
                                link.Headers[key] = jToken.ToString();
                            }                                                            
                        }
                    }
                    using (HttpWebResponse respone = link.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader sr = new StreamReader(respone.GetResponseStream()))
                        {
                            result = sr.ReadToEnd();
                        }
                        Debug.LogError(" 連線狀態： " + respone.StatusCode);
                    }
                }
            }
            Debug.LogError(result);
            JObject returnJob = JsonConvert.DeserializeObject<JObject>(result);
            return returnJob;
        }
        /// <summary>
        /// <param name="requestJobj"></param>
        ///  JsonObj裡面應該要有 timout(等待延遲的時間)，contexttyp (回傳格式由於我們是用json，所以這邊預設會是用json )
        ///  headers 則是因為有可能會有多筆，所以要用個Jarry去包，長得像下面這樣
        /// {
        ///     "contenttype": "application/json",
        ///     "url": "id=3636"
        ///     "requesttext": "id=3636"
        ///     "timeout": 5000,
        ///     "headers": [
        ///         {
        ///             "x-auth-token": "sphinx:1a97b014338aba192df9c85900fcb8f247319dbf",
        ///             "x-service-id": "rIDUJ45MgU0aqOxOZyZU8CpLqlKxhXB8okNQoZKoyahN2NOfc2GRlwYcHiSBeWcD"
        ///         }
        ///     ]
        /// } 
        /// 他會直接回給你一個JsonObj
        /// </summary>
        public static JObject PostRequestJobj(JObject requestJobj)
        {
            string result = "";
            string senderjobj = "";

            if (requestJobj != null)
            {
                HttpWebRequest link;
                if (requestJobj["url"] != null && requestJobj["url"].ToString() != "")
                {
                    link = HttpWebRequest.Create(requestJobj["url"].ToString()) as HttpWebRequest;
                    link.Method = "POST";
                    link.ContentType = requestJobj["contenttype"] != null && requestJobj["contenttype"].ToString() != "" ? requestJobj["contenttype"].ToString() : "application/json";
                    //link.Timeout = Convert.ToInt32(requestJobj["timeout"].ToString());
                    link.KeepAlive = true;
                    if (requestJobj["requesttext"] != null && requestJobj["requesttext"].ToString() != "")
                    {
                       senderjobj = requestJobj["requesttext"].ToString();
                    }
                    if (requestJobj["headers"] != null)
                    {
                        JObject headersJarry = (JObject)requestJobj["headers"];
                        if (headersJarry.Count > 0)
                        {
                            foreach (KeyValuePair<string, JToken> item in headersJarry)
                            {
                                string key = item.Key;
                                JToken jToken = item.Value;
                                link.Headers[key] = jToken.ToString();
                            }
                        }                                    
                    }
                    byte[] bs = Encoding.UTF8.GetBytes(senderjobj);

                    link.ContentLength = bs.Length;

                    using (Stream reqStream = link.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                    }
                    using (WebResponse response = link.GetResponse())
                    {
                        StreamReader sr = new StreamReader(response.GetResponseStream());
                        result = sr.ReadToEnd();
                        sr.Close();
                    }
                }
            }
            JObject returnJob = JsonConvert.DeserializeObject<JObject>(result);
            return returnJob;
        }
        public static JArray PostRequestJarray(JObject requestJobj)
        {
            string result = "";
            string senderjobj = "";

            if (requestJobj != null)
            {
                HttpWebRequest link;
                if (requestJobj["url"] != null && requestJobj["url"].ToString() != "")
                {
                    link = HttpWebRequest.Create(string.Concat(requestJobj["url"].ToString())) as HttpWebRequest;
                    link.Method = "POST";
                    link.ContentType = "application/json";
                    //link.Timeout = Convert.ToInt32(requestJobj["timeout"].ToString());
                    link.KeepAlive = true;
                    if (requestJobj["requesttext"] != null && requestJobj["requesttext"].ToString() != "")
                    {
                        senderjobj = requestJobj["requesttext"].ToString();
                    }
                    if (requestJobj["headers"] != null)
                    {
                        JObject headersJarry = (JObject)requestJobj["headers"];
                        if (headersJarry.Count > 0)
                        {
                           // for (int i = 0; i < headersJarry.Count; i++)
                            {
                                //JObject headersJobj = (JObject)headersJarry[i];
                                if (headersJarry.Count > 0)
                                {
                                    foreach (KeyValuePair<string, JToken> item in headersJarry)
                                    {
                                        string key = item.Key;
                                        JToken jToken = item.Value;
                                        link.Headers[key] = jToken.ToString();
                                    }
                                }
                            }
                        }
                    }
                    byte[] bs = Encoding.ASCII.GetBytes(senderjobj);

                    link.ContentLength = bs.Length;

                    using (Stream reqStream = link.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                    }
                    using (WebResponse response = link.GetResponse())
                    {
                        StreamReader sr = new StreamReader(response.GetResponseStream());
                        result = sr.ReadToEnd();
                        sr.Close();
                    }
                }
            }
            Console.WriteLine(result);
            JArray returnJob = JsonConvert.DeserializeObject<JArray>(result);
            return returnJob;
        }

        /// <summary>
        /// 發送faceBook貼文
        /// </summary>
        /// <param name="sendMsg">使用者的留言</param>
        /// <param name="sendLink">內嵌連結</param>
        /// <param name="sendName">內嵌連結你要給的名稱</param>
        /// <param name="sendPic">內嵌連結的圖片</param>
        /// <param name="sendCap">內嵌連結的內文</param>
        /// <param name="sendDes">內嵌連結的解說</param>
        //public static void postfacebookFeed(string sendMsg, string sendLink, string sendName, string sendPic, string sendCap, string sendDes)
        //{
        //    if (GlobalData.FACEBOOKTOKEN != null && GlobalData.FACEBOOKTOKEN != "" &&GlobalData.FACEBOOKTOKEN != string.Empty)
        //    {
        //        //宣告一個FB發送器
        //        Facebook.FacebookClient fb = new Facebook.FacebookClient(GlobalData.FACEBOOKTOKEN);
        //        //貼文至塗鴉牆XD
        //        dynamic result = fb.Post("me/feed", new { message = sendMsg, link = sendLink, name = sendName, picture = sendPic, caption = sendCap, description = sendDes });

        //    }
        //    else
        //    {
        //        _auth = new OAuth2Authenticator(
        //               clientId: GlobalData.FACEBOOKCLIENTID,
        //               scope: "email,publish_actions",
        //               authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
        //               redirectUrl: new Uri(GlobalData.FACEBOOKREDIRECTURL));
        //        Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
        //        MemoryManager.showStatus();

        //        //叫出登入授權畫面
        //        GlobalData.ACTIVITY.StartActivity(_auth.GetUI(GlobalData.ACTIVITY));
        //        MemoryManager.showStatus();

        //        //如果token回來了便會進到這個小funtion
        //        _auth.Completed += (s, e) =>
        //        {
        //            {   //先檢查看看是否真的有事件被觸發
        //                if (e.IsAuthenticated)
        //                {   //宣告一個變數來存偷啃
        //                    string access_token = "沒有偷啃";
        //                    //把偷啃丟給剛宣告的變數
        //                    access_token = e.Account.Properties["access_token"];
        //                    GlobalData.FACEBOOKTOKEN = access_token;
        //                    //拿偷啃去要我們要的資料拉
        //                    _strResult = GetWebRequest(string.Concat("https://graph.facebook.com/me?access_token=", access_token));
        //                    //一樣~回傳的是個json string所以要把他轉Jobject
        //                    JObject returnJob = JsonConvert.DeserializeObject<JObject>(_strResult);
        //                    //先檢查有沒有我們要的東西
        //                    if (returnJob != null && returnJob["email"] != null && returnJob["email"].ToString() != "")
        //                    {
        //                        //宣告一個FB發送器
        //                        Facebook.FacebookClient fb = new Facebook.FacebookClient(GlobalData.FACEBOOKTOKEN);
        //                        //貼文至塗鴉牆XD
        //                        dynamic result = fb.Post("me/feed", new { message = sendMsg, link = sendLink, name = sendName, picture = sendPic, caption = sendCap, description = sendDes });
        //                        //把email丟給回傳用的變數
        //                        GlobalData.USEREMAIL = returnJob["email"].ToString();
        //                        sempleToast.createToast("已分享至FaceBook!!",GlobalData.TRANSPARENT,Color.White);
        //                    }
        //                }
        //            }
        //        };                
        //    }
        //}
        /// <summary>
        /// Line分享
        /// </summary>
        /// <param name="sendMsg">與分享的連結或訊息</param>
        //public static void lineShare(string sendMsg)
        //{
        //    GlobalData.ACTIVITY.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse(string.Concat("http://line.naver.jp/R/msg/text/?", sendMsg))));
        //}

        public static JObject Api(string method,string path, JObject jobj)
        {
            string result = "";

            string json = ConvertManager.JsonToString(jobj);
            byte[] databyte = System.Text.Encoding.UTF8.GetBytes(json);

            HttpWebRequest link;

            link = HttpWebRequest.Create(GlobalData.APIADDRESS + path) as HttpWebRequest;
            link.Method = method;

            link.ContentType = "application/json";
            //link.Timeout = Convert.ToInt32(requestJobj["timeout"].ToString());
            link.KeepAlive = true;

            link.ContentLength = databyte.Length;

            using (Stream reqStream = link.GetRequestStream())
            {
                reqStream.Write(databyte, 0, databyte.Length);
            }
            using (WebResponse response = link.GetResponse())
            {
                StreamReader sr = new StreamReader(response.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
            }

            JObject returnJob = JsonConvert.DeserializeObject<JObject>(result);
            return returnJob;
        }


        public static string ApiString(string method, string path, JObject jobj)
        {
            string result = "";

            string json = ConvertManager.JsonToString(jobj);
            byte[] databyte = System.Text.Encoding.UTF8.GetBytes(json);

            HttpWebRequest link;

            link = HttpWebRequest.Create(GlobalData.APIADDRESS + path) as HttpWebRequest;
            link.Method = method;

            link.ContentType = "application/json";
            //link.Timeout = Convert.ToInt32(requestJobj["timeout"].ToString());
            link.KeepAlive = true;

            link.ContentLength = databyte.Length;

            using (Stream reqStream = link.GetRequestStream())
            {
                reqStream.Write(databyte, 0, databyte.Length);
            }
            using (WebResponse response = link.GetResponse())
            {
                StreamReader sr = new StreamReader(response.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
            }

            return result;
        }


        public static void authOfacebookLogin()
        { 
        
        }
    }
}