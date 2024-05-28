using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorWaypoint : MonoBehaviour
{
    public float radius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rmouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dif= Mathf.Abs(rmouseposition.x - transform.position.x);
        float dif2= Mathf.Abs(rmouseposition.y - transform.position.y);
        if (dif<= radius && dif2<=radius)
        { 
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 50);
        }
        else 
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
    }
}
