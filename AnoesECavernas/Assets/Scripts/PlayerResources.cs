using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    //moedas
    public int coins = 0;
    public int passiveQtt = 2;
    //texto da vida
    [SerializeField] TextMeshProUGUI coinsText;

    public GameObject towerMenu;
    public List<int> towerPrices;
    
    public List<GameObject> towerButtons;

    void Start()
    {
        
        for(int i = 0; i < towerMenu.GetComponent<TowerButton>().towerPrefabs.Count; i++) 
        {
            towerPrices.Add(towerMenu.GetComponent<TowerButton>().towerPrefabs[i].GetComponent<Barrack>().price);
        }
        //inicializar texto da vida igual a vida
        coinsText.text = coins.ToString();
        //não da pra passar parâmetro desse jeito (usar coroutine?)
        InvokeRepeating("PassiveIncome", 0, 1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && towerButtons[0].GetComponent<Button>().interactable == true) {
            towerButtons[0].GetComponent<Button>().onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && towerButtons[1].GetComponent<Button>().interactable == true)
        {
            towerButtons[1].GetComponent<Button>().onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && towerButtons[2].GetComponent<Button>().interactable == true)
        {
            towerButtons[2].GetComponent<Button>().onClick.Invoke();
        }
    }

    public void PassiveIncome()
    {
        AddResource(passiveQtt);
        //atualizar texto
        coinsText.text = coins.ToString();
    }

    public void AddResource(int qtt)
    {
        coins += qtt;
        coinsText.text = coins.ToString();
        for (int i = 0; i < towerMenu.GetComponent<TowerButton>().towerPrefabs.Count; i++)
        {
            if(towerPrices.Count > 0)
            {
                if (coins < towerPrices[i])
                {
                    towerButtons[i].GetComponent<Button>().interactable = false;
                    
                }
                else
                {
                    towerButtons[i].GetComponent<Button>().interactable = true;

                }
            }
            
        }
    }

}
