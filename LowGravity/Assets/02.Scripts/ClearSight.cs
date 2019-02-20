using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSight : MonoBehaviour {

    public float DistanceToPlayer = 5.0f;
    public Material TransparentMaterial = null;
    public float FadeInTimeout = 0.6f;
    public float FadeOutTimeout = 0.2f;
    public float TargetTransparency = 0.3f;

    public GameObject m_Player;

    private void Update()
    {
        RaycastHit[] hits; // you can also use CapsuleCastAll() 
                           // TODO: setup your layermask it improve performance and filter your hits. 

        Vector3 direction = (m_Player.GetComponent<Transform>().position + new Vector3(0,1f,0)) - transform.position;
              

        hits = Physics.RaycastAll(transform.position, direction.normalized, direction.magnitude);
        Debug.DrawLine(transform.position, m_Player.GetComponent<Transform>().position);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Renderer[] R = hit.collider.GetComponentsInChildren<Renderer>();
                if (R == null)
                {
                    continue;
                }
                // no renderer attached? go to next hit 
                // TODO: maybe implement here a check for GOs that should not be affected like the player
                foreach (Renderer r in R)
                {
                    //Debug.Log(r.gameObject.name);
                   // r.material = TransparentMaterial;

                    AutoTransparent AT = r.GetComponent<AutoTransparent>();
                    if (AT == null) // if no script is attached, attach one
                    {
                        AT = r.gameObject.AddComponent<AutoTransparent>();
                    }
                    AT.BeTransparent(); // get called every frame to reset the falloff
                }
            }
        }
    }
}
