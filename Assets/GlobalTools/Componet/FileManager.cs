using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public class FileManager : MonoBehaviour {


    // 顯示副檔名類別
    private static string _FILE_EXTENSION_PNG = "*.png";
    private static string _FILE_EXTENSION_JPG = "*.jpg";
    // 委託事件 - 成功 
    public delegate void DeletDelegateCB();
    // 委託物件 - 成功
    public static DeletDelegateCB onDeletCompleteCB;

    private static FileManager mInstance;

    public static FileManager Instance
    {
        get { return mInstance; }
    }


    // Use this for initialization
    void Start () {
        mInstance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 讀取目錄下所有檔案
    public static  ArrayList getFiles(string path)
    {
        ArrayList files = new ArrayList();

        if (Directory.Exists(path))
        {
            files.AddRange(Directory.GetFiles(path, _FILE_EXTENSION_PNG));
            files.AddRange(Directory.GetFiles(path, _FILE_EXTENSION_JPG));
        }
        
        return files;
    }
    public static void saveFile()
    {
       string filePath = Application.persistentDataPath + "/" + DateTime.Now.ToString("yyyyMMddmmss") + ".png";
       FileInfo file = new FileInfo(GlobalData.PATH);
       file.CopyTo(filePath,true);

       GlobalData.fileList.Add((filePath));

    }
    public static void saveFileByTexture()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/DCIM"))
        {
            string filePath = Application.persistentDataPath + "/DCIM/" + DateTime.Now.ToString("yyyyMMddmmss") + ".png";
            File.WriteAllBytes(filePath, GlobalData.PNG);//存储png图
            GlobalData.fileList.Add((filePath));
        }
        else
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/DCIM");
            string filePath = Application.persistentDataPath + "/DCIM/" + DateTime.Now.ToString("yyyyMMddmmss") + ".png";
            File.WriteAllBytes(filePath, GlobalData.PNG);//存储png图
            if (GlobalData.fileList == null)
            {
                paseFilePath();
            }
            GlobalData.fileList.Add((filePath));
        }
    }
    private static void paseFilePath()
    {
        List<string> _pathList = new List<string>();

        string path = string.Concat(Application.persistentDataPath + "/DCIM/");
        ArrayList filePath = FileManager.getFiles(path);

        if (filePath.Count > 0)
        {
            foreach (var item in filePath)
            {
                string Path = item.ToString();
                if (Application.platform == RuntimePlatform.Android)
                {
                    _pathList.Add(string.Concat(path, Path.Substring(Path.LastIndexOf("/") + 1)));

                }
                else
                {
                    _pathList.Add(string.Concat(Path.Substring(Path.LastIndexOf("\\") + 1)));
                }
            }
        }
        GlobalData.fileList = _pathList;
    }
    public static void deleteFile(System.Delegate cbFunc)
    {
        onDeletCompleteCB = (DeletDelegateCB)cbFunc;

        if (System.IO.File.Exists(GlobalData.PATH))
        {
            System.IO.File.Delete(GlobalData.PATH);
            onDeletCompleteCB.Invoke();
        }
    }

    public static string changePathSlash(string path)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return path.Replace("/", "/");
        }
        else
        {
            return path.Replace("/", "\\");
        }
    }
}
