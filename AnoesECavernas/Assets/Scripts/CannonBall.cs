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
    [SerializeField] GameObject indicatorPrefab;
    SpriteRenderer indicatorSR;
    GameObject indicator;
    private float travelTime;
    
    public float maxHeight = 1.0f; // Maximum height of the cannonball's motion
    public float currentHeight = 0.0f; // Current height of the cannonball
    public float ascendSpeed = 1.0f; // Speed at which the cannonball ascends
    public float descendSpeed = 1.0f; // Speed at which the cannonball descends
    [SerializeField] Transform cannonballTransform;
    [SerializeField] Transform shadowTransform;
    public float initialDistance;
    [SerializeField] GameObject particleExp;
    private void Start()
    {
        
        if (target != null)
        {
            targetGround = target.transform.position;
            indicator = Instantiate(indicatorPrefab, new Vector3(targetGround.x, targetGround.y, 2), Quaternion.identity);
            indicator.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1);

            // Get the SpriteRenderer component and set its initial alpha to 0
            indicatorSR = indicator.GetComponent<SpriteRenderer>();
            Color color = indicatorSR.color;
            color.a = 0;
            indicatorSR.color = color;
            indicator.transform.parent = null;
            // Calculate the travel time
            float distance = Vector3.Distance(transform.position, targetGround);
            travelTime = distance / speed;

            // Start the coroutine to fade in the alpha
            StartCoroutine(FadeInIndicator(travelTime));

            initialDistance = Vector3.Distance(transform.position, targetGround);
            speed = speed / (speed/initialDistance);
        }
    }



    // Update is called once per frame
    void Update()
    {
        GoToTarget(gameObject.transform, targetGround);
        UpdateCannonballMotion();
    }

    public void GoToTarget(Transform bulletTransform, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - bulletTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        
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
        GameObject exp = Instantiate(particleExp, transform.position, Quaternion.identity);
        exp.transform.parent = null;
        FindObjectOfType<SoundManager>().Play("Cannon");
        Destroy(exp, 1f);
        Destroy(indicator,0.03f);
        Destroy(gameObject);
    }

    private void UpdateCannonballMotion()
    {
        float distanceTraveled = Vector3.Distance(transform.position, targetGround);
        float normalizedDistance = distanceTraveled / initialDistance;

        // Curva em parabola
        float maxHeightOffset = 1.0f; 
        currentHeight = Mathf.Sin(normalizedDistance * Mathf.PI) * maxHeight + maxHeightOffset;


        Vector3 cannonballPosition = cannonballTransform.localPosition;
        cannonballPosition.y = currentHeight;
        cannonballTransform.localPosition = cannonballPosition;

        // Sombra
        shadowTransform.localScale = new Vector3(1 / (currentHeight + 1) * 2, 1 / (currentHeight + 1) * 2, 1);
    }
    private IEnumerator FadeInIndicator(float duration)
    {
        float targetAlpha = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0, targetAlpha, elapsed / duration);
            Color color = indicatorSR.color;
            color.a = alpha;
            indicatorSR.color = color;
            yield return null;
        }

        // Ensure the alpha is set to the target value at the end
        Color finalColor = indicatorSR.color;
        finalColor.a = targetAlpha;
        indicatorSR.color = finalColor;
    }
}
