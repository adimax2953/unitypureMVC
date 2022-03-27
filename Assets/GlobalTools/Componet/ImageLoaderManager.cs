using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;



    public class ImageLoaderManager : MonoBehaviour
    {
        // 委託事件 - 成功 
        public delegate void DelegateCB(string key, WWW sbytes);
        // 委託物件 - 成功
        public static DelegateCB onCompleteCB;
        //load圖線程
        private static Thread _loadThread;
        //load圖線程起始funtion
        private static ThreadStart _threadStart;
        //拿來存放delegate
        private static Dictionary<string, Delegate> _delegateList;
        //load圖排隊的list
        private static ArrayList _loadList;
        /// <summary>
        /// 這裡創建一個load圖請求
        /// </summary>
        /// <param name="key">該物件的編號，這裡比較取向於設定你拿到圖片後，所要設定的物件ID</param>
        /// <param name="url">便是圖片資源的路徑了</param>
        /// <param name="cbFunc">回傳到哪個funtion</param>
        public static void CreateLoad(string key, string url, System.Delegate cbFunc)
        {
            //先指定線程的起始funtion
            _threadStart = Load;
            //檢查資料用的funtion
            checkList(cbFunc, key, url);
            //這裡我想不必解釋了，如果不做這檢查，他會狂new 線程 ，如果我對他要求20次他就給我開20次，大概CPU就擠爆了XD
            if (_loadThread == null)
            {
                _loadThread = new Thread(_threadStart);
            }
            //在啟動前要先看一下，這條線程是不是已經在使用中，如果不這樣做，那你就會不斷的重新開始
            if (_loadThread.IsAlive == false)
            {   //是否使用被警執行
                _loadThread.IsBackground = true;
                //啟動線程
                _loadThread.Start();
            }
        }
        /// <summary>
        /// 開始load圖
        /// </summary>
        private static void Load()
        {
            JObject jsonObj;
            string url = "";
            string key = "";
            if (_loadList != null && _loadList.Count > 0)
            {
                for (int i = 0; i < _loadList.Count; i++)
                {
                    // 取出元件
                    jsonObj = (JObject)_loadList[0];
                    // 刪除第一筆
                    _loadList.RemoveAt(0);

                    if (jsonObj != null)
                    {
                        // 取出URL
                        url = (string)jsonObj["url"];
                        // 取出KEY
                        key = (string)jsonObj["key"];
                    }

                    onCompleteCB = (DelegateCB)_delegateList[key];

                    WWW imageBitmap = new WWW(url);
                    if (imageBitmap.error != null)
                    {
                        if (key.Length > 0 && _delegateList != null && _delegateList.Count > 0)
                        {
                            // 取出回傳委託
                            onCompleteCB = (DelegateCB)_delegateList[key];
                            // 回傳讀取失敗訊息
                            onCompleteCB.Invoke("error", imageBitmap);
                            imageBitmap.Dispose();                        
                        }

                    }
                    else if (imageBitmap.isDone)
                    {
                        if (key.Length > 0 && _delegateList != null && _delegateList.Count > 0)
                        {
                            onCompleteCB.Invoke(key, imageBitmap);                        
                        }
                        imageBitmap.Dispose();
                        i--;
                    }
                }
            }
        }
        /// <summary>
        /// 取得圖片檔案
        /// </summary>
        /// <param name="url">圖片連結</param>
        /// <returns></returns>
        //private static WWW GetImageBitmapFromUrl(string url)
        //{
        //    WWW imageBitmap = null;
        //    if (!(url == "null"))
        //    {
        //        using (WebClient webClient = new WebClient())
        //        {
        //            var imageBytes = webClient.DownloadData(url);
        //            if (imageBytes != null && imageBytes.Length > 0)
        //            {
        //                imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
        //            }
        //        }
        //    }
        //    return imageBitmap;
        //}
        public static void closeLoad()
        {
            //_loadThread.Abort();
            //_loadThread.Join(5000);
        }
        /// <summary>
        /// 檢查資料
        /// </summary>
        /// <param name="delcb">回傳funtion對象</param>
        /// <param name="key">當筆物件索引</param>
        /// <param name="url">當筆連結</param>
        private static void checkList(Delegate delcb, string key, string url)
        {
            if (_loadList == null)
            {//初始化 排隊列表
                _loadList = new ArrayList();
            }
            if (_delegateList == null)
            {//初始化回傳物件列表
                _delegateList = new Dictionary<string, Delegate>();
            }
            //建立一個jsonobj來存放索引和連結
            JObject jsonObj = new JObject { { "key", key }, { "url", url } };

            if (_loadList != null && _loadList.Count > 0)
            {
                for (int i = 0; i < _loadList.Count; i++)
                {
                    JObject trmpJsonObj = (JObject)_loadList[i];
                    string tempKey = null;

                    if (trmpJsonObj != null)
                    {
                        // 取出Key
                        tempKey = (string)trmpJsonObj["key"];
                    }

                    if (tempKey != null && tempKey.Length > 0 && tempKey == key)
                    {
                        // 有重複，就砍掉，加入新的至最尾端
                        _loadList.RemoveAt(i);
                        // 找到就可以跳出
                        break;
                    }
                }
            }
            if (jsonObj != null)
            {
                _delegateList.Add(jsonObj["key"].ToString(), delcb);
                // 將資料放入列表
                _loadList.Add(jsonObj);
            }
        }

    

    /*
    public static void createLocalLoad(UITexture item ,string path)
    {
       localLoad(item,path);
    }    


    private static void localLoad(UITexture item, string path)
    {
        
        Texture2D texture2D = new Texture2D(0, 0);
        byte[] imageBytes = File.ReadAllBytes(path);
        texture2D.LoadImage(imageBytes);
        item.mainTexture = texture2D;
        texture2D = null;
        Resources.UnloadUnusedAssets();

    }*/
    

}
