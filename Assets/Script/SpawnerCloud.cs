using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/*
 * Name: Juliana Mills
 * Date: 11/12/26
 * Function: Handles cloud position and fruit spawning
 */

public class SpawnerCloud : MonoBehaviour

{
    //Position checks
    private Vector2 currPos; //Position of where the cloud is
    private Vector2 currentTouchPos; //Position where the player has touched
    private Vector2 spawnPos; //Position of where the cloud wll spawn fruit from
    private Transform selectedObject; //Transform of the object that has been selecte (cloud)
    private Vector2 offset;
    private Plane dragPlane; //Plane which the player can drag and drop the cloud on

    //Camera reference
    [Header("Camera Reference")]
    public Camera camera; //Reference to player camera

    //Fruit spawn checks
    private bool newFruit; 
    private bool hasSpawned; //Checks if the fruit has spawned
    private int whichFruit; //Checks the enum of which fruit it is that has spawned

    //Input references
    [Header("Input Reference")]
    public InputActionReference trackingAction; //Dragging
    public InputActionReference clickingAction; //Tapping


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {

    }    
}
