using UnityEngine;
using System.Collections;
using System;

public class AudiosManager : MonoBehaviour {



    private static AudiosManager mInstance;

    public static AudiosManager Instance
    {
        get { return mInstance; }
    }
    [Tooltip("拉入按鈕音效，需要幾筆自己新增吧")]
    public AudioClip[] BtnAudio;

    [Tooltip("拉入特效音效，需要幾筆自己新增吧")]
    public AudioClip[] EffectAudio;

    [Tooltip("拉入背景音樂音效，需要幾筆自己新增吧")]
    public AudioClip[] BackGroundAudio;

    private AudioSource _EffectAudioSource;
    private AudioSource _BackGroundAudioSource;
    private AudioSource _BtnAudioSource;




    // Use this for initialization
    void Start () {
        mInstance = this;
        _EffectAudioSource = transform.Find("EffectAudio").GetComponent<AudioSource>();
        _BtnAudioSource = transform.Find("BtnAudio").GetComponent<AudioSource>();
        _BackGroundAudioSource = transform.Find("BackGroundAudio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
	
	}
    /// <summary>
    /// 按鈕音效播放
    /// </summary>
    /// <param name="Index">填入按鈕音效的筆數</param>
    public void BtnAudioPlay(int Index = 999)
    {
        if (BtnAudio == null || BtnAudio.Length == 0)
        {
            Debug.LogError("BtnAudio陣列裡面是空的或者沒有半筆資料");
            return;
        }
        if (Index != 999 && BtnAudio.Length >= Index)
        {
            _BtnAudioSource.clip = BtnAudio[Index];
            _BtnAudioSource.Play();
        }
        else
        {
            Debug.LogError("Index大於現有內容數");
        };
    }
    /// <summary>
    /// 特效音效播放
    /// </summary>
    /// <param name="Index">填入特效音效</param>
    public void BtnEffectAudioPlay(int Index)
    {
        if (EffectAudio == null || EffectAudio.Length == 0)
        {
            Debug.LogError("EffectAudio陣列裡面是空的或者沒有半筆資料");
            return;
        }
        if (Index != 999 && EffectAudio.Length >= Index)
        {
            _EffectAudioSource.clip = EffectAudio[Index];
            _EffectAudioSource.Play();
        }
        else
        {
            Debug.LogError("Index大於現有內容數");
        };
    }
    /// <summary>
    /// 背景音樂撥放
    /// </summary>
    /// <param name="Index"></param>
    public void BackGroundAudioPlay(int Index)
    {
        if (BackGroundAudio == null || BackGroundAudio.Length == 0)
        {
            Debug.LogError("BackGroundAudio陣列裡面是空的或者沒有半筆資料");
            return;
        }
        if (Index != 999 && EffectAudio.Length >= Index)
        {
            _BackGroundAudioSource.clip = BackGroundAudio[Index];
            
            _BackGroundAudioSource.Play();
        }
        else
        {
            Debug.LogError("Index大於現有內容數");
        };
    }


    /// <summary>
    /// 取得一般的音樂撥放器 按鈕使用的
    /// </summary>
    public AudioSource GetBtnAudioPlayer
    {
        get { return _BtnAudioSource; }
    }
    /// <summary>
    /// 取得一般的音樂撥放器 特效使用的
    /// </summary>
    public AudioSource GetEffectAudioPlayer
    {
        get { return _EffectAudioSource; }
    }
    /// <summary>
    /// 取得背景音樂撥放器 背景音樂使用
    /// </summary>
    public AudioSource GetBackGroundAudioPlayer
    {
        get { return _BackGroundAudioSource; }
    }
}
