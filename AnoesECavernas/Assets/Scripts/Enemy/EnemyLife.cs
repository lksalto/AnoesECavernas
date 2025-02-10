using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyLife : MonoBehaviour
{
    //vida
    public float life = 1;
    float maxLife;
    public float magicResist = 0.5f;
    public float initialSpeed;
    public float speed = 10f;
    public int dmg = 1;
    public float slowTimer=0;
    public float slowCounter=0;
    bool isRooted = false;

    //qnts dinheiros ele vale ao morrer
    public int value = 1;
    //particle effect do sangue
    [SerializeField] GameObject bloodParticle;

    //Referencia pra dar dinheiro ao player
    PlayerResources playerResources;

    private float flashDuration = 0.1f;

    [SerializeField] SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    [SerializeField] Material whiteFlashMaterial;
    public GameObject hitCanvasPrefab;
    public Image lifeBar;
    [SerializeField] Transform hitSpawn;


    Animator anim;
    private void Awake()
    {
        if (GetComponent<Animator>() != null) { anim = GetComponent<Animator>(); }
        else { anim = GetComponentInChildren<Animator>(); }
        playerResources = FindObjectOfType<PlayerResources>();
        maxLife = life;
        originalMaterial = spriteRenderer.material;

    }

    private void Update()
    {
        CheckSpeed();
        
    }

    //função pública para tomar dano, adicionar uma referência no script da torre para bater nele
    public void TakeHit(float dmg, int type)
    {

        TriggerFlash();
        if ((type == 1))
        {
            if(magicResist <= 0)
            {
                magicResist = 1;
            }
            dmg /= magicResist;
        }
        life -= dmg;

        GameObject hit = Instantiate(hitCanvasPrefab, hitSpawn.position, Quaternion.identity);

        hit.GetComponent<HitText>().text.text = dmg.ToString();
        if (type == 0)
        {
            hit.GetComponent<HitText>().text.color = Color.red;
        }
        else
        {
            hit.GetComponent<HitText>().text.color = Color.blue;
        }
        Destroy(hit, 0.3f);

        lifeBar.fillAmount = life/maxLife;
        if (life <= 0)
        {
            Die();
        }
    }

    //Sequencia de ações ao morrer (podemos add uma animação ou algo assim)
    void Die()
    {
        playerResources.AddResource(value);
        GameObject blood = Instantiate(bloodParticle, transform.position, Quaternion.identity);
        blood.transform.parent = null;
        //CheckGameOver();
        Destroy(blood, 0.8f);
        Destroy(gameObject);
    }
    public void TriggerFlash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        spriteRenderer.material = whiteFlashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = originalMaterial;
    }

    public void GetRooted(float secs)
    {
        StartCoroutine(RootEnemy(secs));
    }

    IEnumerator RootEnemy(float s)
    {
        isRooted = true;
        speed = 0;
        Color color = spriteRenderer.color;
        spriteRenderer.color = new Color(0.678f, 0.847f, 1.0f, 1.0f);
        anim.speed = 0;
        yield return new WaitForSeconds(s);
        isRooted = false;
        anim.speed = 1;
        speed = initialSpeed;
        spriteRenderer.color = color;
    }

    public void Slow(float qtt)
    {
        
        slowCounter = qtt;
        speed = initialSpeed / qtt;
    }

    void CheckSpeed()
    {
        if(!isRooted)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer > 0)
            {
                speed = initialSpeed;

            }
            else
            {
                speed = initialSpeed;
            }
        }
        
    }
    public void CheckGameOver()
    {
        EnemyPathing[] obj = FindObjectsOfType<EnemyPathing>();
        EnemySpawner spawn = FindObjectOfType<EnemySpawner>();
        if (spawn !=null && obj!=null &&obj.Length == 1 && spawn.end)
        {
            FindObjectOfType<PlayerLife>().EndGame(true);
        }
    }
}
