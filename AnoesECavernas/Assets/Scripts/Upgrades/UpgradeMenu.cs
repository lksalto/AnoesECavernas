using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeMenu : MonoBehaviour
{
    private Barrack tower;
    public Canvas canvas;
    public TextMeshProUGUI lvl;
    public TextMeshProUGUI priceTxt;
    public PlayerResources playerResources;
    public Button btn;

    public int price;
    private void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();

    }
    public void SetTower(Barrack selectedTower)
    {
        playerResources = FindObjectOfType<PlayerResources>();
        tower = selectedTower;
        lvl.text = selectedTower.getLevel().ToString();
        if(tower.lvl == 3)
        {
            priceTxt.text = "";
        }
        else
        {
            price = selectedTower.price;
            priceTxt.text = price.ToString();
        }
        
        

    }

    private void Update()
    {
        btn.interactable = playerResources.coins >= price;
    }
    public void UpgradeTower()
    {
        Debug.Log("tentando upar " + tower);
        if (tower != null)
        {
            Debug.Log("upou");
            tower.UpLevel();
        }
    }



}
