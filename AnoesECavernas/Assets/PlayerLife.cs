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

    void LoseGame()
    {
        //no momento restarta a cena, mas iremos mudar para mostrar o game over
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
