using UnityEngine;

public class CinematicConstraints : MonoBehaviour
{


    [Header("Segmente principale")]
    public ArticulationBody verticalArm;
    public ArticulationBody horizontalArm;
    public ArticulationBody upDownSegment;

    [Header("Segmente secundare")]
    public ArticulationBody pumpSupport1;
    public ArticulationBody vertical2;
    public ArticulationBody horizontalSegment1;
    public ArticulationBody vertical1;
    public ArticulationBody horizontalSegment2;

    // [Header("Offsets")]

    // public float offset;
    // public float offset1;
    // public float offset2;


    //public float pumpOffset;
    // public float leaningIntensity;
    // public float leaningIntensityPump ;





    void FixedUpdate()
    {
        float anglehorizontalArm = horizontalArm.jointPosition[0] * Mathf.Rad2Deg;
        float anglehorizontalSegment1 = horizontalSegment1.jointPosition[0] * Mathf.Rad2Deg;
        float anglePump = pumpSupport1.jointPosition[0] * Mathf.Rad2Deg;
        float angleVertica2 = verticalArm.jointPosition[0] * Mathf.Rad2Deg;
        float angleUpDown = upDownSegment.jointPosition[0] * Mathf.Rad2Deg;
        float angleUpDown1 = upDownSegment.jointPosition[0];





        float angleV = verticalArm.jointPosition[0];
        float angleVertical = angleV * Mathf.Rad2Deg;

        // Debug.Log($"Sursa: { verticalArm} | Radiani: { angleV} | Grade: {angleVertical }");

        float upDownAngle = upDownSegment.jointPosition[0] * Mathf.Rad2Deg;
        float verticalArmAngle = verticalArm.jointPosition[0] * Mathf.Rad2Deg;


        //////////////////////////////
        var driveH = horizontalArm.xDrive;
        driveH.target = -(angleUpDown + angleVertical);
        horizontalArm.xDrive = driveH;


        /////////////////////////////
        var driveP = pumpSupport1.xDrive;
        driveP.target = (2 * anglehorizontalSegment1) - anglehorizontalArm - angleVertica2;
        pumpSupport1.xDrive = driveP;


        ////////////////////////////
        var vertical1Drive = vertical1.xDrive;
        vertical1Drive.target = -(upDownAngle + verticalArmAngle);
        vertical1.xDrive = vertical1Drive;

        //////////////////////////////
        CompensationPositive(verticalArm, vertical2);

        //////////////////////////////////
        CompensationPositive(verticalArm, horizontalSegment2);

        /////////////////////////////////
        CompensationNegative(upDownSegment, horizontalSegment1);


    }
    void CompensationNegative(ArticulationBody source, ArticulationBody target)
    {
        float angle = source.jointPosition[0] * Mathf.Rad2Deg;

        var drive = target.xDrive;

        drive.target = -angle;

        target.xDrive = drive;


    }


    void CompensationPositive(ArticulationBody source, ArticulationBody target)
    {
        float angle = source.jointPosition[0] * Mathf.Rad2Deg;

        var drive = target.xDrive;

        drive.target = angle;

        target.xDrive = drive;


    }
}


