using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
 
    [SerializeField] GameObject target;

    // 0 = barraca, 1 = magic, 2 = cannon
    [SerializeField] int type;

    public Sprite sprite;
    public int atkDmg;
    public float atkSpeed;
    public float atkCd;
    public bool atkClosestEnemy;
    public int atkRange;
    
    public GameObject bulletPrefab;
    public UnityEngine.Transform spawnPoint;


    void Start()
    {

    }


    void Update()
    {
        atkCd -= Time.deltaTime;
        DetectEnemy(transform.position);
        
        
    }


    public void targetEnemy(GameObject tgt)
    {
        //apenas para testar
        if ((!atkClosestEnemy || atkClosestEnemy) && atkCd < 0 && atkSpeed > 0)
        {
            atkCd = 5 / atkSpeed;
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity) ;
           
            switch (type)
            {
                case 0: 
                    bullet.GetComponent<Arrow>().target = tgt;
                    break;
                case 1:
                    bullet.GetComponent<MagicMissile>().target = tgt;
                    break;
                case 2:
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.GetComponent<CannonBall>().target = tgt;
                    break;
            }
            
        }
    }

    public void DetectEnemy(Vector3 pos)
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, atkRange);
        if (colliders.Length > 0)
        {
            target = colliders[0].gameObject;
            targetEnemy(target);
        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

}
