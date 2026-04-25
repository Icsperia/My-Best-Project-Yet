using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Robotics.UrdfImporter.Control;
using UnityEngine.AI;
using UnityEngine.Rendering;





public class ToggleXRControls : MonoBehaviour
{
    public ArticulationBody baseRotative;
    public InputActionProperty moveAction;

    public ArticulationBody verticalArm;

    public ArticulationBody upDownSegment;

    public InputActionReference bButton;

    public InputActionReference aButton;

    public InputActionReference mainTrigger;

    public InputActionReference secondaryTrigger;

    public ArticulationBody pumpSupport2;
    void OnEnable()
    {
        if (moveAction.action != null)
            moveAction.action.Enable();

        if (bButton.action != null)
            bButton.action.Enable();

        if (aButton.action != null)
            aButton.action.Enable();

        if (mainTrigger.action != null)
            mainTrigger.action.Enable();

        if (secondaryTrigger.action != null)
            secondaryTrigger.action.Enable();
    }

    void OnDisable()
    {
        if (bButton.action != null)
            bButton.action.Disable();
        if (aButton.action != null)
            aButton.action.Disable();
        if (mainTrigger.action != null)
            mainTrigger.action.Disable();

        if (secondaryTrigger.action != null)
            secondaryTrigger.action.Disable();
    }


    void Update()
    {
        if (baseRotative == null || moveAction.action == null || bButton == null || bButton.action == null) return;
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        float upDownAngle = upDownSegment.jointPosition[0]*Mathf.Rad2Deg;
        float veritcalAngle = verticalArm.jointPosition[0]*Mathf.Rad2Deg;
        float horizontal = input.x;
       // float vertical = input.y;


        JointControl brControl = baseRotative.GetComponent<JointControl>();
        JointControl vaControl = verticalArm.GetComponent<JointControl>();
        JointControl uDControl = upDownSegment.GetComponent<JointControl>();

        JointControl pSControl = pumpSupport2.GetComponent<JointControl>();
        if (brControl != null)
        {
            if (horizontal > 0.1f) brControl.direction = RotationDirection.Negative;
            else if (horizontal < -0.1f) brControl.direction = RotationDirection.Positive;
            else brControl.direction = RotationDirection.None;
        }

       if (vaControl != null && uDControl != null)

        if (mainTrigger.action.IsPressed())
        {
            vaControl.direction = RotationDirection.Negative;
            uDControl.direction = RotationDirection.Positive;
        }
        else if (secondaryTrigger.action.IsPressed())
        {
            vaControl.direction = RotationDirection.Positive;
            uDControl.direction = RotationDirection.Negative;
        }

        else if (bButton.action.IsPressed())
        {
            uDControl.direction = RotationDirection.Positive;
           
            if (upDownAngle > 7f) vaControl.direction = RotationDirection.Negative;
            else vaControl.direction = RotationDirection.Positive;
        }
        else if (aButton.action.IsPressed())
        {
            uDControl.direction = RotationDirection.Negative;
            vaControl.direction = RotationDirection.None; 
        }
   
        else
        {
            vaControl.direction = RotationDirection.None;
            uDControl.direction = RotationDirection.None;
        }
    }

        // if (pSControl != null)
        // {
        //     if (mainTrigger.action.IsPressed())
        //     {
        //         pSControl.direction = RotationDirection.Positive;
        //     }
        //     else if (secondaryTrigger.action.IsPressed())
        //     {
        //         pSControl.direction = RotationDirection.Negative;
        //     }else pSControl.direction = RotationDirection.None;
        // }

        // else if (!bButton.action.IsPressed())
        // {
        //     Debug.Log("Deapasat");
        // }
        // else
        // {
        //     Debug.Log("NImic");
        // }




    }
