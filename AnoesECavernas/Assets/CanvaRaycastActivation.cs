using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaRaycastActivation : MonoBehaviour
{
    public GameObject canvagroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canvagroup.activeInHierarchy) 
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else 
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
