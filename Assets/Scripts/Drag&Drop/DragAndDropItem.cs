using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Every "drag and drop" item must contain this script
/// </summary>
[RequireComponent(typeof(Image))]
public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    static public DragAndDropItem draggedItem;                                      // Item that is dragged now
    static public GameObject icon;                                                  // Icon of dragged item
    
    public Color empty = new Color();                                       // Sprite color for empty cell
    public Color full = new Color();                                        // Sprite color for filled cell


    public delegate void DragEvent(DragAndDropItem item);
    static public event DragEvent OnItemDragStartEvent;                             // Drag start event
    static public event DragEvent OnItemDragEndEvent;                               // Drag end event

    public GameObject cell;                                                  // Source cell of dragged item
    public string power;
    public AudioSource source;

    /// <summary>
    /// This item is dragged
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedItem = this;                                                         // Set as dragged item
        icon = new GameObject("Icon");                                              // Create object for item's icon
        Image image = icon.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.raycastTarget = true;                                                // Disable icon's raycast for correct drop handling
        RectTransform iconRect = icon.GetComponent<RectTransform>();
        // Set icon's dimensions
        iconRect.sizeDelta = new Vector2(0.50f , 0.50f);
        SetBackgroundState(true);
        Canvas canvas = GetComponentInParent<Canvas>();                             // Get parent canvas
        if (canvas != null)
        {
            // Display on top of all GUI (in parent canvas)
            icon.transform.SetParent(canvas.transform, true);                       // Set canvas as parent
            icon.transform.SetAsLastSibling();                                      // Set as last child in canvas transform
        }
        if (OnItemDragStartEvent != null)
        {
            OnItemDragStartEvent(this);                                             // Notify all about item drag start
        }
    }

    /// <summary>
    /// Every frame on this item drag
    /// </summary>
    /// <param name="data"></param>
    public void OnDrag(PointerEventData data)
    {
        if (icon != null)
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = 10.0f;
            icon.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }
    }

    /// <summary>
    /// This item is dropped
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (icon != null)
        {
            source.Play();
            var screenPoint = Input.mousePosition;
            screenPoint.z = 10.0f;
            icon.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            GameManager.instance.CreatePower(power, icon.transform.position);
            Destroy(icon);                                                          // Destroy icon on item drop
            SetBackgroundState(false);
        }
        MakeVisible(true);                                                          // Make item visible in cell
        if (OnItemDragEndEvent != null)
        {
            OnItemDragEndEvent(this);                                               // Notify all cells about item drag end
        }
        draggedItem = null;
        icon = null;
    }

    /// <summary>
    /// Enable item's raycast
    /// </summary>
    /// <param name="condition"> true - enable, false - disable </param>
    public void MakeRaycast(bool condition)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = condition;
        }
    }

    /// <summary>
    /// Enable item's visibility
    /// </summary>
    /// <param name="condition"> true - enable, false - disable </param>
    public void MakeVisible(bool condition)
    {
        GetComponent<Image>().enabled = condition;
    }


    private void SetBackgroundState(bool condition)
    {
        cell.GetComponent<Image>().color = condition ? full : empty;
    }
}
