using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour {
    public int m_StageNo;
    public GameObject m_GoalLight;
    public GameObject m_NextPos;

    public GameObject m_GameManager;
    
    bool m_IsCurrentStage;

    MeshRenderer[] m_MeshRenderers;

    // Obstacles
    GameObject m_Ground_1_Prefab;




	// Use this for initialization
	void Start () {
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager");

        string stagePath = "Obstacles/";
        string tempPath = stagePath + "Ground_1";


        //m_Ground_1_Prefab = (GameObject)Resources.Load(tempPath, typeof(GameObject));

        //m_GameManager = GameObject.FindGameObjectWithTag("GameManager");

        //Transform[] transforms = GetComponentsInChildren<Transform>();
        //foreach(Transform tr in transforms)
        //{
        //    if (tr.CompareTag("Ground_1_Pos"))
        //    {
        //        m_Ground_1_Pos.Add(tr);
        //        GameObject obj = GameObject.Instantiate(m_Ground_1_Prefab, tr.position, tr.rotation);
        //        obj.transform.SetParent(gameObject.transform);
        //        m_Obstacles.Add(obj);
        //    }
        //}


        // 현재 스테이지가 아닐경우 렌더하지 않기

        //if(m_StageNo == m_GameManager.GetComponent<GameManagerScript>().GetCurrentStageIdx())
        //{            

        //}
        //else
        //{
        //    m_BigDoor.SetActive(true);
        //    StartCoroutine(UpBigDoor(1.0f, m_BigDoor.GetComponent<Transform>()));

        //    m_MeshRenderers = GetComponentsInChildren<MeshRenderer>();

        //    foreach(MeshRenderer mr in m_MeshRenderers)
        //    {
        //        if (mr.CompareTag("SwitchRender"))
        //            mr.enabled = false;
        //    }
        //}
        //m_GameManager.GetComponent<GameManagerScript>().SetStagePrefab(this.gameObject);

        // 현재 스테이지가 아닐경우 바다속에 처넣기

    }


   
    public void SetIsCurrentStage(bool current)
    {
        m_IsCurrentStage = current;
    }
    

    //public void TurnLightGreen()
    //{
    //    Color green = new Color(0, 1, 0.07450981f);
    //    Color green_Emission = new Color(0.2039216f, 1, 0.1921569f);
    //    m_GoalLight.GetComponent<Renderer>().material.SetColor("_Color", green);
    //    m_GoalLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", green_Emission);

    //}

    //public void TurnLightRed()
    //{
    //    Color red = new Color(1,0,0, 1);
    //    Color red_Emission = new Color(1, 0.1843137f, 0.1843137f);

    //    m_GoalLight.GetComponent<Renderer>().material.SetColor("_Color", red);
    //    m_GoalLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", red_Emission);

    //}

    //public void SetGoalLight(GameObject Light)
    //{
    //    m_GoalLight = Light;
    //}


    public void ReachPlayerGoal()
    {
        m_GameManager.GetComponent<GameManagerScript>().FinishCurrentStage();
    }

    //public void DownBigDoor()
    //{
    //    StartCoroutine(DownBigDoor(1.0f, m_BigDoor.GetComponent<Transform>()));

    //}

    public GameObject GetNextPos()
    {
        return m_NextPos;
    }

    //public void RenderObstacles()
    //{
    //    foreach (MeshRenderer mr in m_MeshRenderers)
    //    {
    //        if (mr.CompareTag("SwitchRender"))
    //            mr.enabled = true;
    //    }
    //}

    public void Retry()
    {
        //ResetPoop();
        //ResetObstacles();
    }
}
