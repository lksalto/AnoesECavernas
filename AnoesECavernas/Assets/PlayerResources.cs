using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerResources : MonoBehaviour
{
    //moedas
    public int coins = 0;
    public int passiveQtt = 2;
    //texto da vida
    [SerializeField] TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        //inicializar texto da vida igual a vida
        coinsText.text = coins.ToString();
        //não da pra passar parâmetro desse jeito (usar coroutine?)
        InvokeRepeating("PassiveIncome", 0, 1);
    }    

    public void PassiveIncome()
    {
        coins += passiveQtt;
        //atualizar texto
        coinsText.text = coins.ToString();
    }

    public void AddResource(int qtt)
    {
        coins += qtt;
        coinsText.text = coins.ToString();
    }

}
