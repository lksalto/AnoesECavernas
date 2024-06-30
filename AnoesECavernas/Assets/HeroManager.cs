using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeroManager : MonoBehaviour
{
    public GameObject[] heros;
    public float HerosRadius;
    private Tilemap caminho;
    public Grid grid;
    public GameObject cursor;
    public bool MouseOnTop;
    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        caminho = cursor.GetComponent<Cursor>().caminho;
        grid = cursor.GetComponent<Cursor>().grid;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < heros.Length; i++) 
        {
            for(int j = 0; j < heros.Length; j++) 
            {
                if (heros[i]!=heros[j] &&OnRadiusOf(heros[i].transform, heros[j].transform, HerosRadius)) 
                {
                    if (heros[i].GetComponent<AiMove>().reached|| heros[j].GetComponent<AiMove>().reached) 
                    {
                        heros[i].GetComponent<AiMove>().Reached();
                        heros[j].GetComponent<AiMove>().Reached();
                    }
                    Vector3 direc= heros[i].transform.position - heros[j].transform.position;
                    heros[i].transform.position+= direc*heros[i].GetComponent<AiMove>().Speed*Time.deltaTime;
                }
            }
            //Checa se o heroi esta fora do caminho e armazenana sua ultima posição dentro do caminho
            if (caminho.HasTile(grid.WorldToCell(heros[i].transform.position)))
            {
                heros[i].GetComponent<AiMove>().LastPos = heros[i].transform.position;
            }
            else 
            {                
                heros[i].GetComponent<AiMove>().ReturnToLastPos();
            }
        }
        if (Input.GetMouseButtonDown(3)) 
        {
            for (int i = 0; i < heros.Length; i++)
            {
                //DeSelectHero(heros[i]);
                heros[i].GetComponent<AiMove>().selecionado = false;
                Debug.Log(heros[i].name);
            }
            heros = new GameObject[0];
        }
        
    }
    public void AddHero(GameObject hero) 
    {
        GameObject[] aux = heros;
        heros=new GameObject[aux.Length+1];
        aux.CopyTo(heros,0);
        heros[aux.Length] = hero;
    }
    public void DeSelectHero(GameObject hero) 
    {
        int ind=0;
        GameObject[] aux = new GameObject[heros.Length-1];
        for(int i = 0; i < heros.Length; i++) 
        {
            if(heros[i] == hero) 
            {
                heros[i]=null;
            }
            else 
            {
                aux[ind] = heros[i];
                ind++;
            }
        }
        heros=aux;
    }
    private bool OnRadiusOf(Transform gamobj,Transform gamobjRad, float radius)
    {
        if (Vector3.Distance(gamobj.position, gamobjRad.position) <= radius)
        {
            Debug.Log("sim");
            return true;
        }
        else
        {
            return false;
        }
    }
}
