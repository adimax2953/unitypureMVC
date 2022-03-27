using System;
using PureMVC.Patterns;
using Assets.Src.MyUI.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Assets.Src.MyUI.Model
{
	public class UIProxy : Proxy
	{
		///<summary>
		/// �W��
		///</summary>
		public const string _NAME = "ui_proxy";

        // �إߵe��
        private ScreenView _screenSenderCreate;
        // �����e��
        private ScreenView _screenSenderClose;
		public UIProxy (string proxyName = null, object data = null)
			: base(_NAME, data)
		{
            _screenSenderCreate = ScreenFactory.SetScreenView("Create");
            _screenSenderClose = ScreenFactory.SetScreenView("Close");
        }
		
		///<summary>
		/// �W��
		///</summary>
		public static string name()			
		{
			return _NAME;
		}


        private string getName(string name)
        {
            return name.Substring(0, name.Length - 5);
        }

        
    }
}

