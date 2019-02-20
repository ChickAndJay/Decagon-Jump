using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds.Api;

public class GameManagerScript : MonoBehaviour {
    
    bool m_ShowAds;

    static bool isAdsBannerSet = false;
    BannerView banner = null;
    UnityAdsHelper m_AdsHelper;
    int m_PlayNumbering = 0;

    public GameObject m_JumpController;
    //public GameObject m_StartController;
    //public GameObject m_FailController;

    public GameObject m_Player;
    public GameObject m_Camera;

    public GameObject m_MenuPanel;
    public GameObject m_CompletePanel;
    Animator m_CompleteAnimator;
    Animator m_MenuAnimator;
    public GameObject m_StageText;

    public LevelSelectContent m_LevelSelectContent;
    public Button m_NextBtn;

    bool m_isPlaying;
    bool m_isPause;

    int m_CurrentStageIdx;
    int m_ClearedStageIdx;
    int m_LastStageIdx;

    GameObject m_CurrentStage;
    GameObject m_NextStage;
    GameObject m_NextPos;

    Vector3 m_ResetPos;

    AudioSource m_AudioSource;

    bool m_EffectSoundOn;
    bool m_BGMOn;
    // Use this for initialization

    public int m_Stage_No;

    public GameObject m_ExplodeParticle;

    public GameObject m_NoAdsCanvas;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0; // vsync 사용안함 
        Application.targetFrameRate = 60; // 30 프레임 고정. 

        m_LastStageIdx = 50;

        m_ClearedStageIdx = 0;
        m_CurrentStageIdx = m_Stage_No;
        m_BGMOn = true;
        m_EffectSoundOn = true;

        m_ShowAds = true;

        LoadFile();

        //GetComponent<AudioSource>().mute = m_BGMOn;
        //m_BGMOn = m_BGMOn;
               
        string stagePath = "Stages/Stage_";
        string tempPath = stagePath + m_CurrentStageIdx;

        m_CurrentStage = GameObject.Instantiate((GameObject)Resources.Load(tempPath, typeof(GameObject)));
        
