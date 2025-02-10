using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool Escable;
    private bool active=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&Escable) 
        {
            active = transform.GetChild(0).gameObject.activeInHierarchy;
            transform.GetChild(0).gameObject.SetActive(!active);
            if (!active)
            { 
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;

            }
        }
    }
    public void AtivaGameobj(GameObject gamobj) 
    {
        gamobj.SetActive(true);
    }
    public void DesativaGameobj(GameObject gamobj)
    {
        gamobj.SetActive(false);
    }
    public void NovaCena(string scene) 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
    public void Sair() 
    {
        Application.Quit();
    }
    
}
