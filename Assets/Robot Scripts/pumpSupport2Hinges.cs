using UnityEngine;

public class VerticalHingeCompensator : MonoBehaviour
{
    private HingeJoint hinge;
    public Transform horizontalPiece; // Trage aici "Piesa Orizontală" în Inspector

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        // Asigură-te că Use Spring este bifat în Inspector
        hinge.useSpring = true;
    }

    void FixedUpdate()
    {
        if (horizontalPiece != null)
        {
            // Citim rotația piesei orizontale pe axa de mișcare (ex: Z)
            float currentRotation = horizontalPiece.localEulerAngles.z;

            // Ajustăm rotația pentru a fi în intervalul -180, 180
            if (currentRotation > 180) currentRotation -= 360;

            // Setăm ținta arcului să fie EXACT OPUSUL rotației brațului
            JointSpring js = hinge.spring;
            js.targetPosition = -currentRotation; 
            hinge.spring = js;
        }
    }
}