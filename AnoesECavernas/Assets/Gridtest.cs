using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridtest : MonoBehaviour
{
    public Vector3 mouseWorldpos;
    public Vector3Int mouseCellpos;
    public Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        //seleciona o grid
        grid= GameObject.FindGameObjectWithTag("Tile").GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        //pega a posição do mouse pela camera
        mouseWorldpos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transforma na coordenada do grid
        mouseCellpos=grid.WorldToCell(mouseWorldpos);
    }
}
