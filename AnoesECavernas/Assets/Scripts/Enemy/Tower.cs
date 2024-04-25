using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class Tower : ScriptableObject
{
    public Sprite sprite;
    public int atkDmg;
    public float atkSpeed;
    public float atkCd;
    public bool atkClosestEnemy;
    public int atkRange;
    public GameObject bulletPrefab;
    public UnityEngine.Transform spawnPoint;

    private GameObject target;

    
    public enum Towertype
    {
        Barrack=1, Cannon=2, Magic=3
    };


    public void detectEnemy(Vector3 pos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, atkRange);
        if(colliders.Length > 0 ) 
        {
            target = colliders[0].gameObject;
            targetEnemy(target);
        }


    }

    public void targetEnemy(GameObject tgt)
    {
        //apenas para testar
        if((!atkClosestEnemy || atkClosestEnemy) && atkCd < 0 && atkSpeed > 0)
        {
            atkCd = 5 / atkSpeed;
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Arrow>().target = tgt;
        }
    }


}
