using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using B83.Win32;


public class DragTest : MonoBehaviour
{
    public Texture2D map;
    DropInfo dropInfo = null;
    class DropInfo
    {
        public string file;
        public Vector2 pos;
    }
    void OnEnable()
    {
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += OnFiles;

    }
    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }

    void OnFiles(List<string> aFiles, POINT aPos)
    {
        string file = "";
        // scan through dropped files and filter out supported image types
        foreach (var f in aFiles)
        {
            var fi = new System.IO.FileInfo(f);
            var ext = fi.Extension.ToLower();
            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
            {
                file = f;
                break;
            }
        }
        // If the user dropped a supported file, create a DropInfo
        if (file != "")
        {
            var info = new DropInfo
            {
                file = file,
                pos = new Vector2(aPos.x, aPos.y)
            };
            dropInfo = info;
        }
    }

    void LoadImage(DropInfo aInfo)
    {
        if (aInfo == null)
            return;
        var data = System.IO.File.ReadAllBytes(aInfo.file);
        var tex = new Texture2D(1, 1);
        tex.LoadImage(data);
        if (map != null)
            Destroy(map);
        map = tex;
        GetComponent<LevelEditor>().AllTiles[0].map = map;
        GetComponent<LevelEditor>().DragFilter.SetActive(false);
        GetComponent<DragTest>().enabled = false;
    }

    private void OnGUI()
    {
        DropInfo tmp = null;
        if (Event.current.type == EventType.Repaint && dropInfo != null)
        {
            tmp = dropInfo;
            dropInfo = null;
        }
       
        LoadImage(tmp); 
    }
}
