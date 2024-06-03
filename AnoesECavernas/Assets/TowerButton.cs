using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public List<GameObject> towerPrefabs;

    private void Start()
    {
        
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = towerPrefabs[i].GetComponent<Barrack>().price.ToString();
        }
    }
    private void Update()
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

        //se usar isso, gasta dinheiro ao clicar no botão
        //FindObjectOfType<PlayerResources>().AddResource(-gamobj.GetComponent<TowerPlacement>().Tower.GetComponent<Barrack>().price);
    }


}
