using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CursorTorre(GameObject gamobj) 
    {
        Transform parent= gamobj.transform.parent;
        for(int i = 0; i < parent.childCount; i++) 
        {
            parent.GetChild(i).gameObject.SetActive(false);
        }
        gamobj.SetActive(true);
    }
}