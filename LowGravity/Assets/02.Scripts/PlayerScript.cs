using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    public GameObject m_GameManager;
    
    Transform m_Player_Tr;
    Rigidbody m_Player_Rb;

    // Jump Power details
    float m_Boost_Power_Max = 15f;
    float m_Boot_Limit = 1.8f;

    bool m_Start;
    bool m_GoalMade;
    bool m_Grounded;
    float m_RayLong = 1f;
    
    // Check Details for Jump
    float m_Gage;
    bool m_JumpReady;
    bool m_Gage_Up;
    bool m_Jump;

    // Sounds
    public AudioSource m_BeepAudioSource;
    public AudioSource m_ExplodeAudioSource;
    AudioClip m_ExplosionSound;
    AudioClip m_BeepSound;
    AudioClip m_SuccesSound;

    // Particle
    public GameObject m_ExplodeParticle;    

    // Render dots where Player will land
    LaunchArcRenderer m_LaunchArcRenderer;

    // Changes color to show that Player is ready
    // to Jump
    public Material m_Body_LineMat;
    Color m_Original_Color;
    Color m_JumpColor;
    
    // when fails, Player's body parts disappear
    List<Transform> m_BodyParts = new List<Transform>();


    // Beep Triggers collector
    BoxCollider[] m_BeepColliderCollection = new BoxCollider[10];

    // Use this for initialization
    void Start () {
        m_Player_Tr = GetComponent<Transform>();
        m_Player_Rb = GetComponent<Rigidbody>();

        m_Start = true;
        m_Grounded = false;
        m_Gage = 0f;        
        m_JumpReady = false;
        m_Jump = false;

        m_ExplosionSound = Resources.Load<AudioClip>("Sounds/EXPLOSION_Distorted_04_Medium_stereo");
        m_BeepSound = Resources.Load<AudioClip>("Sounds/8BIT_RETRO_Hit_Bump_Distorted_Thud_mono");
        m_SuccesSound = Resources.Load<AudioClip>("Sounds/VOICE_RADIO_MALE_Success_1_mono");

        m_BeepAudioSource.clip = m_BeepSound;
        m_ExplodeAudioSource.clip = m_ExplosionSound;
        SoundOnOff(m_GameManager.GetComponent<GameManagerScript>().GetEffectSoundBool());

        m_LaunchArcRenderer = GetComponent<LaunchArcRenderer>();
        m_LaunchArcRenderer.SetCharacterBoostPower(m_Boost_Power_Max);

        m_Gage_Up = true;

        gameObject.GetComponentInChildren<MeshCollider>().enabled = true;
        m_BeepColliderCollection = GetComponentsInChildren<BoxCollider>();
        foreach(BoxCollider bc in m_BeepColliderCollection)
        {
            bc.enabled = true;
        }
        Transform[] trs = GetComponentsInChildren<Transform>();
        foreach (Transform tr in trs)
        {
            if (tr.CompareTag("PlayerRender"))
            {
                m_BodyParts.Add(tr);
            }
        }

        m_GoalMade = false;
        m_Original_Color = new Color(0, 0.1098039f, 0.7490196f);
        m_JumpColor = new Color(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {    
        RaycastHit hit;

        // if Player hit floor then make it ready to jump and change color
        if (Physics.Raycast(transform.position + new Vector3(0, 0, 0.7f), Vector3.down, out hit, 1.5f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, -0.7f), Vector3.down, out hit, 1.5f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, 0), Vector3.down , out hit, 1.5f))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                m_Body_LineMat.SetColor("_EmissionColor", m_JumpColor);
                m_Grounded = true;
            }            
        }
        // if Player is not on the ground, then make it unable to jump
        // and change color
        else
        {
            m_Body_LineMat.SetColor("_EmissionColor", m_Original_Color);    
            m_Grounded = false;
        }


        // if Player falls under -10 on axis y, explode it        
        if (m_Player_Tr.position.y <= -10f && m_Start && !m_GoalMade) {
            Fail();
            return;
        }

        // Ready m_Gage(using for Jump Power)
        // when Jump Btn Pressed and Player is on ground
        if (CrossPlatformInputManager.GetButton("Jump") && m_Grounded)
        {
            m_JumpReady = true;

            // m_Gage_Up == true -> m_Gage for Jump Power increases
            // m_Gage_Up == false -> m_Gage for Jump Power decreases

            if (m_Gage <= 0)            
                m_Gage_Up = true;            
            else if (m_Gage >= 1f)
                m_Gage_Up = false;            

            if (m_Gage_Up)
                m_Gage += 1f / m_Boot_Limit * Time.deltaTime * 2;            
            else            
                m_Gage -= 1f / m_Boot_Limit * Time.deltaTime * 2;            

            // while m_Gage changes render arc dots
            // so that Player can predict Jump Power
            m_LaunchArcRenderer.SetRenderLine(true);
        }
        // GetButton("Jump") == false ->
        //                    check ready to jump
        else
        {
            m_LaunchArcRenderer.SetRenderLine(false);

            // if Player is ready to jump, make m_Jump true 
            // m_Jump is used in FIxedUadate()
            if (m_JumpReady && m_Gage >= 0.1)            
                m_Jump = true;
                        
            if (m_Gage > 0f && !m_Jump)            
                m_Gage = 0;            
            else if (m_Gage <= 0f)            
                m_Gage = 0;
            
        }

        // LaunchArcRenderer needs m_Gage for 
        // draw dots to predict where Player land
        m_LaunchArcRenderer.SetCharacterGage(m_Gage);              
    }


    private void FixedUpdate()
    {
        if(m_Start && m_Jump)
        {
            m_Jump = false; m_JumpReady = false;

            // initialize character's velocity
            m_Player_Rb.velocity = Vector3.zero; 
            // m_Gage affect only forward direction on z axis
            m_Player_Rb.AddForce(0, m_Boost_Power_Max, m_Boost_Power_Max * m_Gage, ForceMode.VelocityChange);
        }

    }

    public void StartCharacter()
    {
        m_Start = true;
    }

    public void PlayExplosionSound()
    {
        m_ExplodeAudioSource.Play();
    }

    public void Fail()
    {

        m_Gage = 0f;
        PlayExplosionSound();

        m_GameManager.GetComponent<GameManagerScript>().Fail();

        CrossPlatformInputManager.SetButtonUp("Jump");

        m_LaunchArcRenderer.SetRenderLine(false);

        gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
        foreach (BoxCollider bc in m_BeepColliderCollection)
        {
            bc.enabled = false;
        }

        foreach (Transform tr in m_BodyParts)
        {
            tr.gameObject.SetActive(false);
        }

        GameObject.Instantiate(m_ExplodeParticle, transform.position, transform.rotation);

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;

        m_Start = false;
    }

    public void ResetPlayer()
    {
        m_Gage = 0f;

        m_Start = true;

        transform.position = m_GameManager.GetComponent<GameManagerScript>().GetResetPos();
        transform.rotation = new Quaternion(0, 0, 0, 0);

        gameObject.GetComponentInChildren<MeshCollider>().enabled = true;
        foreach (BoxCollider bc in m_BeepColliderCollection)
        {
            bc.enabled = true;
        }

        foreach (Transform tr in m_BodyParts)
        {
            tr.gameObject.SetActive(true);
        }

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;

        m_Player_Rb.velocity = new Vector3(0, 0, 0);
        m_Player_Rb.angularVelocity = new Vector3(0, 0, 0);

        m_BeepAudioSource.clip = m_BeepSound;        
    }

    public void SetGround(bool ground)
    {
        m_Grounded = ground;
    }

    public void SoundOnOff(bool on)
    {
        if (on)
        {
            m_BeepAudioSource.mute = !on;
            m_ExplodeAudioSource.mute = !on;
        }
        else
        {
            m_BeepAudioSource.mute = !on;
            m_ExplodeAudioSource.mute = !on;
        }
    }

    public void GoalMade(bool goal)
    {
        if (m_BeepAudioSource.isPlaying)
            m_BeepAudioSource.Stop();

        if (goal)
        {
            m_BeepAudioSource.clip = m_SuccesSound;
            m_BeepAudioSource.Play();
        }
        m_GoalMade = goal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosive"))
        {
            Fail();
        }else if (other.CompareTag("ExplodeAndDestroy"))
        {
            other.GetComponentInParent<HeightDestroy>().DestroyFromPlayer();
            Fail();
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }


    private void OnCollisionStay(Collision collision)
    {
    }

    public void ChangeToWhite()
    {
        m_Body_LineMat.color = Color.white;
    }

    public void ChangeToBlue()
    {
        m_Body_LineMat.color = Color.blue;

    }

    public bool GetGoalMade()
    {
        return m_GoalMade;
    }
}
