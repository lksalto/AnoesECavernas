using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int life = 1;
    [SerializeField] GameObject bloodParticle;

    private void Update()
    {
        //apenas para testar a função de dano, lembrar de tirar isso
        if(Input.GetKeyDown(KeyCode.X))
        {
            //TakeHit(1);
        }
    }

    //função pública para tomar dano, adicionar uma referência no script da torre para bater nele
    public void TakeHit(int dmg)
    {
        life -= dmg;
        if(life <= 0 )
        {
            Die();
        }
    }

    //Sequencia de ações ao morrer (podemos add uma animação ou algo assim
    void Die()
    {
        GameObject blood = Instantiate(bloodParticle, transform.position, Quaternion.identity);
        blood.transform.parent = null;
        Destroy(gameObject);
    }
}
