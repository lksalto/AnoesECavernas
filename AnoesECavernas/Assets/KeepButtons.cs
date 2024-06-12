using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepButtons : MonoBehaviour
{
    public float height, width, Oheight, Owidth, posX, posY, Oposx, Opsy;
    public float ButtonHeight, ButtonWidth, OButtonHeight, OButtonWidth;
    public int ButtonCount,ButtonDiscount;
    public RectTransform rec;
    public GameObject[] childs;
    // Start is called before the first frame update
    void Start()
    {
        rec = GetComponent<RectTransform>();
        Oheight = rec.sizeDelta.y;
        Owidth = rec.sizeDelta.x;
        width = rec.sizeDelta.x;
        posX = rec.localPosition.x;
        posY = rec.localPosition.y;
        findButtons();
        OButtonWidth = childs[0].GetComponent<RectTransform>().rect.width;
        OButtonHeight = childs[0].GetComponent<RectTransform>().rect.height;
        ButtonHeight = OButtonHeight;
        Oposx = childs[0].GetComponent<RectTransform>().localPosition.x;
        Opsy = childs[0].GetComponent<RectTransform>().localPosition.y;
        ButtonCount = transform.childCount- ButtonDiscount;
    }

    // Update is called once per frame
    void Update()
    {
        height = Oheight - ButtonHeight;
        rec.sizeDelta = new Vector2(width, height);
        rec.localPosition = new Vector2(posX, posY + Oheight / 2 - height / 2);
        if (transform.childCount - ButtonDiscount != childs.Length)
        {
            findButtons();
            ButtonCount = transform.childCount- ButtonDiscount;
        }
        else
        {
            if (ButtonCount > 0) ButtonWidth = OButtonWidth / ButtonCount;
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].GetComponent<RectTransform>().sizeDelta = new Vector2(ButtonWidth, ButtonHeight);
                float posx = Oposx - (OButtonWidth / 2) + (ButtonWidth / 2) + ButtonWidth * i;
                float posy = Opsy;
                childs[i].GetComponent<RectTransform>().localPosition = new Vector2(posx, posy);
            }
        }


    }
    void findButtons()
    {
        ButtonDiscount = 0;
        GameObject[] temp = new GameObject[transform.childCount];
        int index=0;
        for (int i = 0; i < temp.Length; i++)
        {
            if (transform.GetChild(i).GetComponent<Button>()!=null) 
            {
                temp[index] = transform.GetChild(i).gameObject;
                index++;
            }
            else 
            {
                ButtonDiscount++;
            }
        }
        childs = new GameObject[transform.childCount-ButtonDiscount];
        for(int i = 0; i < childs.Length; i++) 
        {
            childs[i]=temp[i];
        }
    }
}
