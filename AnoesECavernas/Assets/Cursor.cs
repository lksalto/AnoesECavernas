using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cursor : MonoBehaviour
{
    private Grid grid;
    public Tilemap caminho;
    public GameObject Sprit;
    private Color color;
    public Color NewColor;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        //seleciona o grid
        grid = GameObject.FindGameObjectWithTag("Tile").GetComponent<Grid>();
        color = Sprit.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //pega posição global do mouse pela camera
        Vector3 rmouseposition= Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //armezena o valor original de z
        float z=transform.position.z;

        //coloca o pivot do objeto no centro da cell que o mouse está
        transform.position =grid.CellToWorld(grid.WorldToCell(rmouseposition));

        //restaura o valor de z e aplica offset
        transform.position = offset + new Vector3(transform.position.x,transform.position.y,z);

        
        if (caminho.HasTile(grid.WorldToCell(rmouseposition)) && caminho.GetTile(grid.WorldToCell(rmouseposition))!=null) 
        {
            Debug.Log("caminho");
            Sprit.GetComponent<SpriteRenderer>().color = NewColor;
        }
        else { Sprit.GetComponent<SpriteRenderer>().color = color; }
    }
}