        m_CurrentStage.GetComponent<Transform>().position = new Vector3(0, 0, 0);
        m_ResetPos = m_CurrentStage.GetComponent<Transform>().position + new Vector3(0,3.5f,0);
        m_NoAdsCanvas.SetActive(m_ShowAds);
    }

    void Start () {
        m_JumpController.SetActive(true);

        m_JumpController.GetComponent<JumpAndMenuScript>().RetryAvailable();
        m_JumpController.GetComponent<JumpAndMenuScript>().PauseAvailable();
        m_JumpController.GetComponent<JumpAndMenuScript>().StartBtnActive(false);
        m_JumpController.GetComponent<JumpAndMenuScript>().RetryAfterFailBtnActive(false);
        
        m_MenuAnimator = m_MenuPanel.GetComponent<Animator>();
        m_MenuAnimator.SetBool("Hide", true);
        m_MenuAnimator.SetBool("Show", false);

        m_CompleteAnimator = m_CompletePanel.GetComponent<Animator>();

        m_isPlaying = true;
        m_isPause = false;

        m_AudioSource = GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Chase WAITING LOOP");

        m_AudioSource.clip = clip;
        m_AudioSource.loop = true;
        m_AudioSource.Play();

        BGM_On_Off(m_BGMOn);
        //EffectSound_On_Off(m_EffectSoundOn);
        m_PlayNumbering++;

        if (!isAdsBannerSet && m_ShowAds)
            RequestBanner();

       m_AdsHelper = GetComponent<UnityAdsHelper>();
    }

    // Update is called once per frame
    void Update () {

    }

    public void LevelSelect(int Level)
    {
        Time.timeScale = 1f;

        m_CompleteAnimator.SetBool("Show", false);
        m_CompleteAnimator.SetBool("Hide", true);

        m_MenuAnimator.SetBool("Hide", true);
        m_MenuAnimator.SetBool("Show", false);

        m_JumpController.GetComponent<JumpAndMenuScript>().RetryAvailable();
        m_JumpController.GetComponent<JumpAndMenuScript>().PauseAvailable();
        m_JumpController.GetComponent<JumpAndMenuScript>().StartBtnActive(false);
        m_JumpController.GetComponent<JumpAndMenuScript>().RetryAfterFailBtnActive(false);
        m_JumpController.GetComponent<JumpAndMenuScript>().JumpBtnActive(true);

        m_Player.GetComponent<PlayerScript>().ResetPlayer();

        m_Player.GetComponent<PlayerScript>().GoalMade(false);
        m_Camera.GetComponent<CameraMovement>().m_Following = true;

        m_isPlaying = true;
        m_isPause = false;

        Destroy(m_CurrentStage);
        m_CurrentStageIdx = Level;

        string stagePath = "Stages/Stage_";
        string tempPath = stagePath + (m_CurrentStageIdx);

        m_CurrentStage = GameObject.Instantiate((GameObject)Resources.Load(tempPath, typeof(GameObject)),
            new Vector3(0, 0, 0),
            new Quaternion(0, 0, 0, 0));


        m_PlayNumbering++;
        if(m_PlayNumbering > 5 && m_ClearedStageIdx > 8)
        {
            if (m_ShowAds)
                m_AdsHelper.ShowRewardedAd();
        }

        SaveFile();
    }

    public void FinishCurrentStage()
    {
        m_Camera.GetComponent<CameraMovement>().m_Following = false;
        m_Player.GetComponent<PlayerScript>().GoalMade(true);

        m_isPlaying = false;
        m_isPause = true;

        m_JumpController.GetComponent<JumpAndMenuScript>().RetryDisable();
        m_JumpController.GetComponent<JumpAndMenuScript>().PauseDisable();

        m_MenuAnimator.SetBool("Hide", false);
        m_MenuAnimator.SetBool("Show", true);

        m_CompleteAnimator.SetBool("Show", true);
        m_CompleteAnimator.SetBool("Hide", false);

        if (m_CurrentStageIdx == m_LastStageIdx)
        {
            m_NextBtn.interactable = false;
        }
        else
            m_NextBtn.interactable = true;

        if (m_ClearedStageIdx < m_CurrentStageIdx)
        {
            m_ClearedStageIdx = m_CurrentStageIdx;
            m_LevelSelectContent.ContentUpdate(m_ClearedStageIdx);
        }
        
        m_PlayNumbering--;

        SaveFile();

        if (m_ShowAds && m_ClearedStageIdx > 8)
            m_AdsHelper.ShowRewardedAd();
    }

    public void LoadNextLevel()
    {
        m_CompleteAnimator.SetBool("Show", false);
        m_CompleteAnimator.SetBool("Hide", true);

        m_MenuAnimator.SetBool("Hide", true);
        m_MenuAnimator.SetBool("Show", false);

        m_JumpController.GetComponent<JumpAndMenuScript>().RetryAvailable();
        m_JumpController.GetComponent<JumpAndMenuScript>().PauseAvailable();
        m_JumpController.GetComponent<JumpAndMenuScript>().StartBtnActive(false);
        m_JumpController.GetComponent<JumpAndMenuScript>().RetryAfterFailBtnActive(false);

        m_Player.GetComponent<PlayerScript>().ResetPlayer();

        m_Player.GetComponent<PlayerScript>().GoalMade(false);
        m_Camera.GetComponent<CameraMovement>().m_Following = true;

        m_isPlaying = true;
        m_isPause = false;

        Destroy(m_CurrentStage);
        m_CurrentStageIdx++;
        m_PlayNumbering++;

        string stagePath = "Stages/Stage_";
        string tempPath = stagePath + (m_CurrentStageIdx);

        m_CurrentStage = GameObject.Instantiate(
            (GameObject)Resources.Load(tempPath, typeof(GameObject)),
            new Vector3(0, 0, 0),
            new Quaternion(0, 0, 0, 0));

        SaveFile();

    }

    public void Retry()
    {
        Time.timeScale = 1f;

        m_Player.GetComponent<PlayerScript>().GoalMade(false);
        m_Camera.GetComponent<CameraMovement>().m_Following = true;

        m_isPlaying = true;
        m_isPause = false;

        m_Player.GetComponent<PlayerScript>().ResetPlayer();
        m_CurrentStage.GetComponent<StageScript>().Retry();

        m_JumpController.GetComponent<JumpAndMenuScript>().RetryAvailable();
        m_JumpController.GetComponent<JumpAndMenuScript>().PauseAvailable();
        m_JumpController.GetComponent<JumpAndMenuScript>().StartBtnActive(false);
        m_JumpController.GetComponent<JumpAndMenuScript>().RetryAfterFailBtnActive(false);
        m_JumpController.GetComponent<JumpAndMenuScript>().HideLevelSelPanel();
        m_JumpController.GetComponent<JumpAndMenuScript>().JumpBtnActive(true);

        m_MenuAnimator.SetBool("Hide", true);
        m_MenuAnimator.SetBool("Show", false);

        m_CompleteAnimator.SetBool("Show", false);
        m_CompleteAnimator.SetBool("Hide", true);

        m_PlayNumbering++;

        if (m_PlayNumbering > 5)
        {
            m_PlayNumbering = 0;
            if(m_ShowAds && m_ClearedStageIdx > 8)
                m_AdsHelper.ShowRewardedAd();
        }
    }

    public void RetryInMiddleOfPlaying()
    {
        GameObject instance = GameObject.Instantiate(m_ExplodeParticle, transform.position, transform.rotation);
        instance.GetComponent<Transform>().position = m_Player.GetComponent<Transform>().position;
        instance.GetComponent<ParticleSystem>().Play();
        m_Player.GetComponent<PlayerScript>().PlayExplosionSound();
        m_Camera.GetComponent<CameraMovement>().ShakeCamera();

        Retry();
    }

    public void Fail()
    {
        m_isPlaying = false;
        m_Camera.GetComponent<CameraMovement>().ShakeCamera();

        m_JumpController.GetComponent<JumpAndMenuScript>().RetryAfterFailBtnActive(true);

        m_JumpController.GetComponent<JumpAndMenuScript>().RetryDisable();
        m_JumpController.GetComponent<JumpAndMenuScript>().PauseDisable();

        m_MenuAnimator.SetBool("Hide", false);
        m_MenuAnimator.SetBool("Show", true);

    }

    public void Pause()
    {
        m_MenuAnimator.SetBool("Hide", false);
        m_MenuAnimator.SetBool("Show", true);

        m_JumpController.GetComponent<JumpAndMenuScript>().StartBtnActive(true);

        m_isPause = true;

        if (m_isPlaying)
            Time.timeScale = 0f;
    }

    public void Resume()
    {
        m_MenuAnimator.SetBool("Hide", true);
        m_MenuAnimator.SetBool("Show", false);

        m_isPause = false;
        Time.timeScale = 1f;

    }

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";

        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);


        GameData data = new GameData(m_CurrentStageIdx, m_ClearedStageIdx, m_BGMOn, m_EffectSoundOn, m_ShowAds);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            //m_ShowAds = m_NoAdsCanvas.GetComponent<Purchaser>().DidBuyNoAds();
            //Continue_Btn.enabled = false;
            m_ShowAds = true;
            m_CurrentStageIdx = 1;
            m_ClearedStageIdx = 0;
            m_BGMOn = true;
            m_EffectSoundOn = true;

            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        m_CurrentStageIdx = data.m_StageIdx;
        m_ClearedStageIdx = data.m_ClearedStageIdx;
        m_BGMOn = data.m_BGM;
        m_EffectSoundOn = data.m_EffectSound;
        m_ShowAds = data.m_ShowAds;

    }
    public bool GetIsPlaying()
    {
        return m_isPlaying;
    }

    public bool GetIsPause()
    {
        return m_isPause;
    }

    public void SetStagePrefab(GameObject stage)
    {
        m_CurrentStage = stage;
    }

    public void ShowStageText()
    {

        m_StageText.GetComponent<Text>().enabled = true;
        m_StageText.GetComponent<Text>().text = "STAGE - " + m_CurrentStageIdx;
        m_StageText.GetComponent<Animator>().SetBool("ShowText", true);
    }

    public void DisableStageText()
    {
        m_StageText.GetComponent<Text>().enabled = false;
    }

    public GameObject GetCurrentStageObj()
    {
        return m_CurrentStage;
    }

    public Vector3 GetResetPos()
    {
        return m_ResetPos;
    }

    public int GetCurrentStageIdx()
    {
        return m_CurrentStageIdx;
    }

    public int GetLastStageNo()
    {
        return m_LastStageIdx;
    }

    public int GetClearedStageIdx()
    {
        return m_ClearedStageIdx;
    }

   

    private void OnApplicationQuit()
    {
        SaveFile();
    }

    public bool GetBGMBool()
    {
        return m_BGMOn;
    }

    public bool GetEffectSoundBool()
    {
        return m_EffectSoundOn;
    }

    public void BGM_On_Off(bool on)
    {
        if (on)
        {
            GetComponent<AudioSource>().mute = false;
            m_BGMOn = on;
        }else
        {
            GetComponent<AudioSource>().mute = true;
            m_BGMOn = on;
        }
    }

    public void EffectSound_On_Off(bool on)
    {
        if (on)
        {
            m_Player.GetComponent<PlayerScript>().SoundOnOff(on);
            m_EffectSoundOn = on;
        }
        else
        {
            m_Player.GetComponent<PlayerScript>().SoundOnOff(on);
            m_EffectSoundOn = on;
        }
    }


    private void RequestBanner()

    {

#if UNITY_ANDROID

        string AdUnitID = "ca-app-pub-2970708053906682/7147937170";

#else

        string AdUnitID = "unDefind";

#endif

        banner = new BannerView(AdUnitID, AdSize.Banner, AdPosition.Bottom);



        AdRequest request = new AdRequest.Builder().Build();
        //AdRequest request =
            //new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("C251A04598CF28276A850C491D2B598B").Build();

        banner.LoadAd(request);
        banner.Show();
        
        isAdsBannerSet = true;

    }

    public void AdsHide()
    {
        if(banner != null)
            banner.Hide();

        m_ShowAds = false;

        m_NoAdsCanvas.SetActive(false);

        SaveFile();
    }

    
}
