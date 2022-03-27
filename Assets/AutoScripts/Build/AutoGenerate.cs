using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class AutoGenerate : MonoBehaviour
{

    private string ClassName;
    public InputField InputTarget;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnValueChange()
    {
        ClassName = InputTarget.text;
    }

    public void OnClickBasic()
    {
        MainMake();
        UIEventsMake();
        ScreenViewMake();
        UIFacadeMake();
        UIProxyMake();
        BIDProxyMake();
        RelaseMake();
        StarupCommandMake();
    }
    public void OnClickMvc()
    {
        UIFacadeUpdate();
        MediatorMake();
        EventsMake();
        ControllerMake();
        RelaseUpdate();
        StarupCommandUpdate();
    }
    /// <summary>
    /// 更新StarupCommand
    /// </summary>
    void StarupCommandUpdate()
    {
        Debug.LogError("StartupCommand be Update " + ClassName + "'s Mediator");
        if (ClassName == null || ClassName == string.Empty || ClassName == "")
        {
            Debug.LogError("請輸入ClassName");
            return;
        }
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Src/MyUI/Controller/" + string.Concat("StartupCommand") + ".cs");
        string template = textAsset.ToString();


        StringBuilder sb = new StringBuilder();

        sb.Append(string.Concat("            Facade.RegisterMediator(new ", ClassName, "Mediator(", ClassName, "Events.", ClassName, "_Name, note.Body));\n 			//$MemberFields"));

        template = template.Replace("//$MemberFields", sb.ToString());

        if (System.IO.File.Exists("Assets/Src/MyUI/Controller/"))
        {
            using (var writer = new StreamWriter("Assets/Src/MyUI/Controller/" + string.Concat("StartupCommand") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI/Controller/");
            using (var writer = new StreamWriter("Assets/Src/MyUI/Controller/" + string.Concat("StartupCommand") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成StarupCommand
    /// </summary>
    void StarupCommandMake()
    {
        Debug.LogError("StartupCommand be Created");
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/StartupCommandTemplate.txt");
        string template = textAsset.ToString();
        if (!System.IO.File.Exists("Assets/Src/MyUI"))
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI");

        }
        if (System.IO.File.Exists("Assets/Src/MyUI/Controller/"))
        {
            using (var writer = new StreamWriter("Assets/Src/MyUI/Controller/" + string.Concat("StartupCommand") + ".cs"))
            {
                writer.WriteLine(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI/Controller/");
            using (var writer = new StreamWriter("Assets/Src/MyUI/Controller/" + string.Concat("StartupCommand") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 更新Relase
    /// </summary>
    void RelaseUpdate()
    {
        Debug.LogError("ReleaseModeCreatView be Update " + ClassName + "'s Events");
        if (ClassName == null || ClassName == string.Empty || ClassName == "")
        {
            Debug.LogError("請輸入ClassName");
            return;
        }
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Src/" + string.Concat("ReleaseModeCreatView") + ".cs");
        string template = textAsset.ToString();


        StringBuilder sb = new StringBuilder();

        sb.Append(string.Concat("        screenSenderCreate.None(", ClassName, "Events.", ClassName, "_Name.Substring(0,", ClassName, "Events.", ClassName, "_Name.Length - 5));\n 			//$MemberFields"));

        template = template.Replace("//$MemberFields", sb.ToString());
        if (System.IO.File.Exists("Assets/Src/"))
        {
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("ReleaseModeCreatView") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/");
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("ReleaseModeCreatView") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成Relase
    /// </summary>
    void RelaseMake()
    {
        Debug.LogError("ReleaseModeCreatView be Created");
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/ReleaseModeCreatViewTemplate.txt");
        string template = textAsset.ToString();

        if (System.IO.File.Exists("Assets/Src/"))
        {
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("ReleaseModeCreatView") + ".cs"))
            {
                writer.WriteLine(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/");
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("ReleaseModeCreatView") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 更新UIFacade
    /// </summary>
    void UIFacadeUpdate()
    {
        Debug.LogError("UIFacade be Update "+ ClassName+"'s Command");
        if (ClassName == null || ClassName == string.Empty || ClassName == "")
        {
            Debug.LogError("請輸入ClassName");
            return;
        }
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Src/" + string.Concat("UIFacade") + ".cs");
        string template = textAsset.ToString();


        StringBuilder sb = new StringBuilder();

        sb.Append(string.Concat("RegisterCommand(", ClassName, "Events.", ClassName, "_Geneal_Result, typeof(C_", ClassName, "Controller));\n 			//$MemberFields"));

        template = template.Replace("//$MemberFields", sb.ToString());
        if (System.IO.File.Exists("Assets/Src/"))
        {
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("UIFacade") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/");
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("UIFacade") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成UIFacade
    /// </summary>
    void UIFacadeMake()
    {
        Debug.LogError("UIFacade be Created");
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/UIFacadeTemplate.txt");
        string template = textAsset.ToString();

        if (System.IO.File.Exists("Assets/Src/"))
        {
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("UIFacade") + ".cs"))
            {
                writer.WriteLine(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/");
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("UIFacade") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成Controller
    /// </summary>
    void ControllerMake()
    {
        Debug.LogError("C_"+ClassName + "Controller be Created");
        if (ClassName == null || ClassName == string.Empty || ClassName == "")
        {
            Debug.LogError("請輸入ClassName");
            return;
        }
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/ControllerTemplate.txt");
        string template = textAsset.ToString();


        StringBuilder sb = new StringBuilder();

        sb.Append(string.Concat("        public override void Execute(INotification note)\n"));
        sb.Append("        {" + "\n");
        sb.Append("            UIProxy uiProxy = Facade.RetrieveProxy(UIProxy.name()) as UIProxy;\n");
        sb.Append("            if (note.Type != null)\n");
        sb.Append("            {" + "\n");

        sb.Append("              switch (note.Type)" + "\n");
        sb.Append("              {" + "\n");

        sb.Append(string.Concat("                ", "case " + ClassName, "Events.", ClassName, "_Create:") + "\n");
        sb.Append("                    break;" + "\n");

        sb.Append(string.Concat("                ", "case " + ClassName, "Events.", ClassName, "_Open:") + "\n");
        sb.Append("                    break;" + "\n");

        sb.Append(string.Concat("                ", "case " + ClassName, "Events.", ClassName, "_Update:") + "\n");
        sb.Append("                    break;" + "\n");

        sb.Append(string.Concat("                ", "case " + ClassName, "Events.", ClassName, "_Close:") + "\n");
        sb.Append("                    break;" + "\n");

        sb.Append("                " + "default: " + "\n");
        sb.Append("                    break;" + "\n");
        sb.Append("            }" + "\n");
        sb.Append("        }" + "\n");


        sb.Append("            BIDProxy bidProxy = Facade.RetrieveProxy(BIDProxy.name()) as BIDProxy;\n");

        sb.Append("            if (note.Type != null)" + "\n");
        sb.Append("            {" + "\n");
        sb.Append("            }" + "\n");
        sb.Append("        }" + "\n");


        template = template.Replace("$ClassName", string.Concat("C_", ClassName, "Controller : ", " SimpleCommand"));
        template = template.Replace("$MemberFields", sb.ToString());
        if (!System.IO.File.Exists("Assets/Src/MyUI"))
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI");

        }

        if (System.IO.File.Exists("Assets/Src/MyUI/Controller/"))
        {
            using (var writer = new StreamWriter("Assets/Src/MyUI/Controller/" + string.Concat("C_", ClassName, "Controller") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI/Controller/");
            using (var writer = new StreamWriter("Assets/Src/MyUI/Controller/" + string.Concat("C_", ClassName, "Controller") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成Mediator
    /// </summary>
    void MediatorMake()
    {
        Debug.LogError(ClassName + "Mediator be Created");
        if (ClassName == null || ClassName == string.Empty || ClassName == "")
        {
            Debug.LogError("請輸入ClassName");
            return;
        }
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/MediatorTemplate.txt");
        string template = textAsset.ToString();


        StringBuilder sb = new StringBuilder();

        sb.Append(string.Concat("        public ", ClassName, "Mediator(string NAME = null, object viewComponent = null)" + "\n"));
        sb.Append("                    : base(NAME, viewComponent)" + "\n");
        sb.Append("        {" + "\n");
        sb.Append("        }" + "\n");

        sb.Append("        ///<summary>" + "\n" + "        /// 註冊監聽對象" + "\n" + "        ///<summary>\n");
        sb.Append("        public override IList<string> ListNotificationInterests()" + "\n");
        sb.Append("        {" + "\n");
        sb.Append("            return new List<string>(new string[] {            " + "\n");
        sb.Append(string.Concat("                ", ClassName, "Events.", ClassName, "_Create,") + "\n");
        sb.Append(string.Concat("                ", ClassName, "Events.", ClassName, "_Open,") + "\n");
        sb.Append(string.Concat("                ", ClassName, "Events.", ClassName, "_Update,") + "\n");
        sb.Append(string.Concat("                ", ClassName, "Events.", ClassName, "_Close,") + "\n");
        sb.Append("			});" + "\n");
        sb.Append("        }" + "\n");

        sb.Append("        ///<summary>" + "\n" + "        /// 處理發送過來的資訊" + "\n" + "        ///<summary>\n");
        sb.Append("        public override void HandleNotification(INotification note)" + "\n");
        sb.Append("        {" + "\n");
        sb.Append("            switch (note.Name)" + "\n");
        sb.Append("            {" + "\n");

        sb.Append(string.Concat("                ", "case " + ClassName, "Events.", ClassName, "_Create:") + "\n");
        sb.Append("                    CreateView();" + "\n");
        sb.Append("                    CloseView();" + "\n");
        sb.Append("                    break;" + "\n");

        sb.Append(string.Concat("                ", "case " + ClassName, "Events.", ClassName, "_Open:") + "\n");
        sb.Append("                    OpenView();" + "\n");
        sb.Append("                    InitView();" + "\n");
        sb.Append("                    break;" + "\n");

        sb.Append(string.Concat("                ", "case " + ClassName, "Events.", ClassName, "_Update:") + "\n");
        sb.Append("                    UpdateView();" + "\n");
        sb.Append("                    break;" + "\n");

        sb.Append(string.Concat("                ", "case " + ClassName, "Events.", ClassName, "_Close:") + "\n");
        sb.Append("                    CloseView();" + "\n");
        sb.Append("                    break;" + "\n");
        sb.Append("            }" + "\n");
        sb.Append("        }" + "\n");


        sb.Append("        ///<summary>" + "\n" + "        /// 建立畫面" + "\n" + "        ///<summary>\n");
        sb.Append("        private void CreateView()" + "\n");
        sb.Append("        {" + "\n");
        sb.Append("            try" + "\n");
        sb.Append("            {" + "\n\n\n");
        sb.Append("            }" + "\n");
        sb.Append("            catch (Exception e)" + "\n");
        sb.Append("            {" + "\n");
        sb.Append("                Debug.LogError(e);" + "\n");
        sb.Append("            }" + "\n");
        sb.Append("        }" + "\n");

        sb.Append("        ///<summary>" + "\n" + "        /// 開啟畫面" + "\n" + "        ///<summary>\n");
        sb.Append("        private void OpenView()" + "\n");
        sb.Append("        {" + "\n");
        sb.Append("            try" + "\n");
        sb.Append("            {" + "\n\n\n");
        sb.Append("            }" + "\n");
        sb.Append("            catch (Exception e)" + "\n");
        sb.Append("            {" + "\n");
        sb.Append("                Debug.LogError(e);" + "\n");
        sb.Append("            }" + "\n");
        sb.Append("        }" + "\n");

        sb.Append("        ///<summary>" + "\n" + "        /// 畫面執行中" + "\n" + "        ///<summary>\n");
        sb.Append("        private void InitView()" + "\n");
        sb.Append("        {" + "\n");
        sb.Append("            try" + "\n");
        sb.Append("            {" + "\n\n\n");
        sb.Append("            }" + "\n");
        sb.Append("            catch (Exception e)" + "\n");
        sb.Append("            {" + "\n");
        sb.Append("                Debug.LogError(e);" + "\n");
        sb.Append("            }" + "\n");
        sb.Append("        }" + "\n");

        sb.Append("        ///<summary>" + "\n" + "        /// 更新畫面" + "\n" + "        ///<summary>\n");
        sb.Append("        private void UpdateView()" + "\n");
        sb.Append("        {" + "\n");
        sb.Append("            try" + "\n");
        sb.Append("            {" + "\n\n\n");
        sb.Append("            }" + "\n");
        sb.Append("            catch (Exception e)" + "\n");
        sb.Append("            {" + "\n");
        sb.Append("                Debug.LogError(e);" + "\n");
        sb.Append("            }" + "\n");
        sb.Append("        }" + "\n");

        sb.Append("        ///<summary>" + "\n" + "        /// 關閉畫面" + "\n" + "        ///<summary>\n");
        sb.Append("        private void CloseView()" + "\n");
        sb.Append("        {" + "\n");
        sb.Append("            try" + "\n");
        sb.Append("            {" + "\n\n\n");
        sb.Append("            }" + "\n");
        sb.Append("            catch (Exception e)" + "\n");
        sb.Append("            {" + "\n");
        sb.Append("                Debug.LogError(e);" + "\n");
        sb.Append("            }" + "\n");
        sb.Append("        }" + "\n");

        template = template.Replace("$ClassName", string.Concat(ClassName, "Mediator : ", " Mediator"));
        template = template.Replace("$MemberFields", sb.ToString());
        if (!System.IO.File.Exists("Assets/Src/MyUI"))
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI");

        }

        if (System.IO.File.Exists("Assets/Src/MyUI/View/"))
        {
            using (var writer = new StreamWriter("Assets/Src/MyUI/View/" + string.Concat(ClassName, "Mediator") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI/View/");
            using (var writer = new StreamWriter("Assets/Src/MyUI/View/" + string.Concat(ClassName, "Mediator") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成Events
    /// </summary>
    void EventsMake()
    {
        Debug.LogError(ClassName+"Events be Created");
        if (ClassName == null || ClassName == string.Empty || ClassName == "")
        {
            Debug.LogError("請輸入ClassName");
            return;
        }
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/EventsTemplate.txt");
        string template = textAsset.ToString();


        StringBuilder sb = new StringBuilder();
        sb.Append("        ///<summary>" + "\n" + "        /// Name" + "\n" + "        ///<summary>\n");
        sb.Append(string.Concat("        public const string ", ClassName, "_Name = ", "\"", ClassName, "_Name", "\"", ";\n"));

        sb.Append("        ///<summary>" + "\n" + "        /// Create" + "\n" + "        ///<summary>\n");
        sb.Append(string.Concat("        public const string ", ClassName, "_Create = ", "\"", ClassName, "_Create", "\"", ";\n"));

        sb.Append("        ///<summary>" + "\n" + "        /// Update" + "\n" + "        ///<summary>\n");
        sb.Append(string.Concat("        public const string ", ClassName, "_Update = ", "\"", ClassName, "_Update", "\"", ";\n"));

        sb.Append("        ///<summary>" + "\n" + "        /// Open" + "\n" + "        ///<summary>\n");
        sb.Append(string.Concat("        public const string ", ClassName, "_Open = ", "\"", ClassName, "_Open", "\"", ";\n"));

        sb.Append("        ///<summary>" + "\n" + "        /// Close" + "\n" + "        ///<summary>\n");
        sb.Append(string.Concat("        public const string ", ClassName, "_Close = ", "\"", ClassName, "_Close", "\"", ";\n"));

        sb.Append("        ///<summary>" + "\n" + "        /// 通用回傳" + "\n" + "        ///<summary>\n");
        sb.Append(string.Concat("        public const string ", ClassName, "_Geneal_Result = ", "\"", ClassName, "_Geneal_Result", "\"", ";\n"));

        template = template.Replace("$ClassName", string.Concat(ClassName, "Events"));
        template = template.Replace("$MemberFields", sb.ToString());
        if (!System.IO.File.Exists("Assets/Src/MyUI"))
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI");

        }

        if (System.IO.File.Exists("Assets/Src/MyUI/Events/"))
        {
            using (var writer = new StreamWriter("Assets/Src/MyUI/Events/" + string.Concat(ClassName, "Events") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI/Events/");
            using (var writer = new StreamWriter("Assets/Src/MyUI/Events/" + string.Concat(ClassName, "Events") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成UIProxy
    /// </summary>
    void UIProxyMake()
    {
        Debug.LogError(string.Concat("UI", "Proxy") + " be Created");

        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/UIProxyTemplate.txt");
        string template = textAsset.ToString();

        template = template.Replace("$ClassName", string.Concat("UI", "Proxy"));
        if (!System.IO.File.Exists("Assets/Src/MyUI"))
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI");

        }

        if (System.IO.File.Exists("Assets/Src/MyUI/Model/"))
        {
            using (var writer = new StreamWriter("Assets/Src/MyUI/Model/" + string.Concat("UI", "Proxy") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI/Model/");
            using (var writer = new StreamWriter("Assets/Src/MyUI/Model/" + string.Concat("UI", "Proxy") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成BIDProxy
    /// </summary>
    void BIDProxyMake()
    {
        Debug.LogError(string.Concat("BID", "Proxy") + " be Created");
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/BIDProxyTemplate.txt");
        string template = textAsset.ToString();

        template = template.Replace("$ClassName", string.Concat("BID", "Proxy"));
        if (!System.IO.File.Exists("Assets/Src/MyUI"))
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI");

        }

        if (System.IO.File.Exists("Assets/Src/MyUI/Model/"))
        {
            using (var writer = new StreamWriter("Assets/Src/MyUI/Model/" + string.Concat("BID", "Proxy") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI/Model/");
            using (var writer = new StreamWriter("Assets/Src/MyUI/Model/" + string.Concat("BID", "Proxy") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成ScreenView
    /// </summary>
    void ScreenViewMake()
    {
        Debug.LogError("ScreenFactory" + " be Created");
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/ScreenViewTemplate.txt");
        string template = textAsset.ToString();

        if (System.IO.File.Exists("Assets/Src/"))
        {
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("ScreenFactory") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/");
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("ScreenFactory") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }


    /// <summary>
    /// 生成UIEvents
    /// </summary>
    void UIEventsMake()
    {
        Debug.LogError("UIEvents" + " be Created");
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/UIEventsTemplate.txt");
        string template = textAsset.ToString();

        if (!System.IO.File.Exists("Assets/Src/MyUI"))
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI");

        }

        if (System.IO.File.Exists("Assets/Src/MyUI/Events/"))
        {
            using (var writer = new StreamWriter("Assets/Src/MyUI/Events/" + string.Concat("UIEvents") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/MyUI/Events/");
            using (var writer = new StreamWriter("Assets/Src/MyUI/Events/" + string.Concat("UIEvents") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成Main
    /// </summary>
    void MainMake()
    {
        Debug.LogError("Main" + " be Created");
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AutoScripts/Templates/MainTemplate.txt");
        string template = textAsset.ToString();

        if (System.IO.File.Exists("Assets/Src/"))
        {
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("Main") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory("Assets/Src/");
            using (var writer = new StreamWriter("Assets/Src/" + string.Concat("Main") + ".cs"))
            {
                writer.Write(template);
                writer.Close();
            }
        }
        AssetDatabase.Refresh();
    }
}
