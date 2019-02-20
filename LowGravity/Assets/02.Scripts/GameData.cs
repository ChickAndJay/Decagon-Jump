using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public int m_StageIdx;
    public int m_ClearedStageIdx;
    public bool m_BGM;
    public bool m_EffectSound;
    public bool m_ShowAds;

    
    public GameData(int stageIdx,int m_ClearedStageIdx,  bool bgm, bool effectsound, bool ShowAds)
    {
        this.m_StageIdx = stageIdx;
        this.m_ClearedStageIdx = m_ClearedStageIdx;
        this.m_BGM = bgm;
        this.m_EffectSound = effectsound;
        this.m_ShowAds = ShowAds;
    }
}
