using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Vector3 targetGround;

    public float dmg;
    public float radius = 5.0f;
    public GameObject target;
    public float speed;
    // 0 = fisico, 1 = magico
    public int dmgType = 0;

    private void Start()
    {
        if (target != null)
        {
            targetGround = target.transform.position;
            

        }

    }

    // Update is called once per frame
    void Update()
    {

        GoToTarget(gameObject.transform, targetGround);



    }
    public void GoToTarget(Transform bulletTransform, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - bulletTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        bulletTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bulletTransform.position = Vector3.MoveTowards(bulletTransform.position, targetPosition, speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.02f && Mathf.Abs((transform.position.y - targetPosition.y)) < 0.02f)
        {
            DealDamage(dmg, dmgType);

        }

    }

    public void DealDamage(float dmg, int type)
    {

        Vector2 position = transform.position;
        int layerMask = LayerMask.GetMask("Enemy");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, radius, layerMask);

        foreach (Collider2D hitCollider in hitColliders)
        {
            EnemyLife enemyLife = hitCollider.GetComponent<EnemyLife>();
            if (enemyLife != null)
            {
                enemyLife.TakeHit(dmg, type);
            }
        }
        Destroy(gameObject);
    }
}
