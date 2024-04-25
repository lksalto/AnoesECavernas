using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    //vida
    public int life = 1;

    //qnts dinheiros ele vale ao morrer
    public int value = 1;

    //particle effect do sangue
    [SerializeField] GameObject bloodParticle;

    //Referencia pra dar dinheiro ao player
    PlayerResources playerResources;


    private void Awake()
    {
        playerResources = FindObjectOfType<PlayerResources>();
    }
    private void Update()
    {
        //apenas para testar a função de dano, lembrar de tirar isso
        if(Input.GetKeyDown(KeyCode.X))
        {
            TakeHit(1);
        }
    }

    //função pública para tomar dano, adicionar uma referência no script da torre para bater nele
    public void TakeHit(int dmg)
    {
        Debug.Log("OUCH");
        life -= dmg;
        if(life <= 0 )
        {
            Die();
        }
    }

    //Sequencia de ações ao morrer (podemos add uma animação ou algo assim)
    void Die()
    {
        playerResources.AddResource(value);
        GameObject blood = Instantiate(bloodParticle, transform.position, Quaternion.identity);
        blood.transform.parent = null;
        Destroy(blood, 0.8f);
        Destroy(gameObject);
    }
}
