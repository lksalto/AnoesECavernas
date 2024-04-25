using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Bullet bullet;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(target!=null)
        {
            bullet.GoToTarget(gameObject.transform, target.transform.position);
            DealDamage(bullet.dmg);
        }
        
    }


    public void DealDamage(int dmg)
    {
        Debug.Log("A");
        //com certeza tem um jeito melhor de fazer
        if(Mathf.Abs(transform.position.x - target.transform.position.x) < 0.02f && Mathf.Abs((transform.position.y - target.transform.position.y)) < 0.02f)
        {
            target.GetComponent<EnemyLife>().TakeHit(dmg);
            Destroy(gameObject);
        }
        
    }
}
