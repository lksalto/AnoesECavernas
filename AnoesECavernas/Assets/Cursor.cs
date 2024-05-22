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
    public Vector3Int offsetTilezed;
    // Start is called before the first frame update
    void Start()
    {
        //seleciona o grid
        if(GameObject.FindGameObjectWithTag("Tile").activeInHierarchy) grid = GameObject.FindGameObjectWithTag("Tile").GetComponent<Grid>();
        else { grid = null; Debug.Log("Usando o tilemap errado"); }
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
        transform.position = offset + new Vector3(transform.position.x,transform.position.y,z) + new Vector3(offsetTilezed.x*grid.cellSize.x, offsetTilezed.y * grid.cellSize.y, 0);

        
        if (caminho.HasTile(grid.WorldToCell(rmouseposition)) && caminho.GetTile(grid.WorldToCell(rmouseposition))!=null) 
        {
            Debug.Log("caminho");
            Sprit.GetComponent<SpriteRenderer>().color = NewColor;
        }
        else { Sprit.GetComponent<SpriteRenderer>().color = color; }
    }
}
