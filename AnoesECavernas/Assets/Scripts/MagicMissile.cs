using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{

    public Vector3 targetGround;

    public float dmg;
    public GameObject target;
    public float speed;
    // 0 = fisico, 1 = magico
    public int dmgType;


    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            targetGround = target.transform.position;
            GoToTarget(gameObject.transform, target.transform.position);

        }
        else
        {
            GoToTarget(gameObject.transform, targetGround);

        }



    }
    public void GoToTarget(Transform bulletTransform, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - bulletTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        bulletTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bulletTransform.position = Vector3.MoveTowards(bulletTransform.position, targetPosition, speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.02f && Mathf.Abs((transform.position.y - targetPosition.y)) < 0.02f)
        {
            if (target != null)
            {
                if (target.GetComponent<EnemyLife>() != null)
                {
                    DealDamage(dmg, dmgType);

                }
            }

            Destroy(gameObject);

        }

    }

    public void DealDamage(float dmg, int type)
    {

        if (target.GetComponent<EnemyLife>() != null)
        {
            target.GetComponent<EnemyLife>().TakeHit(dmg, type);

        }
        Destroy(gameObject);


    }
}
