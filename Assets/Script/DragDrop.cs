using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragDrop : MonoBehaviour
{

    //TouchPositions
    private Transform selectedObject; //Transform of the object that has been selected (cloud)
    private Vector2 currentTouchPos; //Position where the player has touched
    private Plane dragPlane; //Plane which the player can drag and drop the cloud on
    private Vector3 offset; //Determines exact location of touched object

    //Input references
    [Header("Input Reference")]
    public InputActionReference trackingAction; //Dragging
    public InputActionReference clickingAction; //Tapping


    [Header("Draggable Object")]
    //Object Reference
    [SerializeField] private GameObject dragObject;

    //Camera reference
    [Header("Camera Reference")]
    public Camera camera; //Reference to player camera

    /// <summary>
    /// Gets the selected objected based on hit transform
    /// </summary>
    /// <param name="context"></param>
    private void OnTouchPress(InputAction.CallbackContext context)
    {
        Ray ray = camera.ScreenPointToRay(currentTouchPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            selectedObject = hit.transform;
            dragPlane = new Plane(-camera.transform.forward, hit.point);
            offset = selectedObject.position - hit.point;
        }
    }

    /// <summary>
    /// Gets the position of the selected object within the world
    /// </summary>
    /// <param name="context"></param>
    private void OnTouchPosition(InputAction.CallbackContext context)
    {
        currentTouchPos = context.ReadValue<Vector2>();

        if (selectedObject != null)
        {
            Ray ray = camera.ScreenPointToRay(currentTouchPos);

            if (dragPlane.Raycast(ray, out float distance))
            {
                selectedObject.position = ray.GetPoint(distance) + offset;
            }
        }
    }

    /// <summary>
    /// Sets the selected object as null when the player stops touching it
    /// </summary>
    /// <param name="context"></param>
    private void OnTouchRelease(InputAction.CallbackContext context)
    {
        selectedObject = null;
    }


    /// <summary>
    /// Enables clicking and tracking actions
    /// </summary>
    private void OnEnable()
    {
        trackingAction.action.Enable();
        clickingAction.action.Enable();

        trackingAction.action.performed += OnTouchPosition;
        clickingAction.action.performed += OnTouchPress;
        clickingAction.action.canceled += OnTouchRelease;
    }


    /// <summary>
    /// Disables clicking and tracking actions
    /// </summary>
    private void OnDisable()
    {
        trackingAction.action.performed -= OnTouchPosition;
        clickingAction.action.performed -= OnTouchPress;
        clickingAction.action.canceled -= OnTouchRelease;

        trackingAction.action.Disable();
        clickingAction.action.Disable();
    }
}
