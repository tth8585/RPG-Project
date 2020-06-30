
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorMain;
    public Texture2D cursorTalk;
    public Texture2D cursorCombat;
    public Texture2D cursorClick;
    public Texture2D cursorLoot;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private void Update()
    {
        if (Camera.main == null)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 10000))
        {
            if (hit.transform.tag == "Enemy")
            {
                Cursor.SetCursor(cursorCombat, hotSpot, cursorMode);
            }
            else if (hit.transform.tag == "NPC")
            {
                Cursor.SetCursor(cursorTalk, hotSpot, cursorMode);
            }
            else if (hit.transform.tag == "Loot")
            {
                Cursor.SetCursor(cursorLoot, hotSpot, cursorMode);
            }
            else if (hit.transform.tag == "Click")
            {
                Cursor.SetCursor(cursorClick, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorMain, hotSpot, cursorMode);
            }
        }
    }
}
