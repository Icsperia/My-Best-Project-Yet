using System;
using UnityEngine;
using UnityEngine.AI;

public class ResetPositions : MonoBehaviour
{
    public ArticulationBody verticalArm;
    //public ArticulationBody horizontalArm;
    public ArticulationBody rotativeBase;

    public ArticulationBody upDownSegment;

    public ArticulationBody noozle;
   [SerializeField]
    public float   resetSpeed;

     
     public bool isReseting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetKeyDown(KeyCode.Space))
        {
            isReseting =true;
             Debug.Log("The robot position was reseted");
        }
    }

    void FixedUpdate()
    {
        // float verticalArmAngle = verticalArm.jointPosition[0]*Mathf.Rad2Deg;
        // float rotativeBaseAngle = rotativeBase.jointPosition[0]*Mathf.Rad2Deg;
        // float upDownAngle = upDownSegment.jointPosition[0]*Mathf.Rad2Deg;
        // float noozleAngle = noozle.jointPosition[0]*Mathf.Rad2Deg;

        // var verArm = verticalArm.xDrive;
        // var rotBase = rotativeBase.xDrive;
        // var uDSegment = upDownSegment.xDrive;
        // var noozleVar = noozle.xDrive;

        if (Input.GetKeyDown(KeyCode.Space))
        {
          bool verArm = SmoothReset(verticalArm);
           bool rotBase = SmoothReset(rotativeBase);
           bool uDSegment = SmoothReset(upDownSegment);
           bool noozleVar = SmoothReset(noozle);


if(verArm && rotBase && uDSegment && noozleVar)
      
      isReseting  = false;
           Debug.Log("Reset Complete");
        }

    }


bool SmoothReset(ArticulationBody body)
    {
        
        var drive = body.xDrive;
        float currentTarget=drive.target;

        if (Mathf.Abs(currentTarget) < 0.1f)
        {
            drive.target=0;
            body.xDrive=drive;
     return true;
        }
        drive.target=Mathf.MoveTowards(currentTarget, 0f, resetSpeed*Time.fixedDeltaTime);
        body.xDrive=drive;
        return false;
   

    }
} 
