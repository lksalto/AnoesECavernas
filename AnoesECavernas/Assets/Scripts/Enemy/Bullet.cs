using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    
    public int dmg;
    public GameObject target;
    public float speed;

    

    public void GoToTarget(Transform bulletTransform, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - bulletTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        bulletTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bulletTransform.position = Vector3.MoveTowards(bulletTransform.position, targetPosition, speed * Time.deltaTime);
    }

    public void DealDamage(int dmg, Transform myTransform)
    {
        
        //com certeza tem um jeito melhor de fazer
        if (Mathf.Abs(myTransform.position.x - target.transform.position.x) < 0.02f && Mathf.Abs((myTransform.position.y - target.transform.position.y)) < 0.02f)
        {
            target.GetComponent<EnemyLife>().TakeHit(dmg);
            Destroy(myTransform.gameObject);
        }

    }

}
