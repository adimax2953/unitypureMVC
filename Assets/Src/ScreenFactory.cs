using Assets.Src;
using System.Collections;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

public abstract class ScreenView
{
    public abstract void None(string name);
    public abstract void Value(string name, object jsonObj = null);
    public abstract void Open(string name, object jsonObj = null);
}

public class CreateMethod : ScreenView
{
    public override void None(string name)
    {
        UIFacade.Instance.SendNotification(name + "_Create");
    }

    public override void Value(string name, object jsonObj = null)
    {
        UIFacade.Instance.SendNotification(name + "_Create");
        UIFacade.Instance.SendNotification(name + "_Open", (object)jsonObj);
    }
    public override void Open(string name, object jsonObj = null)
    {
        UIFacade.Instance.SendNotification(name + "_Open", (object)jsonObj);
    }
}

public class CloseMethod : ScreenView
{
    public override void None(string name)
    {
        UIFacade.Instance.SendNotification(name + "_Close");
    }

    public override void Value(string name, object jsonObj = null)
    {
        UIFacade.Instance.SendNotification(name + "_Close", (object)jsonObj);
    }
    public override void Open(string name, object jsonObj = null)
    {
        UIFacade.Instance.SendNotification(name + "_Close", (object)jsonObj);
    }
}

public class ScreenFactory
{
    public static ScreenView SetScreenView(string mode)
    {
        ScreenView screenView = null;
        if (mode.Equals("Create"))
        {
            screenView = new CreateMethod();
        }
        else if (mode.Equals("Close"))
        {
            screenView = new CloseMethod();
        }
        return screenView;
    }
}

// �ϥνd��
public class efefe
{
    public efefe()
    {
        ScreenView agg = ScreenFactory.SetScreenView("Close");
        agg.None("aaaaaaa");
        
    }
}

