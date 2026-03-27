using UnityEngine;

public class CompensationSupports : MonoBehaviour
{
    public float offset = 0f;

    public ArticulationBody vertical1;
    public ArticulationBody upDownSegment;

    public ArticulationBody verticalArm;

    public ArticulationBody vertical2;

    public ArticulationBody horizontalSegment2;

    public ArticulationBody horizontalArm;

    public ArticulationBody horizontalSegment1;
    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       float upDownAngle =  upDownSegment.jointPosition[0]*Mathf.Rad2Deg;
       float verticalArmAngle =  verticalArm.jointPosition[0]*Mathf.Rad2Deg;
       float vert1Angle =  vertical1.jointPosition[0]*Mathf.Rad2Deg;
        float horizontalArmAngle =  horizontalArm.jointPosition[0]*Mathf.Rad2Deg;

    //    Debug.Log("Vert1 angle"+vert1Angle);
    //    Debug.Log("UpDown"+upDownAngle);


       var vertical1Drive= vertical1.xDrive;

       
       vertical1Drive.target = -(upDownAngle+verticalArmAngle);

       vertical1.xDrive = vertical1Drive;

 
    //////////////////////////////
        // float vertical2Angle = vertical2.jointPosition[0]*Mathf.Rad2Deg;

        // Debug.Log("vertical2Angle"+vertical2Angle);
   
        // var vertical2Drive = vertical2.xDrive;
        // vertical2Drive.target = verticalArmAngle;

        // vertical2.xDrive = vertical2Drive;
        CompensationPositive(verticalArm, vertical2);
    //////////////////////////////////
//     float horizontalSegment2Angle = horizontalSegment2.jointPosition[0]*Mathf.Rad2Deg;
//  float vertical2Angle = horizontalSegment1.jointPosition[0]*Mathf.Rad2Deg;
// Debug.Log("HorSeg2Angle"+horizontalSegment2Angle);
// Debug.Log("VericalArmAngle"+verticalArmAngle);
//         //Debug.Log("vertical2Angle"+vertical2Angle);
   
//         var horizontalSegment2Drive = horizontalSegment2.xDrive;
//     horizontalSegment2Drive.target = verticalArmAngle;

//        horizontalSegment2.xDrive = horizontalSegment2Drive;
//    CompensationNegative(horizontalArm, horizontalSegment2);
   CompensationPositive(verticalArm, horizontalSegment2);
   CompensationNegative(upDownSegment,horizontalSegment1 );
   

    }

        void CompensationNegative(ArticulationBody source, ArticulationBody target)
    {
        float angle =  source.jointPosition[0]*Mathf.Rad2Deg;

        var drive = target.xDrive;

        drive.target = -angle;

        target.xDrive = drive;


    }


    void CompensationPositive(ArticulationBody source, ArticulationBody target)
    {
        float angle =  source.jointPosition[0]*Mathf.Rad2Deg;

        var drive = target.xDrive;

        drive.target = angle;

        target.xDrive = drive;


    }
    
}


