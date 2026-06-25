
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetPositions : MonoBehaviour
{
    public ArticulationBody verticalArm;
    //public ArticulationBody horizontalArm;
    public ArticulationBody rotativeBase;

    public ArticulationBody upDownSegment;

    public ArticulationBody noozle;

    public InputActionReference joystickButton;

    public float resetSpeed = 50f;
    private bool isResetting = false;
    void OnEnable()
    {
        if (joystickButton != null)
            joystickButton.action.Enable();
    }

    void OnDisable()
    {
        if (joystickButton != null)
            joystickButton.action.Disable();
    }

    void FixedUpdate()
    {

        if (joystickButton.action.WasPressedThisFrame() && !isResetting)
        {
            
  StartCoroutine(SmoothResetCouroutine());
 
        }

    }

    void resetPosition(ArticulationBody source)
    {
        var drive = source.xDrive;
        drive.target = 0.0f;
        source.xDrive = drive;

    }
   
    IEnumerator SmoothResetCouroutine()
    {
        
        isResetting = true;
        bool allZero = false;

        while (!allZero)
        {
            bool vDone = MoveTowardtoZero(verticalArm);
            bool rDone = MoveTowardtoZero(rotativeBase);
            bool nDone = MoveTowardtoZero(noozle);
            bool uDone = MoveTowardtoZero(upDownSegment);
      
         allZero = rDone && vDone &&  nDone && uDone;
         yield return new WaitForFixedUpdate();
    
        }
      isResetting = false;
    
    }
    bool MoveTowardtoZero(ArticulationBody source)
    {
             var drive = source.xDrive;
             drive.target  = Mathf.MoveTowards(drive.target, 0.0f, resetSpeed*Time.deltaTime);
             source.xDrive = drive;

             return Mathf.Abs(drive.target) < 0.001f;

    }

}