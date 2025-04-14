using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _startPosition;
    private Transform _startParent;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;

    [Tooltip("Optional: Snap distance to target. If 0, snapping is disabled.")]
    public float snapDistance = 0f;

    [Tooltip("Optional: Target Transform to snap to. Leave null for no snapping.")]
    public Transform snapTarget = null;


    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if(_canvasGroup == null)
        {
            // Add CanvasGroup if it doesn't exist.  This allows the object to be dragged smoothly even behind other UI elements
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        _rectTransform = GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _startParent = transform.parent;
        transform.SetParent(transform.root); // Bring to front during drag
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;

        if (snapTarget != null && snapDistance > 0f)
        {
            float distance = Vector3.Distance(transform.position, snapTarget.position);

            if (distance <= snapDistance)
            {
                transform.position = snapTarget.position;
                transform.SetParent(snapTarget);
            }
            else
            {
                 transform.position = _startPosition;
                 transform.SetParent(_startParent);
            }
        }
        else
        {
            transform.position = _startPosition;
            transform.SetParent(_startParent);
        }
    }


    public void ResetToStart()
    {
        transform.position = _startPosition;
        transform.SetParent(_startParent);
    }

    //Optional method for setting the original start variables, can be called before dragging actually occurs.
    public void SetStart(Vector3 position, Transform parent)
    {
        _startPosition = position;
        _startParent = parent;
    }
}