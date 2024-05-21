using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    //vida
    public float life = 1;
    public float magicResist = 0.5f;
    //qnts dinheiros ele vale ao morrer
    public int value = 1;
    public int speed = 1;
    //particle effect do sangue
    [SerializeField] GameObject bloodParticle;

    //Referencia pra dar dinheiro ao player
    PlayerResources playerResources;



    private void Awake()
    {
        playerResources = FindObjectOfType<PlayerResources>();
    }


    //função pública para tomar dano, adicionar uma referência no script da torre para bater nele
    public void TakeHit(float dmg, int type)
    {
        if ((type == 1))
        {
            if(magicResist <= 0)
            {
                magicResist = 1;
            }
            dmg /= magicResist;
        }
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
