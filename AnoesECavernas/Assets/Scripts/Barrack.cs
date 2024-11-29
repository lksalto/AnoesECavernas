using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
 
    [SerializeField] GameObject target;
    [SerializeField] SpriteRenderer sr;
    // 0 = barraca, 1 = magic, 2 = cannon
    [SerializeField] int type;

    public Sprite sprite;
    public Sprite[] UpgradeSprites;
    public int atkDmg;
    public float atkSpeed;
    public float atkCd;
    public bool atkClosestEnemy;
    public int atkRange;
    
    public GameObject bulletPrefab;
    public UnityEngine.Transform spawnPoint;

    [SerializeField] LayerMask layerMask;
    //lista 2D
    public List<List<object>> enemiesDistances = new List<List<object>>();

    //Lista dos inimigos (primeira linha)
    public List<object> gameObjectRow = new List<object>();

    //Lista das velocidades
    public List<object> floatRow = new List<object>();
    int enemyIndex;
    int maxIndex;
    public int price = 5;

    //upgrades
    public GameObject upgradeMenuPrefab;
    private GameObject upgradeMenuInstance;
    public int numOfClicks = 0; //apenas para nao aparecer o menu qndo colocar a torre
    public int lvl = 1;
    void Start()
    {

    }


    void Update()
    {
        atkCd -= Time.deltaTime;
        DetectEnemy(transform.position);
        
        
    }


    public void TargetEnemy(GameObject tgt)
    {
        //apenas para testar
        if ((!atkClosestEnemy || atkClosestEnemy) && atkCd < 0 && atkSpeed > 0)
        {
            atkCd = 5 / atkSpeed;
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
            switch (type)
            {
                case 0: 
                    bullet.GetComponent<Arrow>().target = tgt;
                    bullet.GetComponent<Arrow>().dmg = atkDmg;
                    break;
                case 1:
                    bullet.GetComponent<MagicMissile>().target = tgt;
                    bullet.GetComponent<MagicMissile>().dmg = atkDmg;
                    break;
                case 2:
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.GetComponent<CannonBall>().target = tgt;
                    bullet.GetComponent<CannonBall>().dmg = atkDmg;
                    break;
            }
            
        }
    }

    public void DetectEnemy(Vector3 pos)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, atkRange, layerMask);

        if (colliders.Length > 0)
        {
            // Clear the rows to avoid duplicating entries each frame
            gameObjectRow.Clear();
            floatRow.Clear();

            foreach (Collider2D e in colliders)
            {
                gameObjectRow.Add(e.gameObject);
                floatRow.Add(e.gameObject.GetComponent<EnemyPathing>().distanceTravelled);
            }

            // Add the rows to the enemiesDistances list
            enemiesDistances.Clear(); // Clear previous data to avoid duplicates
            enemiesDistances.Add(new List<object>(gameObjectRow)); // Use new instances
            enemiesDistances.Add(new List<object>(floatRow)); // Use new instances

            // Find the index with the maximum value in floatRow
            int maxIndex = FindMaxIndex(floatRow);

            // Assign target based on the found maxIndex
            if (target == null)
            {
                enemyIndex = maxIndex;
                target = (GameObject)enemiesDistances[0][enemyIndex];
            }
            else if (enemyIndex != maxIndex)
            {
                enemyIndex = maxIndex;
                target = (GameObject)enemiesDistances[0][enemyIndex];
            }

            // Perform actions with the target
            TargetEnemy(target);
        }
        else
        {
            target = null;
        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    int FindMaxIndex(List<object> floatRow)
    {
        int maxIndex = 0;
        float maxValue = (float)floatRow[0];

        for (int i = 1; i < floatRow.Count; i++)
        {
            float currentValue = (float)floatRow[i];
            if (currentValue > maxValue)
            {
                maxValue = currentValue;
                maxIndex = i;
            }
        }

        return maxIndex;
    }

    public void CloseUpgradeMenu()
    {
        if (upgradeMenuInstance != null)
        {
            Destroy(upgradeMenuInstance);
        }
    }
    public void UpLevel()
    {
        if(lvl < 3)
        {
            lvl++;
            price *= 5;
            atkSpeed = atkSpeed * lvl;
            atkDmg = atkDmg * lvl;

            int LvlIndex = lvl - 2;
            if (UpgradeSprites.Length > LvlIndex) 
            { 
                sr.sprite = UpgradeSprites[LvlIndex]; 
            }
            else
            {

                Color clr = sr.color;
                clr.g -= 0.25f;
                clr.b -= 0.25f;
                sr.color = clr;
            }

        }

    }
    public int getLevel()
    {
        return lvl;
    }
}
