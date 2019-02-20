using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectContent : MonoBehaviour {
    public GameObject m_GameManager;
    GameManagerScript m_GameManagerScript;

    [Range(0, 500)]
    public int m_panOffset;
    [Range(0f, 100f)]
    public float m_SnapSpeed;
    [Header("Other Objects")]
    public GameObject m_PanPrefab;
    private GameObject[] m_InstPans;
    private Vector2[] m_PansPos;

    int m_Cleared_Stage_Idx;
    int m_MaxStageNo;

    private RectTransform m_ContentRect;
    Vector2 m_ContentVector;

    public int m_selectPanID;

    public bool m_isScrolling;
	// Use this for initialization
	void Start () {
        m_GameManagerScript = m_GameManager.GetComponent<GameManagerScript>();
        m_MaxStageNo = m_GameManagerScript.GetLastStageNo();
        m_Cleared_Stage_Idx = m_GameManagerScript.GetClearedStageIdx() + 1;

        m_ContentRect = GetComponent<RectTransform>();

        m_InstPans = new GameObject[m_MaxStageNo];
        m_PansPos = new Vector2[m_MaxStageNo];


        for(int i=0; i<m_Cleared_Stage_Idx; i++)
        {
            m_InstPans[i] = Instantiate(m_PanPrefab, transform, false);
            m_InstPans[i].GetComponentInChildren<Text>().text = "" + (i + 1);
            if (i == 0) continue;
            m_InstPans[i].transform.localPosition =
                new Vector2(m_InstPans[i - 1].transform.localPosition.x +
                m_PanPrefab.GetComponent<RectTransform>().sizeDelta.x + m_panOffset,
                m_InstPans[i].transform.localPosition.y);
            m_PansPos[i] = -m_InstPans[i].transform.localPosition;
        }
        //m_RectTransform.
	}
	
	// Update is called once per frame
	void Update () {
        float nearestPos = float.MaxValue;

        for (int i = 0; i < m_Cleared_Stage_Idx; i++)
        {
            float distance = Mathf.Abs(m_ContentRect.anchoredPosition.x - m_PansPos[i].x);

            if (distance < nearestPos)
            {
                nearestPos = distance;
                m_selectPanID = i;
            }
        }
        if (m_isScrolling) return;

        m_ContentVector.x = Mathf.SmoothStep(m_ContentRect.anchoredPosition.x,
                m_PansPos[m_selectPanID].x, m_SnapSpeed * Time.unscaledDeltaTime);
        m_ContentRect.anchoredPosition = m_ContentVector;
    }

    

    private void FixedUpdate()
    {
        

    }

    public void Scrolling(bool scroll)
    {
        m_isScrolling = scroll;
    }
    public void ContentUpdate(int clearedStageIdx)
    {
        if (clearedStageIdx != m_MaxStageNo)
        {
            m_Cleared_Stage_Idx++;

            m_InstPans[clearedStageIdx] = Instantiate(m_PanPrefab, transform, false);
            m_InstPans[clearedStageIdx].GetComponentInChildren<Text>().text = "" + (clearedStageIdx + 1);

            m_InstPans[clearedStageIdx].transform.localPosition =
                new Vector2(m_InstPans[clearedStageIdx - 1].transform.localPosition.x +
                m_PanPrefab.GetComponent<RectTransform>().sizeDelta.x + m_panOffset,
                m_InstPans[clearedStageIdx].transform.localPosition.y);
            m_PansPos[clearedStageIdx] = -m_InstPans[clearedStageIdx].transform.localPosition;
        }
    }

    public void BtnSetToCurrentStage()
    {
        int currentIdx = m_GameManagerScript.GetCurrentStageIdx()-1;
        m_ContentRect.anchoredPosition = 
            new Vector2(m_PansPos[currentIdx].x,
            m_ContentRect.anchoredPosition.y);

        m_selectPanID = currentIdx;
    }
}
