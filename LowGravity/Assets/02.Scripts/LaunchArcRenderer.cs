using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArcRenderer : MonoBehaviour {
    LineRenderer m_LineRenderer;

    public float m_velocity;
    public float m_angle;
    public int m_resolution;

    float g; // force of gravity on the y axis
    float m_radianAngle;

    bool m_renderLine;
    float m_CharacterBoostPower;
    float m_CharacterGage;

    GameObject trajectoryDots;
    public GameObject[] dots;                   //The array of points that make up the trajectory
    public float initialDotSize;                //The intial size of the trajectoryDots gameobject
    public Sprite dotSprite;                    //All of the dots will become the sprite assigned to this if this has a sprite assigned to it and changeSpriteAfterStart is true
    public int numberOfDots;					//The number of points representing the trajectory

    bool changeSpriteAfterStart = true;
    private void Awake()
    {
        m_renderLine = false;
        //m_LineRenderer = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
    }

    private void OnValidate()
    {
    }

    // Use this for initialization
    void Start () {
        //RenderArc();

        trajectoryDots = GameObject.Find("Trajectory Dots");        //TRAJECTORY DOTS MUST HAVE THE SAME NAME IN HIERARCHY AS IT DOES HERE
        trajectoryDots.transform.localScale = new Vector3(initialDotSize, initialDotSize, trajectoryDots.transform.localScale.z); //Initial size of trajectoryDots is applied

        for (int k = 0; k < 40; k++)
        {
            dots[k] = GameObject.Find("Dot (" + k + ")");           //All points are applied to the corresponding position in the dots array
            if (dotSprite != null)
            {                               //If a sprite is applied to dotSprite
                dots[k].GetComponent<SpriteRenderer>().sprite = dotSprite;  //All points will have that sprite applied
            }
        }

        for (int k = numberOfDots; k < 40; k++)
        {                   //If the number of points being used is less than 40, the maximum...
            GameObject.Find("Dot (" + k + ")").SetActive(false);    //They will be hidden
        }
        trajectoryDots.SetActive(false);                           //Trajectory initialization complete, the trajectory is hidden




    }

    // populating the LineRender with the appropriate setting
    void RenderArc()
    {

        m_velocity = Mathf.Sqrt(Mathf.Pow(m_CharacterBoostPower,2) + 
            Mathf.Pow(m_CharacterBoostPower * m_CharacterGage,2)) ;
        m_angle = Mathf.Asin(m_CharacterBoostPower / m_velocity);

        Vector3[] dots_Pos = CalculateArcArray();
        for(int i=0; i<m_resolution; i++)
        {
            dots[i].transform.position = dots_Pos[i];
        }
        
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[m_resolution + 1];

        m_radianAngle = m_angle;
        float maxDistance = (m_velocity * m_velocity * Mathf.Sin(2 * m_radianAngle)) / g;

        for(int i= 0; i<= m_resolution; i++)
        {
            float t = (float)i*1.15f / (float)m_resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
            arcArray[i] += new Vector3(0, transform.position.y, transform.position.z);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(m_radianAngle) - ((g * x * x) / (2 * m_velocity * m_velocity * Mathf.Cos(m_radianAngle) * Mathf.Cos(m_radianAngle)));
        return new Vector3(0,y ,x);
    }

    public void SetVelocity(float velocity)
    {
        m_velocity = velocity;
    }

    public void SetRenderLine(bool renderLine)
    {        
        m_renderLine = renderLine;
        //SetVertexZero();
    }

    public void SetCharacterGage(float gage)
    {
        m_CharacterGage = gage;
    }

    public void SetCharacterBoostPower(float boostPower)
    {
        m_CharacterBoostPower = boostPower;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_renderLine)
        {
            trajectoryDots.SetActive(true);
            RenderArc();
        }
        else
        {
            trajectoryDots.SetActive(false);

            SetVertexZero();
        }

        if (changeSpriteAfterStart == true)
        {                                   //If you've allowed the sprite to be continiously changed...
            for (int k = 0; k < numberOfDots; k++)
            {
                if (dotSprite != null)
                {                                       //If a sprite is applied to dotSprite
                    dots[k].GetComponent<SpriteRenderer>().sprite = dotSprite;//Change all points' sprite to the dotSprite sprite
                }
            }
        }
    }
    public void SetVertexZero()
    {
        //m_LineRenderer.SetVertexCount(0);
    }
}
