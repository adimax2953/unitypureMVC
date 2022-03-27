using UnityEngine;
using System.Collections;
//using Vuforia;
public class CameraManager : MonoBehaviour {

    [Tooltip("這裡把UIcamera拖進來 ，拍照時要隱藏")]
    public GameObject UICamera;

    private static CameraManager mInstance;

    public static CameraManager Instance
    {
        get { return mInstance; }
    }





    // Use this for initialization
    void Start()
    {
        mInstance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// 使用的鏡頭
    /// </summary>
    /// <param name="Switch">true用前鏡頭</param>
    public void ChangeCamera(bool Switch = false)
    {
       /* CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        if (Switch == true)
        {
            CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_FRONT);
        }
        else
        {
            CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_BACK);
        }
        CameraDevice.Instance.Start();*/
    }
    /// <summary>
    /// 分享至其他APP
    /// </summary>
    public void Share()
    {
        Debug.LogError(Application.temporaryCachePath);
        ShareApp.instance.shareText(string.Concat(Application.temporaryCachePath, "/uploadTemp.png"));
    }




    /// <summary>
    /// 拍照
    /// </summary>
    public void PhotoGraph(ShareApp.DelegateCB funtion)
    {
        if (UICamera != null)
        {
            UICamera.SetActive(false);
        }
        ShareApp.instance.ScreenShot(new ShareApp.DelegateCB(funtion));        
    }

    


    /// <summary>
    /// 拍照的回傳
    /// </summary>
    public virtual void PhotoGraphCallBack()
    {
        if (UICamera != null)
        {
            UICamera.SetActive(true);
        }
        
        Debug.LogError("拍照");
    }
}
