using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class CreateBorderTile : MonoBehaviour
{
    public Vector2Int HorizontalPoints, VerticalPoints;
    public Vector3Int CurrentTile;
    public Tile tile;
    public Tilemap tilemap,Border;
    public int BorderThick = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int x = HorizontalPoints.x; x < HorizontalPoints.y; x++) 
        {
            for(int y = VerticalPoints.x; y < VerticalPoints.y; y++) 
            {
                CurrentTile = new Vector3Int(x, y,0);
                Vector3Int[] cardials = new Vector3Int[(BorderThick*2+1)* (BorderThick * 2 + 1)];
                int ind=0;
                for(int ix = -BorderThick; ix <= BorderThick; ix++) 
                {
                    for(int iy= -BorderThick; iy <= BorderThick; iy++) 
                    {
                        cardials[ind] = new Vector3Int(x+ix,y+iy, 0);
                        ind++;
                    }
                }
                if (tilemap.HasTile(CurrentTile) && tilemap.GetTile(CurrentTile) != null) 
                {
                    for(int i = 0; i < cardials.Length; i++) 
                    {
                        if (tilemap.GetTile(cardials[i]) == null) 
                        {
                            Border.SetTile(cardials[i], tile);
                        }
                    }
                }
            }
        }
    }
}
