using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTrap : MonoBehaviour
{
    public GameObject Trap;
    public GameObject CursorTrap;
    public bool TrapPlaceable,ButtonUI;
    private AiMove aimove;
    private GameObject cursor,InstantCursorTrap;
    private GameObject HeroManager;
    public GameObject HeroButton;
    // Start is called before the first frame update
    void Start()
    {
        aimove = GetComponent<AiMove>();
        cursor = aimove.cursor;
        HeroManager = GameObject.FindGameObjectWithTag("HeroManager");
    }

    // Update is called once per frame
    void Update()
    {
        int notTheHeroCount = 0;
        for(int i = 0; i < HeroManager.GetComponent<HeroManager>().heros.Length; i++) 
        {
            Debug.Log("1");
            if(HeroManager.GetComponent<HeroManager>().heros[i] == transform.gameObject) 
            {
                HeroButton.SetActive(true);
                if (Input.GetKeyDown(KeyCode.L)||ButtonUI) 
                {
                    InstantCursorTrap = Instantiate(CursorTrap) as GameObject;
                    InstantCursorTrap.transform.SetParent(cursor.transform);
                    InstantCursorTrap.transform.position = cursor.transform.position;

                    aimove.enabled = false;
                    TrapPlaceable = true;
                }
                break;
            }
            else 
            {
                Debug.Log("2");
                notTheHeroCount++;
            }
        }
        if (notTheHeroCount >= HeroManager.GetComponent<HeroManager>().heros.Length) 
        {
            HeroButton.SetActive(false);
        }
        if (TrapPlaceable)
        {
            bool NoCaminho = cursor.GetComponent<Cursor>().caminhoBool;
            if (NoCaminho && Input.GetMouseButtonUp(0))
            {
                GameObject Intanciado = Instantiate(Trap) as GameObject;
                Intanciado.transform.position = cursor.transform.position;

                InstantCursorTrap.SetActive(false);
                Destroy(InstantCursorTrap);

                aimove.enabled = true;
                TrapPlaceable = false;
            }
        }
    }
}
