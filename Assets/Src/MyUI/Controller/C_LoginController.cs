using System.Collections;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using Assets.Src.MyUI.Events;
using Assets.Src.MyUI.Model;
using UnityEngine;

namespace Assets.Src.MyUI.Controller
{

[System.Serializable]
public class C_LoginController :  SimpleCommand
{

        public override void Execute(INotification note)
        {
            UIProxy uiProxy = Facade.RetrieveProxy(UIProxy.name()) as UIProxy;
            if (note.Type != null)
            {
              switch (note.Type)
              {
                case LoginEvents.Login_Create:
                    break;
                case LoginEvents.Login_Open:
                    break;
                case LoginEvents.Login_Update:
                    break;
                case LoginEvents.Login_Close:
                    break;
                default: 
                    break;
            }
        }
            BIDProxy bidProxy = Facade.RetrieveProxy(BIDProxy.name()) as BIDProxy;
            if (note.Type != null)
            {
            }
        }


}
}