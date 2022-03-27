using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using Assets.Src.MyUI.Model;
using Assets.Src.MyUI.View;
using Assets.Src.MyUI.Events;
namespace Assets.Src.MyUI.Controller
{
	public class StartupCommand : SimpleCommand
	{
		public override void Execute(INotification note)
        {		
			/** Register Proxy Start **/
			
			Facade.RegisterProxy( new UIProxy());
			Facade.RegisterProxy( new BIDProxy());
			
			/** Register Proxy End **/
			
			/** Register Mediator Start **/
            //�ϥΪ̵n�J����

			            Facade.RegisterMediator(new LoginMediator(LoginEvents.Login_Name, note.Body));
 			//$MemberFields

            
            /** Register Mediator End **/
        }
    }
}