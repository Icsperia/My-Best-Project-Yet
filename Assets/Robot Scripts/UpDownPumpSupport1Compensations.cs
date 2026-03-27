using UnityEngine;

public class ActiveVerticalStabilizer : MonoBehaviour
{
    public ArticulationBody armBase; // Brațul robotului
    private HingeJoint[] hinges;

    [Header("Control Forță")]
    public float power = 20000f; 
    public float damping = 100f;

    void Start()
    {
        hinges = GetComponents<HingeJoint>();
        
        foreach (var h in hinges)
        {
            // Activăm arcul din cod
            h.useSpring = true;
            
            JointSpring js = h.spring;
            js.spring = power; 
            js.damper = damping;
            h.spring = js;
        }
    }

    void FixedUpdate()
{
    if (armBase != null && hinges != null)
    {
        // 1. Calculăm unghiul invers al brațului
        float targetAngle = -armBase.jointPosition[0] * Mathf.Rad2Deg;

        foreach (var h in hinges)
        {
            JointSpring js = h.spring;

            // VALORI PENTRU STABILITATE TOTALĂ
            js.spring = 100000f;  // Forță imensă ca să nu se lase sub greutate
            js.damper = 10000f;   // Amortizare uriașă ca să nu balanseze deloc
            js.targetPosition = targetAngle;

            h.spring = js;
        }
    }
}
}