using UnityEngine;
using UnityEngine.InputSystem;


/*
 * Name: Juliana Mills
 * Date: 11/17/26
 * Function: Handles cloud position and fruit spawning
 */

public class SpawnerCloud : MonoBehaviour

{
    //Position checks
    private Vector2 currPos; //Position of where the cloud is
    private Vector2 spawnPos; //Position of where the cloud wll spawn fruit from

    //Fruit spawn checks
    private bool newFruit; 
    private bool hasSpawned; //Checks if the fruit has spawned
    private int whichFruit; //Checks the enum of which fruit it is that has spawned


    [Header("Inputs")]
    [SerializeField] private InputActionReference clickAction;
    [SerializeField] private InputActionReference pointerPosAction;

    [Header("Transforms")]
    [SerializeField] private RectTransform panelTransform;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private RectTransform objTransform;

    [Header("Camera")]
    [SerializeField] private Camera camera;

    private Vector2 offset;
    private bool isDragging;

    private bool IsPointerOverObject(Vector2 pointerPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(objTransform, pointerPos, camera);
    }
    
    /// <summary>
    /// Gets the position of the object that is grabbed
    /// </summary>
    /// <param name="ctx"></param>
    private void OnTouchStarted(InputAction.CallbackContext ctx)
    {
        var pointerPos = pointerPosAction.action.ReadValue<Vector2>();

        if (IsPointerOverObject(pointerPos))
        {
            isDragging = true;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, pointerPos, camera, out var localPos);

            offset = panelTransform.anchoredPosition - localPos;
        }    
    }

    private void OnPointMoved(InputAction.CallbackContext ctx)
    {
        if (!isDragging) return;

        var pointerPos = ctx.ReadValue<Vector2>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, pointerPos, camera, out var localPos);

        float yPos = (panelTransform.anchoredPosition).y;
        float xPos = (localPos + offset).x;

        if (xPos < -447 && xPos > -453)
        {
            panelTransform.anchoredPosition = new Vector2(xPos, yPos);
        }
    }

    /// <summary>
    /// Sets is dragging to false when the player lets go
    /// </summary>
    /// <param name="ctx"></param>
    private void OnTouchCanceled(InputAction.CallbackContext ctx)
    {
        isDragging = false;
    }

    private void OnEnable()
    {
        clickAction.action.started += OnTouchStarted;
        clickAction.action.canceled += OnTouchCanceled;
        pointerPosAction.action.performed += OnPointMoved;
    }

    private void OnDisable()
    {
        clickAction.action.started -= OnTouchStarted;
        clickAction.action.canceled -= OnTouchCanceled;
        pointerPosAction.action.performed -= OnPointMoved;

        isDragging = false;
    }
}
