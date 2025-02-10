using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

            if (hit.collider != null && (hit.collider.CompareTag("Tower")) && !IsMouseOverUpgradeUI())
            {

                barrack = hit.collider.GetComponent<Barrack>();
                if (countInstances == 1)
                {
                    if(upgradeMenuInstance!=null) Destroy(upgradeMenuInstance);
                    //tempBar = null;

                }
                countInstances = 1;
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
                if (FindObjectOfType<UpgradeMenu>() != null) { Destroy(FindObjectOfType<UpgradeMenu>().gameObject, 0.2f); }
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
    bool IsMouseOverUpgradeUI()
    {
        if (FindObjectOfType<UpgradeMenu>() != null)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(FindObjectOfType<UpgradeMenu>().transform.GetChild(0).GetComponentInChildren<RectTransform>(), Input.mousePosition, null);
        }
        else 
        {
            return false;
        }
    }
}
