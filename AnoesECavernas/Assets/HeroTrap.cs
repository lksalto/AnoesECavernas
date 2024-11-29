using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeroTrap : MonoBehaviour
{
    public GameObject Trap;
    public GameObject CursorTrap;
    public Tilemap TrapCaminho;
    public bool TrapPlaceable,ButtonUI;
    private AiMove aimove;
    private GameObject cursor,InstantCursorTrap;
    private GameObject HeroManager;
    public GameObject HeroButton;
    public float timer;
    public float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        aimove = GetComponent<AiMove>();
        cursor = aimove.cursor;
        HeroManager = GameObject.FindGameObjectWithTag("HeroManager");
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if (TrapPlaceable&&timer>cooldown)
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
                timer=0;
                HeroButton.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        if (timer > cooldown)
        {
            HeroButton.transform.GetChild(1).gameObject.SetActive(false);
        }
        int notTheHeroCount = 0;
        for(int i = 0; i < HeroManager.GetComponent<HeroManager>().heros.Length; i++) 
        {
            if(HeroManager.GetComponent<HeroManager>().heros[i] == transform.gameObject) 
            {
                HeroButton.SetActive(true);
                if (ButtonUI && timer>cooldown) 
                {
                    InstantCursorTrap = Instantiate(CursorTrap) as GameObject;
                    InstantCursorTrap.transform.SetParent(cursor.transform);
                    InstantCursorTrap.transform.position = cursor.transform.position;
                    InstantCursorTrap.GetComponent<Cursor>().caminho = TrapCaminho;

                    aimove.enabled = false;
                    TrapPlaceable = true;
                    ButtonUI = false;
                }
                break;
            }
            else 
            {
                notTheHeroCount++;
            }
        }
        if (notTheHeroCount >= HeroManager.GetComponent<HeroManager>().heros.Length) 
        {
            HeroButton.SetActive(false);
        }
        
    }
    public void Button()
    {
        if (timer > cooldown) { ButtonUI = true; }
    }
}
