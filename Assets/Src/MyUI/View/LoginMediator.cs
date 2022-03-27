using UnityEngine;
using System;
using System.Collections;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using Assets.Src.MyUI.Events;
using System.Collections.Generic;

namespace Assets.Src.MyUI.View
{

[System.Serializable]
public class LoginMediator :  Mediator
{
        public LoginMediator(string NAME = null, object viewComponent = null)
                    : base(NAME, viewComponent)
        {
        }
        ///<summary>
        /// 註冊監聽對象
        ///<summary>
        public override IList<string> ListNotificationInterests()
        {
            return new List<string>(new string[] {            
                LoginEvents.Login_Create,
                LoginEvents.Login_Open,
                LoginEvents.Login_Update,
                LoginEvents.Login_Close,
			});
        }
        ///<summary>
        /// 處理發送過來的資訊
        ///<summary>
        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case LoginEvents.Login_Create:
                    CreateView();
                    CloseView();
                    break;
                case LoginEvents.Login_Open:
                    OpenView();
                    InitView();
                    break;
                case LoginEvents.Login_Update:
                    UpdateView();
                    break;
                case LoginEvents.Login_Close:
                    CloseView();
                    break;
            }
        }
        ///<summary>
        /// 建立畫面
        ///<summary>
        private void CreateView()
        {
            try
            {


            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        ///<summary>
        /// 開啟畫面
        ///<summary>
        private void OpenView()
        {
            try
            {


            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        ///<summary>
        /// 畫面執行中
        ///<summary>
        private void InitView()
        {
            try
            {


            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        ///<summary>
        /// 更新畫面
        ///<summary>
        private void UpdateView()
        {
            try
            {


            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        ///<summary>
        /// 關閉畫面
        ///<summary>
        private void CloseView()
        {
            try
            {


            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

}
}