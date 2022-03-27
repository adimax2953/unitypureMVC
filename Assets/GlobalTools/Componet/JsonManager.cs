using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class JsonManager : MonoBehaviour {

    /** 委託事件 - 成功 **/
    public delegate void DelegateCB( JObject jobj);
    /** 委託物件 - 成功 **/
    public static DelegateCB onCompleteCB;

    public enum HttpMethod
    {
        Get,Post
    }


    private static JsonManager mInstance;

    public static JsonManager instance
    {
        get { return mInstance; }
    }

    private JObject _returnObj = null;

    void Awake()
    {
        mInstance = this;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadJson(string apiUrl, Delegate CbFun)
    {

        onCompleteCB = (DelegateCB)CbFun;
        StartCoroutine(startLoad(apiUrl));
    }

    IEnumerator startLoad(string apiUrl)
    {

        WWW www = new WWW(apiUrl);

        yield return www;
        if (www.isDone)
        {
            Debug.LogError(www.text);

            _returnObj = JsonConvert.DeserializeObject<JObject>(www.text);
            onCompleteCB.Invoke(_returnObj);
        }

    }



}
