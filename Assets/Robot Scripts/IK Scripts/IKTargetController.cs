
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Robotics.UrdfImporter.Control;
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;



public class IKTargetController : MonoBehaviour
{
    public ArticulationBody baseRotative;

    [Header("Butoane de control")]
    public InputActionProperty leftAction;        // Buton stanga
    public InputActionProperty rightAction;       // Buton dreapta

    public InputActionProperty upAction;          // Buton sus
    public InputActionProperty downAction;        // Buton jos// Buton jos

    public InputActionProperty resetButton;

    public InputActionProperty joystick;
    [Header("Vitezele de deplasare și reset")]
    public float moveSpeed = 0.5f;
   public  float resetSpeed = 5f;
    private bool isResetting = false;
    [Header("Zona de deplasare efector final")]
    public Vector3 minBounds = new Vector3(-2.0f, 1.0f, -2.0f);
    public Vector3 maxBounds = new Vector3(2.0f, 3.0f, 2.0f);

    public Vector3 resetPosition = new Vector3(-0.13f, 2.44f, -0.72f);

    public float smoothTime ;



    void OnEnable()
    {
        if (resetButton != null)
            resetButton.action.Enable();
    }


    void OnDisable()
    {
        if (resetButton != null)
            resetButton.action.Disable();
    }


    void Update()
    {
          if (resetButton.action.WasPressedThisFrame() && !isResetting)
        {
            StartCoroutine(SmoothReset(resetPosition));
                        
  StartCoroutine(SmoothResetCouroutine());

        }


        if (!isResetting)
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


        Vector2 targetDirection = Vector2.zero;



        if (leftAction.action != null)
           targetDirection.x -= leftAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (rightAction.action != null)
           targetDirection.x += rightAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (upAction.action != null)
           targetDirection.y += upAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (downAction.action != null)
           targetDirection.y -= downAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;


            // Vector2 targetVelocity = targetDirection * moveSpeed;
            
            // currentSpeed = Vector2.SmoothDamp(
            //     currentSpeed,
            //     targetVelocity,
            //     ref velocityRef,
            //     smoothTime

            // );
            
            
            transform.Translate(targetDirection, Space.World);



            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            Mathf.Clamp(transform.position.z, minBounds.z, maxBounds.z)
        );
        }
  
    
}

IEnumerator SmoothReset(Vector3 targetPosition)
    {
            
            isResetting = true;
            while(Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position =Vector3.MoveTowards(transform.position, targetPosition, resetSpeed*Time.deltaTime);
         
            yield return null;
        }
             transform.position = targetPosition;
             isResetting  = false;
   

    }
  IEnumerator SmoothResetCouroutine()
    {
        
        isResetting = true;
        bool allZero = false;

        while (!allZero)
        {

            bool rDone = MoveTowardtoZero(baseRotative);

      
         allZero = rDone ;
         yield return new WaitForFixedUpdate();
    
        }
      isResetting = false;
    
    }
    bool MoveTowardtoZero(ArticulationBody source)
    {
             var drive = source.xDrive;
             drive.target  = Mathf.MoveTowards(drive.target, 0.0f, resetSpeed*Time.fixedDeltaTime);
             source.xDrive = drive;

             return Mathf.Abs(drive.target) < 0.001f;

    }

}