using UnityEngine;

public class IKDebug : MonoBehaviour
{
    public ArticulationBody baseRotative;
    public ArticulationBody shoulder;
    public ArticulationBody elbow;
    public Transform target;

    void FixedUpdate()
    {
        if (target == null) { Debug.Log("TARGET E NULL!"); return; }

        Vector3 relativePos = transform.InverseTransformPoint(target.position);
        float x = relativePos.x ;
        float y = relativePos.z ;
        float z = relativePos.y ;

        float r = Mathf.Sqrt(x * x + y * y);
        float h = z - 84f;
        float d = Mathf.Sqrt(r * r + h * h);

        Debug.Log($"=== IK DEBUG ===");
        Debug.Log($"Target relativ: x={x:F1} y={y:F1} z={z:F1}");
        Debug.Log($"r={r:F1} h={h:F1} d={d:F1} | Raza max={L2+L3} Raza min={Mathf.Abs(L2-L3)}");
        Debug.Log($"BASE stiffness={baseRotative.xDrive.stiffness} damping={baseRotative.xDrive.damping}");
        Debug.Log($"SHOULDER stiffness={shoulder.xDrive.stiffness}");
        Debug.Log($"ELBOW stiffness={elbow.xDrive.stiffness}");

        // Test direct: forțează un unghi fix de 30 grade
        var d1 = baseRotative.xDrive;
        d1.target = 30f;
        baseRotative.xDrive = d1;
    }

float L2 = 0.128f; // în metri, nu mm
float L3 = 0.138f;
}