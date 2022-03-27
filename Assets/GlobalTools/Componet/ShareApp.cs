using UnityEngine;
using System.Collections;
using System.IO;
using System;

 
public class ShareApp : MonoBehaviour {
  
 string subject = "商品推薦唷";
 string body = "這件商品很適合您呢";
 string imagePath = Application.temporaryCachePath + "/uploadTemp.png";


 private static ShareApp mInstance;

 // 委託事件 - 成功 
 public delegate void DelegateCB();
 // 委託物件 - 成功
 public static DelegateCB onCompleteCB;

 public static ShareApp instance
 {
     get { return mInstance; }
 }
 void Awake()
 {
     mInstance = this;
 }
	
 void Start()
 {
     //ScreenShot();
     //shareText();
 }

 public void shareText(string path){
 //execute the below lines if being run on a Android device
 #if UNITY_ANDROID
  //Refernece of AndroidJavaClass class for intent
  AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
  //Refernece of AndroidJavaObject class for intent
  AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
  //call setAction method of the Intent object created
  intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
  //set the type of sharing that is happening

  //intentObject.Call<AndroidJavaObject>("setType", "text/plain");


  AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
  AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file:///" + path);
  intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

  //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("ACTION_SEND"), new String[] { });
  //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_EMAIL"), new String[] { });
  intentObject.Call<AndroidJavaObject>("setType", "image/*");

  //add data to be passed to the other activity i.e., the data to be sent
  //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
  //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);


  //get the current activity
  AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
  AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
  //start the activity by sending the intent data
  currentActivity.Call ("startActivity", intentObject);
 #endif
   
 }
 public void ScreenShot(System.Delegate cbFunc)
 {
     onCompleteCB = (DelegateCB)cbFunc;
     StartCoroutine(GetCapture());
 }

 IEnumerator GetCapture()
 {

     yield return new WaitForEndOfFrame();

     int width = Screen.width;
     int height = Screen.height;
     int x0 = width > 1440 ? (width-1440)/2 : 0;
     int x1 = width > 1440 ? 1440 + x0 : width;
     int y1 = Screen.height * 41 / 384;
     int y2 = Screen.height * 177 / 192;
     int capcuteWidth = width > 1440 ? 1440 : width;

     Texture2D tex = new Texture2D(capcuteWidth, Screen.height * 156 / 192, TextureFormat.ARGB32, true);
     //GameObject.Find("Anchor/Label").GetComponent<UILabel>().text = x0.ToString()+"/"+x1.ToString() + "/" + y1.ToString() + "/" + y2.ToString();

     //Texture2D tex = new Texture2D(1080, 1920, TextureFormat.RGB24, false);


     //Debug.Log("picX -> " + picX + " , picY - > " + picY + " , picWidth - > " + picWidth + " , picHeight ->" + picHeight);
     // 讀取螢幕像素，
     tex.ReadPixels(new Rect(x0, y1, x1, y2), 0, 0, true);
     //tex.ReadPixels(new Rect(0, 0, 1080, 1920 - 200), 0, 0, true);

     // 重新計算
     tex.Apply();

     byte[] imagebytes = tex.EncodeToPNG();//转化为png图

     tex.Compress(true);//对屏幕缓存进行压缩

     //		image.mainTexture = tex;//对屏幕缓存进行显示（缩略图）

     //"uploadTemp.png"
     //File.WriteAllBytes(Application.persistentDataPath + "/"+ DateTime.UtcNow.ToString("yyyyMMddmmss")+ ".png", imagebytes);//存储png图
     File.WriteAllBytes(Application.temporaryCachePath + "/" + "uploadTemp.png", imagebytes);//存储png图

     GlobalData.PNG = imagebytes;
     GlobalData.PATH = Application.temporaryCachePath + "/" + "uploadTemp.png";
    

     onCompleteCB.Invoke();

 }
}