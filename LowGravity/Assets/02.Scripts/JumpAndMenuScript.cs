using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpAndMenuScript : MonoBehaviour
{
    public GameObject m_GameManager;
    GameManagerScript m_ManagerScript;

    public GameObject m_RetryBtn;
    public GameObject m_PauseBtn;
    public GameObject m_StartBtn;
    public GameObject m_RetryAfterFailBtn;
    public GameObject m_BGMBtn;
    public GameObject m_EffectSoundBtn;
    public GameObject m_JumpBtn;

    public GameObject m_LevelSelectPanel;
    public LevelSelectContent m_LevelSelectContent;
        
    // Use this for initialization
    void Start()
    {
        m_ManagerScript = m_GameManager.GetComponent<GameManagerScript>();
        if (m_ManagerScript.GetBGMBool())
        {
            m_BGMBtn.GetComponent<Image>().sprite = (Sprite)Resources.Load("Sprite/BGM_On", typeof(Sprite));
        }
        else
        {
            m_BGMBtn.GetComponent<Image>().sprite = (Sprite)Resources.Load("Sprite/BGM_Off", typeof(Sprite));
        }


        if (m_ManagerScript.GetEffectSoundBool())
        {
            m_EffectSoundBtn.GetComponent<Image>().sprite 
                = (Sprite)Resources.Load("Sprite/SE_On", typeof(Sprite));
        }
        else
        {
            m_EffectSoundBtn.GetComponent<Image>().sprite 
                = (Sprite)Resources.Load("Sprite/SE_Off", typeof(Sprite));
        }

        m_StartBtn.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Pause
    public void PauseAvailable()
    {
        m_PauseBtn.GetComponent<Button>().interactable = true;
    }

    public void PauseDisable()
    {
        m_PauseBtn.GetComponent<Button>().interactable = false;

    }

    public void PauseBtnPressed()
    {
        m_ManagerScript.Pause();
        m_JumpBtn.SetActive(false);
    }

    // Retry
    public void RetryAvailable()
    {
        m_RetryBtn.GetComponent<Button>().interactable = true;
    }

    public void RetryDisable()
    {
        m_RetryBtn.GetComponent<Button>().interactable = false;
    }

    public void RetryBtnPressed()
    {
        m_ManagerScript.RetryInMiddleOfPlaying();
    }

    public void RetryAfterFailBtnPressed()
    {
        m_ManagerScript.Retry();
    }

    public void StartBtnActive(bool active)
    {
        m_StartBtn.SetActive(active);
    }

    public void RetryAfterFailBtnActive(bool active)
    {
        m_RetryAfterFailBtn.SetActive(active);
    }

    public void JumpBtnActive(bool active)
    {
        m_JumpBtn.SetActive(active);
    }

    public void StartBtnPressed()
    {
        if (!m_ManagerScript.GetIsPlaying())
        {
            m_JumpBtn.SetActive(true);
        }
        else
        {
            m_JumpBtn.SetActive(true);
            m_ManagerScript.Resume();
        }

        m_StartBtn.SetActive(false);

    }

    public void BGMBtnPressed()
    {
        if(m_ManagerScript.GetBGMBool())
            m_ManagerScript.BGM_On_Off(false);
        else
            m_ManagerScript.BGM_On_Off(true);

        if (m_ManagerScript.GetBGMBool())
        {
            m_BGMBtn.GetComponent<Image>().sprite = (Sprite)Resources.Load("Sprite/BGM_On", typeof(Sprite));
        }
        else
        {
            m_BGMBtn.GetComponent<Image>().sprite = (Sprite)Resources.Load("Sprite/BGM_Off", typeof(Sprite));
        }

    }

    public void EffectSoundPressed()
    {
        if(m_ManagerScript.GetEffectSoundBool())
            m_ManagerScript.EffectSound_On_Off(false);
        else
            m_ManagerScript.EffectSound_On_Off(true);

        if (m_ManagerScript.GetEffectSoundBool())
        {
            m_EffectSoundBtn.GetComponent<Image>().sprite
                = (Sprite)Resources.Load("Sprite/SE_On", typeof(Sprite));
        }
        else
        {
            m_EffectSoundBtn.GetComponent<Image>().sprite
                = (Sprite)Resources.Load("Sprite/SE_Off", typeof(Sprite));
        }
    }

    public void NextBtnPressed()
    {
        m_ManagerScript.LoadNextLevel();
    }

    public void LevelSelected(Text text)
    {
        int level = int.Parse(text.text);
        m_ManagerScript.LevelSelect(level);

        HideLevelSelPanel();
    }

    public void HideLevelSelPanel()
    {
        m_LevelSelectPanel.GetComponent<Animator>().SetBool("Show", false);
        m_LevelSelectPanel.GetComponent<Animator>().SetBool("Hide", true);

        //RetryAvailable();
    }

    public void LevelMenuBtnPressed()
    {
        m_LevelSelectPanel.GetComponent<Animator>().SetBool("Show", true);
        m_LevelSelectPanel.GetComponent<Animator>().SetBool("Hide", false);

        m_LevelSelectContent.BtnSetToCurrentStage();
        //RetryDisable();
    }
}
