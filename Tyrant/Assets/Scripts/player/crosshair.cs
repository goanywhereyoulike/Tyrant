using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorSprite;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    // Start is called before the first frame update
    void Awake()
    {
        //Cursor.visible = false;
        Cursor.SetCursor(cursorSprite, hotSpot, cursorMode);
    }

    // Update is called once per frame
    /*void Update()
    {
        Vector2 crosshair = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = crosshair;
    }*/
}
