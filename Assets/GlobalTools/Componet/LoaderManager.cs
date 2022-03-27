using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoaderManager : MonoBehaviour {

    /** 讀取資料列表 **/
    public static ArrayList LoadDataList;
    /** 回傳函式列表 **/
    public static Dictionary<String, Delegate> cbDic;
    /** 委託事件 - 成功 **/
    public delegate void DelegateCB(String key, WWW www);
    /** 委託物件 - 成功 **/
    public static DelegateCB onCompleteCB;
    /** 委託事件 - 失敗 **/
    public delegate void DelegateErrorCB(String Key, WWW www);
    /** 委託物件 - 失敗 **/
    public static DelegateErrorCB onErrorCB;
    /** 延遲間隔 **/
    private float _TIME_DELAY_MAX = 0.05f;
    /** 計時器 **/
    private float _time = 0f;
    /** 計算次數 **/
    private int _timeNum = 0;
    /** 是否已讀取完畢 **/
    private static Boolean _isLoading;
	/// <summary>
	/// 是否停止讀取
	/// </summary>
	private static bool _isStopLoading;

    void Awake()
    {
        //Debug.Log("Awake");
        // 初始化
        if (LoadDataList == null)
        {
            LoadDataList = new ArrayList();
        }

        cbDic = new Dictionary<string, Delegate>();
    }

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
        // 計時器
        if ((_time += Time.deltaTime) > _TIME_DELAY_MAX)
        {
            // 初始化
            _time = 0;

            // 增加次數
            _timeNum++;
        }
        else
        {
            return;
        }

		if (_isStopLoading) {
			stopCoroutines();
		}

        if (!_isLoading)
        {
            // 開始讀取資料
            startLoadData();
        }
	}

    /// <summary>
    /// 開始讀取資料
    /// </summary>
    private void startLoadData()
    {
        JObject jsonObj;
        string url = "";
        string key = "";        

        if (LoadDataList != null && LoadDataList.Count > 0)
        {
            // 讀取中
            _isLoading = true;

            // 取出元件
            jsonObj = (JObject)LoadDataList[0];

            // 刪除第一筆
            LoadDataList.RemoveAt(0);

            if (jsonObj != null)
            {
                // 取出URL
                url = (string)jsonObj["url"];
                // 取出KEY
                key = (string)jsonObj["key"];
            }
        }

        if (url != "" && key != "")
        {
            // 開啟同步協程
            StartCoroutine(startLoad(key, url));
        }        
    }

    /// <summary>
    /// 開始讀取
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IEnumerator startLoad(String key, String url)
    {
        WWW www = new WWW(url);
        yield return www;

        // 讀取完畢
        _isLoading = false;
        // 讀取失敗
        if (www.error != null)
        {
            if (key.Length > 0 && cbDic != null && cbDic.Count > 0)
            {
                // 取出回傳委託
                onCompleteCB = (DelegateCB)cbDic[key];
                // 回傳讀取失敗訊息
                onCompleteCB.Invoke("error", www);
                www.Dispose();
            }
        }
        else if (www.isDone)
        {
            // 讀取成功
            if (key.Length > 0 && cbDic != null && cbDic.Count > 0)
            {
                // 取出回傳委託
                onCompleteCB = (DelegateCB)cbDic[key];
                // 回傳讀取成功資料
                onCompleteCB.Invoke(key, www);
                www.Dispose();
            }
        }
    }

    /// <summary>
    /// 創建Load
    /// </summary>
    /// <param name="key"></param>
    /// <param name="url"></param>
    /// <param name="CbFun"></param>
    public static void createLoad(String key, String url, Delegate CbFun)
    {
        if (key == "" || url == "" && CbFun != null)
        {
            return;
        }
        
		_isStopLoading = false;

        // 打包資料
        JObject jsonObj = new JObject {
            { "key", key } ,{ "url", url}
        };

        if (LoadDataList == null)
        {
            // 初始化
            LoadDataList = new ArrayList();
        }
        if (LoadDataList != null && LoadDataList.Count > 0)
        {
            for (int i = 0; i < LoadDataList.Count; i++)
            {
                JObject trmpJsonObj = (JObject)LoadDataList[i];
                string tempKey = null;

                if (trmpJsonObj != null)
                {
                    // 取出Key
                    tempKey = (string)trmpJsonObj["Key"];
                }

                if (tempKey != null && tempKey.Length > 0 && tempKey == key)
                {
                    // 有重複，就砍掉，加入新的至最尾端
                    LoadDataList.RemoveAt(i);
                    // 找到就可以跳出
                    break;
                }
            }
        }
        if (jsonObj != null)
        {
            // 將資料放入列表
            LoadDataList.Add(jsonObj);
        }
        // 移除舊資料
        cbDic.Remove(key);
        // 建立引索
        cbDic.Add(key, CbFun);
    }

	/// <summary>
	/// 停止load
	/// </summary>
	public static void stopLoad()
	{
		_isStopLoading = true;
        _isLoading = false;
    }
	/// <summary>
	/// 停止所有startLoad
	/// </summary>
	private void stopCoroutines()
	{
		if (LoadDataList != null) {
			LoadDataList.Clear();
		}
		StopCoroutine("startLoad");
	}
}
