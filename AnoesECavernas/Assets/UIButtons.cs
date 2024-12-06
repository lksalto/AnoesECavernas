using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeGamobjActive(GameObject gamobj) 
    {
        gamobj.SetActive(!gamobj.activeInHierarchy);
    }
}
