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
        for(int i = 0; i < HeroManager.GetComponent<HeroManager>().heros.Length; i++) 
        {
            if(HeroManager.GetComponent<HeroManager>().heros[i] == transform.gameObject) 
            {
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
