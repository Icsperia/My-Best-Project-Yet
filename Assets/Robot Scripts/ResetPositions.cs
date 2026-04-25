using System;
using System.Data.Common;
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
    
    
    public ArticulationBody noozleSupport;

      [SerializeField] public float   resetSpeed;

    public InputActionReference joystickButton;
     
     public bool isReseting;

     public bool    joystickButtonPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnEnable()
    {
        if(joystickButton != null)
        joystickButton.action.Enable();
    }
    // Update is called once per frame
    void Update()
    {

       joystickButtonPressed = joystickButton.action.IsPressed();
 
    }

    
    
    
    void FixedUpdate()
    {
       
    
    
        float verticalArmAngle = verticalArm.jointPosition[0]*Mathf.Rad2Deg;
        float rotativeBaseAngle = rotativeBase.jointPosition[0]*Mathf.Rad2Deg;
        float upDownAngle = upDownSegment.jointPosition[0]*Mathf.Rad2Deg;
        float noozleAngle = noozle.jointPosition[0]*Mathf.Rad2Deg;



          if(joystickButtonPressed)
        {
       
           
          
           
            resetAngles(rotativeBase, 4.0f );
            resetAngles(upDownSegment, 4.0f );
            resetAngles(verticalArm, 4.0f );
            resetAngles(noozle, 4.0f );
            resetAngles(noozleSupport, 4.0f );

   
       
 
        }
       
  
    }
    

    void resetAngles(ArticulationBody body, float value)
    {
        float angle = body.jointPosition[0]*Mathf.Rad2Deg;
        var xDrive= body.xDrive;  
        
        if(Mathf.Abs(value)< 0.5f)
        {
            xDrive.target = 0;
        }
        else
        {
            
        
        
        float speed = Mathf.Abs(angle/value);
        if (angle > 0f) xDrive.target = angle - speed;
        else 
        xDrive.target = angle +speed;
  
        }
       
        body.xDrive = xDrive;
        
         }
    }

