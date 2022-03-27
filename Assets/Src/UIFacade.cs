using UnityEngine;
using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using Assets.Src.MyUI.Events;
using Assets.Src.MyUI.Controller;

namespace Assets.Src
{
    public class UIFacade : Facade
    {
        new public static IFacade Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (/*m_syncRoot*/m_staticSyncRoot) 
                    {
                        if (m_instance == null) m_instance = new UIFacade();
                    }
                }
                return m_instance;
            }
        }

        public void Startup(UnityEngine.Object app)
        {
            NotifyObservers(new Notification(UIEvents.MAIN_START_UP, app));
        }

        static UIFacade()
        { }

        protected override void InitializeController()
        {
            base.InitializeController();
            //Debug.Log("initController");
            RegisterCommand(UIEvents.MAIN_START_UP, typeof(StartupCommand));

			RegisterCommand(LoginEvents.Login_Geneal_Result, typeof(C_LoginController));
 			//$MemberFields


            //RegisterCommand(LoginRegisterEvents.LOGIN_REGISTER_GENEAL_RESULT, typeof(c_LoginRegisterController));
        }
    }
}