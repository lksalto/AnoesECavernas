using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrap : MonoBehaviour
{
    public float expTime = 1.8f;
    public float detection = 2f;
    public float radius;
    public float maxRadius;
    public float rootDuration = 2f;
    public float slow = 2;

    bool activated = false;
    [SerializeField] GameObject indicatorPrefab;
    SpriteRenderer indicatorSR;
    GameObject indicator;
    // Start is called before the first frame update
    void Start()
    {
        maxRadius = radius;
        // Start the coroutine to fade in the alpha
        
    }

    private void Update()
    {
        Activate();
    }
    private IEnumerator FadeInIndicator(float duration)
    {
        float targetAlpha = 1f;
        float elapsed = 0f;

        Color color = indicatorSR.color;
        color.a = 0;
        indicatorSR.color = color;



        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0, targetAlpha, elapsed*1.05f / duration);
            radius = Mathf.Lerp(detection, maxRadius, elapsed * 2f / duration);
            color.a = alpha;
            indicatorSR.color = color;
            RootEnemies(false);
            yield return null;
        }

        // Ensure the alpha is set to the target value at the end
        Color finalColor = indicatorSR.color;
        finalColor.a = targetAlpha;
        indicatorSR.color = finalColor;
        RootEnemies(true);
    }

    public void RootEnemies(bool exploded)
    {

        Vector2 position = transform.position;
        int layerMask = LayerMask.GetMask("Enemy");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, radius, layerMask);
        if(exploded)
        {
            foreach (Collider2D hitCollider in hitColliders)
            {
                EnemyLife enemyLife = hitCollider.GetComponent<EnemyLife>();
                if (enemyLife != null)
                {
                    
                    enemyLife.GetRooted(rootDuration);
                }
            }
            Destroy(indicator);
            Destroy(gameObject);
        }
        else
        {
            foreach (Collider2D hitCollider in hitColliders)
            {
                EnemyLife enemyLife = hitCollider.GetComponent<EnemyLife>();
                if (enemyLife != null)
                {
                    enemyLife.Slow(slow);
                }
            }
        }
        
        //GameObject exp = Instantiate(particleExp, transform.position, Quaternion.identity);
        //exp.transform.parent = null;
        //Destroy(exp, 1f);

    }

    void Activate()
    {
        Vector2 position = transform.position;
        int layerMask = LayerMask.GetMask("Enemy");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, 2, layerMask);

        if (!activated)
        {
            if(hitColliders.Length > 0)
            {
                activated = true;
                indicator = Instantiate(indicatorPrefab, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity);
                indicator.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1);

                indicator.transform.parent = null;
                indicatorSR = indicator.GetComponent<SpriteRenderer>();
                StartCoroutine(FadeInIndicator(expTime));
            }
            
        }

        
    }
}
