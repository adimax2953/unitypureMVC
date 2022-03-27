using System.Collections;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using Assets.Src;
using Assets.Src.MyUI.Events;

public class ReleaseModeCreatView
{
    public ReleaseModeCreatView()
    {
        ScreenView screenSenderCreate = ScreenFactory.SetScreenView("Create");

		        screenSenderCreate.None(LoginEvents.Login_Name.Substring(0,LoginEvents.Login_Name.Length - 5));
 			//$MemberFields


        //screenSenderCreate.Value(OpreationEvents.OPREATION_NAME.Substring(0, OpreationEvents.OPREATION_NAME.Length - 5));
    }
}

