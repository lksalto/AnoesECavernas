using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{
    private bool ended=false;
    //vida
    public int life = 5;

    //texto da vida
    [SerializeField] TextMeshProUGUI lifeText;

    public GameObject endGame;
    // Start is called before the first frame update
    void Start()
    {
        //inicializar texto da vida igual a vida
        lifeText.text = life.ToString();
    }
    private void Update()
    {
        CheckGameOver();
        if (ended)
        {
            if (FindObjectOfType<UpgradeMenu>() != null)
            {
                Destroy(FindObjectOfType<UpgradeMenu>().gameObject, 0.2f);
            }
        }
    }

    public void playerTakeDamage(int dmg)
    {
        life -= dmg;
        if(life <= 0 )
        {
            LoseGame();
        }
        if(FindObjectOfType<SoundManager>()!=null) FindObjectOfType<SoundManager>().Play("PlayerDamage");
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
        Time.timeScale = 0;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame(bool victory)
    {
        endGame.SetActive(true);
        if (victory)
        {
            endGame.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "VITORIA <3";
        }
        else
        {
            endGame.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "DERROTA </3";
        }
        ended = true;
    }
    private void CheckGameOver()
    {
        EnemyPathing[] obj = FindObjectsOfType<EnemyPathing>();
        EnemySpawner spawn = FindObjectOfType<EnemySpawner>();
        if (spawn != null && obj != null && obj.Length <= 0 && spawn.end)
        {
            if (!ended) { EndGame(true); }
        }
    }

}
