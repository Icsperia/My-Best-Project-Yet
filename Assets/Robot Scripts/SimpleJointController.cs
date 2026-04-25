using UnityEngine;
using UnityEngine.InputSystem; // Obligatoriu pentru Unity 6

public class RobotIndepententControl : MonoBehaviour
{
    public float speed = 50f;
    private ArticulationBody joint;

    void Start()
    {
        joint = GetComponent<ArticulationBody>();
    
        var drive = joint.xDrive;
        drive.stiffness = 10000f;
        drive.damping = 100f;
        drive.forceLimit = 10000f;
        joint.xDrive = drive;
    }

    void Update()
    {
        float input = 0;




   

        if (input != 0)
        {
            var drive = joint.xDrive;
            float targetPos = drive.target + (input * speed * Time.deltaTime);
            drive.target = Mathf.Clamp(targetPos, drive.lowerLimit, drive.upperLimit);
            joint.xDrive = drive;
        }
    }
}