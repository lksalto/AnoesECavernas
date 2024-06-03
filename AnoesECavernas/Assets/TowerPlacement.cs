using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacement : MonoBehaviour
{
    public GameObject Tower;
    private bool caminho,MouseUI;
    private int coinsaux;
    public int coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsaux = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerResources>().coins;
        if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.CompareTag("Botão"))
        { 
            MouseUI = true;
        }
        else {MouseUI = false;}
        caminho = GetComponent<Cursor>().caminhoBool;
        if (Input.GetMouseButtonDown(0) && !caminho && !MouseUI && coins <= coinsaux)
        {
            Instantiate(Tower,transform.position,transform.rotation,null);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerResources>().coins -= coins;
            transform.gameObject.SetActive(false);
        }
    }

}
