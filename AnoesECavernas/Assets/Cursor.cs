using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        //seleciona o grid
        grid = GameObject.FindGameObjectWithTag("Tile").GetComponent<Grid>();
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

        //restaura o valor de z
        transform.position = new Vector3(transform.position.x,transform.position.y,z);
    }
}
