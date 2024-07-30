using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject upgradeMenuPrefab;

    public int countInstances = 0;
    Barrack barrack; // barraca a ser melhorada
    GameObject upgradeMenuInstance;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && (hit.collider.CompareTag("Tower")))
            {
                if(countInstances == 1)
                {
                    Destroy(upgradeMenuInstance);
                }
                countInstances = 1;
                barrack = hit.collider.GetComponent<Barrack>();
                if (barrack != null)
                {
                    Vector3 menuPosition = hit.collider.transform.position;
                    upgradeMenuInstance = Instantiate(upgradeMenuPrefab, menuPosition, Quaternion.identity);
                    upgradeMenuInstance.GetComponent<UpgradeMenu>().SetTower(barrack);
                }
            }
            else if(hit.collider == null)
            {
                countInstances = 0;
                Debug.Log("DELETA INSTANCIA");
                Destroy(FindObjectOfType<UpgradeMenu>().gameObject,0.2f);
            }
            
        }

    
    }

    public void UpgradeTower()
    {

        if(barrack != null)
        {
            Debug.Log("upou");
            barrack.UpLevel();
        }
    }
}
