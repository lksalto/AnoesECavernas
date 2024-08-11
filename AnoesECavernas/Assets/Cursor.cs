using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Cursor : MonoBehaviour
{
    public Grid grid;
    public Tilemap caminho;
    public bool caminhoBool;
    public GameObject Sprit;
    public Vector2Int TamanhoTilezed;
    private Color color;
    public Color NewColor;
    public Vector3 offset;
    public Vector3Int offsetTilezed;
    public Color Gizmo;

    private void OnDrawGizmos()
    {
        //seleciona o grid
        if (GameObject.FindGameObjectWithTag("Tile").activeInHierarchy) grid = GameObject.FindGameObjectWithTag("Tile").GetComponent<Grid>();
        else { grid = null; Debug.Log("Usando o tilemap errado"); }

        Gizmos.color = Gizmo;
        Vector3 Size = new Vector3(TamanhoTilezed.x *grid.cellSize.x, TamanhoTilezed.y*grid.cellSize.y, 0);
        Vector3 Posi= new Vector3(transform.position.x + (offset.x*(TamanhoTilezed.x-1)) - (offsetTilezed.x * grid.cellSize.x)
                                , transform.position.y + (offset.y*(TamanhoTilezed.y-1)) - (offsetTilezed.y * grid.cellSize.y)
                                , transform.position.z);
        Gizmos.DrawCube(Posi, Size);
    }

    // Start is called before the first frame update
    void Awake()
    {
        //seleciona o grid
        if(GameObject.FindGameObjectWithTag("Tile").activeInHierarchy) grid = GameObject.FindGameObjectWithTag("Tile").GetComponent<Grid>();
        else { grid = null; Debug.Log("Usando o tilemap errado"); }
        color = Sprit.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //pega posi��o global do mouse pela camera
        Vector3 rmouseposition= Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //armezena o valor original de z
        float z=transform.position.z;

        //coloca o pivot do objeto no centro da cell que o mouse est�
        transform.position =grid.CellToWorld(grid.WorldToCell(rmouseposition));

        //restaura o valor de z e aplica offset
        transform.position = offset + new Vector3(transform.position.x,transform.position.y,z) + new Vector3(offsetTilezed.x*grid.cellSize.x, offsetTilezed.y * grid.cellSize.y, 0);
        int[,] matrix=new int[TamanhoTilezed.x,TamanhoTilezed.y];
        int matrixSum = 0;
        for (int i = 0; i < TamanhoTilezed.x; i++) 
        {
            for (int j = 0; j < TamanhoTilezed.y; j++)
            {
                Vector3 gridedpos = rmouseposition + new Vector3((i * grid.cellSize.x), (j * grid.cellSize.y), 0);
                if (caminho.HasTile(grid.WorldToCell(gridedpos)) && caminho.GetTile(grid.WorldToCell(gridedpos)) != null)
                {
                    matrix[i, j] = 1;
                    
                }
                else { matrix[i, j] = 0; }
                matrixSum += matrix[i, j];
            }
        }
        
        if (matrixSum!=0) 
        {
            
            Sprit.GetComponent<SpriteRenderer>().color = NewColor;
            caminhoBool = true;
        }
        else 
        { 
            Sprit.GetComponent<SpriteRenderer>().color = color; 
            caminhoBool = false;
        }
    }
}
