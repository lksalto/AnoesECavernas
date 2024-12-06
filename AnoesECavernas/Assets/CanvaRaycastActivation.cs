using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaRaycastActivation : MonoBehaviour
{
    public GameObject canvagroup;
    public GameObject[] newcanvs;
    public bool AllChild;
    // Start is called before the first frame update
    void Start()
    {
        if (AllChild)
        {
            newcanvs = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                newcanvs[i] = transform.GetChild(i).gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!AllChild)
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

        if (AllChild)
        {
            for (int i = 0; i < newcanvs.Length; i++)
            {
                if (newcanvs[i].GetComponent<CanvasGroup>() != null)
                {
                    if (newcanvs[i].activeInHierarchy)
                    {
                        newcanvs[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                    else
                    {
                        newcanvs[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }
                }
            }
        }
    }
}
