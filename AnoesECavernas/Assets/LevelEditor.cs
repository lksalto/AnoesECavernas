using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelEditor : MonoBehaviour
{
   
    public EditorLevel[] AllTiles;
    public GameObject DragFilter;

    [System.Serializable]
    public struct EditorTile
    {
        public string Name;
        public Color ColorInEditor;
        public bool UseGameObject;
        public GameObject gamObj;
        public GameObject parent;
        public bool UseTile;
        public Tile tile;
        public Tilemap TilemapUsado;

    }

    [System.Serializable]
    public struct EditorLevel
    {
        public string Name;
        public Texture2D map;
        public Vector3Int offsetTilezed;
        public Vector3 offset;
        public EditorTile[] tiles;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) 
        { 
            GenerateLevel(); 
        }
    }
    void GenerateLevel() 
    {
        for (int k = 0; k < AllTiles.Length; k++)
        {
            Texture2D map = AllTiles[k].map;
            for (int i = 0; i < map.width; i++)
            {
                for (int j = 0; j < map.height; j++)
                {
                    GenerateTile(i, j, map);
                }
            }
        }
    }
   void GenerateTile(int x,int y,Texture2D map) 
    {
        Color CurrentColor = map.GetPixel(x,y);
        for (int j = 0; j < AllTiles.Length; j++) 
        {
            for (int i = 0; i < AllTiles[j].tiles.Length; i++)
            {
                if (CurrentColor == AllTiles[j].tiles[i].ColorInEditor)
                {
                    if (AllTiles[j].tiles[i].UseGameObject)
                    {
                        Debug.Log("Gam");
                        Vector3 worldpos = AllTiles[j].tiles[i].TilemapUsado.CellToWorld(new Vector3Int(x, y, 0) - AllTiles[j].offsetTilezed)+ AllTiles[j].offset;
                        worldpos = new Vector3(worldpos.x, worldpos.y, 0);
                        Instantiate(AllTiles[j].tiles[i].gamObj, worldpos, Quaternion.identity, AllTiles[j].tiles[i].parent.transform);
                    }
                    if (AllTiles[j].tiles[i].UseTile)
                    {
                        AllTiles[j].tiles[i].TilemapUsado.SetTile(new Vector3Int(x, y, 0) - AllTiles[j].offsetTilezed, AllTiles[j].tiles[i].tile);
                    }
                }
            }
        }
    }
    public void ChoseImage(GameObject filter) 
    {
        GetComponent<DragTest>().enabled= true;
        DragFilter = filter;
        filter.gameObject.SetActive(true);
    }
}
