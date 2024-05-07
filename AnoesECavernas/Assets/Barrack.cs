using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
 
    [SerializeField] GameObject target;

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
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) ;
            bullet.GetComponent<Arrow>().target = tgt;
        }
    }

    public void DetectEnemy(Vector3 pos)
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, atkRange);
        if (colliders.Length > 0)
        {
            Debug.Log("a");
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
