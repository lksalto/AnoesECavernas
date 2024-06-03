using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{
    //vida
    int life = 5;

    //texto da vida
    [SerializeField] TextMeshProUGUI lifeText;

    public GameObject endGame;
    // Start is called before the first frame update
    void Start()
    {
        //inicializar texto da vida igual a vida
        lifeText.text = life.ToString();
    }

    public void playerTakeDamage(int dmg)
    {
        life -= dmg;
        if(life <= 0 )
        {
            LoseGame();
        }
        //atualizar texto
        lifeText.text = life.ToString();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void LoseGame()
    {
        //no momento restarta a cena, mas iremos mudar para mostrar o game over
        EndGame(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame(bool victory)
    {
        endGame.SetActive(true);
        if(victory)
        {
            endGame.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "VITORIA <3";
        }
        else
        {
            endGame.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "DERROTA </3";
        }
    }
}
