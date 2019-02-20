using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraMovement : MonoBehaviour {
    public GameObject m_Target;

    Transform m_Target_Tr;

    float m_initial_z_Water;
    Transform m_Water_Tr;

    float m_CamRecent_Z;

    Vector3 m_Offset = new Vector3(13.75f, 5f, -3f);  // Perspective
    Vector3 m_Rotation = new Vector3(15.891f, -58.712f, -1.21f);
    
    public float m_smoothSpeed = 0.125f;

    public bool m_Following{get; set;}

	// Use this for initialization
	void Start () {
        m_Target_Tr = m_Target.GetComponent<Transform>();
        transform.rotation = Quaternion.Euler(m_Rotation);

        m_Following = true;
    }

    // Update is called once per frame
    void Update () {
        if (m_Following)
        {
            Vector3 desiredPos = m_Target_Tr.position + m_Offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, m_smoothSpeed);
            transform.position = smoothedPos;
        }
    }

    public void ShakeCamera()
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 0.5f);
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elaspsed = 0.0f;

        while(elaspsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elaspsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
