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
        var kb = Keyboard.current;

        if (kb == null) return;

        // Folosim taste care NU sunt folosite de XR Interaction Toolkit
        if (kb.uKey.isPressed) input = 1f; // Tasta U pentru un sens
        else if (kb.jKey.isPressed) input = -1f; // Tasta J pentru celălalt sens

        if (input != 0)
        {
            var drive = joint.xDrive;
            float targetPos = drive.target + (input * speed * Time.deltaTime);
            drive.target = Mathf.Clamp(targetPos, drive.lowerLimit, drive.upperLimit);
            joint.xDrive = drive;
        }
    }
}