using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerSlot : MonoBehaviour, IDropHandler
{
    public static string redSlotName;
    public static string yellowSlotName;
    public static string blueSlotName;

    public void OnDrop(PointerEventData eventData)
    {
 
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;

            //Get player name of button dropped on Slot
            string playerName = eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text;

            //Assign according to color
            if (this.name == "RedSlot")
            {
                redSlotName = playerName;
            }
            else if (this.name == "YellowSlot")
            {
                yellowSlotName = playerName;
            }
            if (this.name == "BlueSlot")
            {
                blueSlotName = playerName;
            }
        }
    }
}
