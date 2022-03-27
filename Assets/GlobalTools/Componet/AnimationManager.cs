using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour {

    private static AnimationManager mInstance;

    public static AnimationManager Instance
    {
        get { return mInstance; }
    }

    [Tooltip("填入動畫名稱")]
    public List<string> AnimationName;
    [Tooltip("拉入可撥放動畫的物件")]
    public List<Animation> AnimationTarget;


    // Use this for initialization
    void Start () {
        mInstance = this;
    }
    /// <summary>
    /// 重置檔案表
    /// </summary>
    public void Reset()
    {
        AnimationName = null;
        AnimationTarget = null;
    }

    // Update is called once per frame
    void Update () {
	
	}

    /// <summary>
    /// 撥放動畫
    /// </summary>
    /// <param name="AnimationTargetIndex">動畫物件 EX小錫兵之類的</param>
    /// <param name="AnimationNameIndex">要撥放的動畫名稱</param>
    /// <param name="fadeLength">漸變係數</param>
    public void AnimationPlay(int AnimationTargetIndex =999, int AnimationNameIndex =999, float fadeLength = 0.2f)
    {

        if (AnimationTargetIndex == 999 || AnimationTargetIndex > AnimationTarget.Count)
        {
            Debug.LogError("AnimationTargetIndex大於現有內容數");

            return;
        }
        if (AnimationNameIndex == 999 || AnimationNameIndex > AnimationName.Count)
        {
            Debug.LogError("AnimationNameIndex大於現有內容數");

            return;
        }
        AnimationTarget[AnimationTargetIndex].CrossFade(AnimationName[AnimationNameIndex], fadeLength);

    }

}
