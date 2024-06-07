using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointChanger : MonoBehaviour
{
    public GameObject[] nextpath;
    public bool end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < nextpath.Length; i++) 
        {
            if (nextpath[i] != transform.gameObject && nextpath[i] !=null)
            {
                Debug.DrawLine(transform.position, nextpath[i].transform.position);
            }
        }
    }
}
