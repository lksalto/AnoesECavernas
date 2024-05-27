using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditorWaypoint : MonoBehaviour
{
    public GameObject waypoint,PathPrefab,CurrentPath;
    private bool MouseUI;
    public int CurrentBifurcations;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.CompareTag("Botão"))
        {
            MouseUI = true;
        }
        else { MouseUI = false; }
        if (Input.GetMouseButtonDown(0) && !MouseUI)
        {
            Waypoint();
            transform.gameObject.SetActive(false);
        }
    }
    void Waypoint() 
    {
        if(CurrentPath == null) 
        {
            GameObject AllPath = GameObject.FindGameObjectWithTag("AllPath");
            Instantiate(PathPrefab, transform.position, transform.rotation, AllPath.transform);
            CurrentPath = AllPath.transform.GetChild(0).gameObject;
        }
        else
        {
            Instantiate(waypoint, transform.position, transform.rotation, CurrentPath.transform);

        }
    }
    void Bifurca() 
    {
        if (CurrentPath != null)
        {
            CurrentBifurcations++;
            CurrentPath.GetComponent<WaypointChanger>().nextpath = new GameObject[CurrentBifurcations];
            Instantiate(waypoint, transform.position, transform.rotation, CurrentPath.transform);


        }
    }
    void Fim() { }
}
