using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class PlayerSlot : MonoBehaviour, IDropHandler
{
    public static string redSlotName { get; set; }
    public static string yellowSlotName { get; set; }
    public static string blueSlotName { get; set; }
    public static int playerCount { get; set; }

    private void Awake()
    {
        playerCount = 3;
    }

    /// <summary>Called when [drop].
    /// Assigns players to a color (red, yellow, blue).</summary>
    /// <param name="eventData">The event data.</param>
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.transform.SetParent(this.gameObject.transform, true);

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
