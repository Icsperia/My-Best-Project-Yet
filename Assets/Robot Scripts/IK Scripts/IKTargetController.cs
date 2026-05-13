
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Robotics.UrdfImporter.Control;

public class IKTargetController : MonoBehaviour
{
    public ArticulationBody baseRotative;
    
    [Header("Input Actions")]
    public InputActionProperty leftAction;        // Buton stanga
    public InputActionProperty rightAction;       // Buton dreapta
    // public InputActionProperty forwardAction;     // Buton fata
    // public InputActionProperty backAction;        // Buton spate
    public InputActionProperty upAction;          // Buton sus
    public InputActionProperty downAction;        // Buton jos// Buton jos

    public InputActionProperty resetButton;

    public InputActionProperty joystick;
    [Header("Speed")]
    public float moveSpeed = 0.5f;

    [Header("Boundaries")]
    public Vector3 minBounds = new Vector3(-2.0f, 1.0f, -2.0f);
    public Vector3 maxBounds = new Vector3( 2.0f, 3.0f,  2.0f);

    public Vector3 resetPosition = new Vector3(-0.13f,2.44f,-0.72f);


    void OnEnable()
    {
    if(resetButton!=null)
    resetButton.action.Enable();        


   }


    void Update()
    {
       
        JointControl brControl = baseRotative.GetComponent<JointControl>();
        Vector2 input = joystick.action.ReadValue<Vector2>();

        float horizontal = input.x;
        
              if (brControl != null)
        {
            if (horizontal > 0.1f) brControl.direction = RotationDirection.Negative;
            else if (horizontal < -0.1f) brControl.direction = RotationDirection.Positive;
            else brControl.direction = RotationDirection.None;
        }
        
        
        Vector3 move = Vector3.zero;




        if (leftAction.action != null)
            move.x -= leftAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (rightAction.action != null)
            move.x += rightAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        // if (forwardAction.action != null)
        //     move.z += forwardAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        // if (backAction.action != null)
        //     move.z -= backAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (upAction.action != null)
            move.y += upAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (downAction.action != null)
            move.y -= downAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        
        if(resetButton.action.IsPressed())
            transform.position = resetPosition;
        transform.Translate(move, Space.World);


     


        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            Mathf.Clamp(transform.position.z, minBounds.z, maxBounds.z)
        );
    }
}