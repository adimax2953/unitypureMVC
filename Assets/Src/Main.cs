using UnityEngine;
using System.Collections;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using Assets.Src;
using Assets.Src.MyUI.Events;
using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Main : MonoBehaviour
{
    ReleaseModeCreatView _releaseModeCreateView;
    void Awake()
    {

    }


    // Use this for initialization
    void Start()
    {
        // 啟動 puremvc
        UIFacade facade = UIFacade.Instance as UIFacade;
        facade.Startup(this);

        // 建立畫面
        _releaseModeCreateView = new ReleaseModeCreatView();

    }

    
    void Update()
    {
        
    }
}

    

