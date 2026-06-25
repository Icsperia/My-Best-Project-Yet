using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;
using UnityEngine.Rendering;



public class StepByStep: MonoBehaviour
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

    public float stepSize = 5f;


    private bool wasLeft = false;
    private bool wasRight = false;

    private float targetBaseRotative;
    private float targetVerticalArm;
    private float targetUpDownSegment;

    private float velBase;
    private float velVertical;
    private float velUpDown;

    public float smoothTime = 0.3f;
    void Start()
    {
        if (baseRotative != null) targetBaseRotative = baseRotative.xDrive.target;
        if (verticalArm != null) targetVerticalArm = verticalArm.xDrive.target;
        if (upDownSegment != null) targetUpDownSegment = upDownSegment.xDrive.target;
    }
    
    
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

        float horizontal = input.x;
    
        bool isRight = horizontal >0.5f;
        bool isLeft = horizontal < -0.5f;


    if (isRight && !wasRight) targetBaseRotative -= stepSize;
        if (isLeft && !wasLeft) targetBaseRotative += stepSize; 


        wasRight = isRight;
        wasLeft = isLeft;

        if (mainTrigger.action.WasPressedThisFrame()) targetVerticalArm -= stepSize;
        if (secondaryTrigger.action.WasPressedThisFrame()) targetVerticalArm += stepSize;

 
        if (bButton.action.WasPressedThisFrame()) targetUpDownSegment -= stepSize;
        if (aButton.action.WasPressedThisFrame()) targetUpDownSegment += stepSize;

        ApplySmoothMovement(baseRotative,  targetBaseRotative, ref velBase, smoothTime);
        ApplySmoothMovement(verticalArm, targetVerticalArm, ref velVertical, smoothTime);
        ApplySmoothMovement(upDownSegment,  targetUpDownSegment, ref velUpDown, smoothTime);

    }


void ApplySmoothMovement(ArticulationBody joint,  float targetValue, ref float currentVelocity, float smoothTime)
    {
    
        var drive = joint.xDrive;

        drive.target = Mathf.SmoothDamp(drive.target, targetValue, ref currentVelocity, smoothTime);
        
        joint.xDrive = drive;
    }




}