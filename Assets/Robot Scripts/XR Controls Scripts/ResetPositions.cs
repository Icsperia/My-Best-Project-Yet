using System;
using System.Data.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ResetPositions : MonoBehaviour
{
    public ArticulationBody verticalArm;
    //public ArticulationBody horizontalArm;
    public ArticulationBody rotativeBase;

    public ArticulationBody upDownSegment;

    public ArticulationBody noozle;

    public InputActionReference joystickButton;

    void Start()
    {

    }

    void OnEnable()
    {
        if (joystickButton != null)
            joystickButton.action.Enable();
    }

    void Update()
    {



    }


    void FixedUpdate()
    {

        // float verticalArmAngle = verticalArm.jointPosition[0] * Mathf.Rad2Deg;
        // float rotativeBaseAngle = rotativeBase.jointPosition[0] * Mathf.Rad2Deg;
        // float upDownAngle = upDownSegment.jointPosition[0] * Mathf.Rad2Deg;
        // float noozleAngle = noozle.jointPosition[0] * Mathf.Rad2Deg;

        if (joystickButton.action.IsPressed())
        {

            resetPosition(verticalArm);
            resetPosition(rotativeBase);
            resetPosition(noozle);
            resetPosition(upDownSegment);
            Debug.Log("IsPressed");



        }


        // }


        // void resetAngles(ArticulationBody body, float value)
        // {
        //     float angle = body.jointPosition[0] * Mathf.Rad2Deg;
        //     var xDrive = body.xDrive;

        //     if (Mathf.Abs(value) < 0.5f)
        //     {
        //         xDrive.target = 0;
        //     }
        //     else
        //     {



        //         float speed = Mathf.Abs(angle / value);
        //         if (angle > 0f) xDrive.target = angle - speed;
        //         else
        //             xDrive.target = angle + speed;

        //     }

        //     body.xDrive = xDrive;

        // }
    }

    void resetPosition(ArticulationBody source)
    {
        var drive = source.xDrive;
        drive.target = 0.0f;
        source.xDrive = drive;

    }
}