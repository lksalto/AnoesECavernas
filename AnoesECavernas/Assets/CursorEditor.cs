using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorEditor : MonoBehaviour
{
    public bool WaypointClick, PathClick;
    public GameObject PointClicked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Desenha uma linha até o objeto carregado
        if(PointClicked != null) 
        {
            GetComponent<LineRenderer>().SetPosition(0, PointClicked.transform.position);
            GetComponent<LineRenderer>().SetPosition(1, transform.position);
        }
        else 
        {

            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, transform.position);
        }
    }
}
