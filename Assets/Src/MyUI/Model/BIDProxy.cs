using System;
using PureMVC.Patterns;
using Assets.Src.MyUI.Events;
using Assets.Src.MyUI.View;

namespace Assets.Src.MyUI.Model
{
	public class BIDProxy : Proxy 
	{
		/** �W�� **/
		public const string _NAME = "bid_proxy";
		
		public BIDProxy (string proxyName = null, object data = null)
			: base(_NAME, data)
		{}
		

		/** �W�� **/
		public static string name()			
		{
			return _NAME;
        }



    }
}

