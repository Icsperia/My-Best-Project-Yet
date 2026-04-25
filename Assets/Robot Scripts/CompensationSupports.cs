using UnityEngine;

public class CompensationSupports : MonoBehaviour
{
   

    public ArticulationBody vertical1;
    public ArticulationBody upDownSegment;

    public ArticulationBody verticalArm;

    public ArticulationBody vertical2;

    public ArticulationBody horizontalSegment2;

    public ArticulationBody horizontalArm;

    public ArticulationBody horizontalSegment1;
    public  ArticulationBody pumpSupport1;
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
       float pumpSupportAngle = pumpSupport1.jointPosition[0]*Mathf.Rad2Deg;
       float horizontalSegemnt1Angle = horizontalSegment1.jointPosition[0]*Mathf.Rad2Deg;
       float horizontalSegemnt2Angle = horizontalSegment2.jointPosition[0]*Mathf.Rad2Deg;
       var vertical1Drive= vertical1.xDrive;    
       vertical1Drive.target = -(upDownAngle+verticalArmAngle);
       vertical1.xDrive = vertical1Drive;

 
    
    //     if (upDownSegment != null && verticalArm != null && horizontalArm != null)
    // {
       

    //     var driveH = horizontalSegment1.xDrive;

    //     float totalBaseAngle = -(upDownAngle + verticalArmAngle);

   
    //    // if (angleVertical > 0)
    //    // {
            
    //       // driveH.target = totalBaseAngle   -angleVertical ;
    //    // }
    //    // else
    //    // {
    //        driveH.target = totalBaseAngle ;
    //    // }

    //    horizontalSegment1.xDrive = driveH;
    // }

    //////////////////////////////
      
   CompensationPositive(verticalArm, vertical2,0);
    //////////////////////////////////
   CompensationPositive(verticalArm, horizontalSegment2, 0f);
   
// var horizontalSegment2Drive = horizontalSegment2.xDrive;
// horizontalSegment2Drive.target = verticalArmAngle-pumpSupportAngle-horizontalArmAngle;
// horizontalSegment2.xDrive =horizontalSegment2Drive;
  CompensationNegative(upDownSegment,horizontalSegment1);

// var horizontalSegment1Drive = horizontalSegment1.xDrive;
// horizontalSegment1Drive.target = upDownAngle+pumpSupportAngle;
// horizontalSegment1.xDrive =horizontalSegment1Drive;

//  Debug.Log("verticalArmAngle"+verticalArmAngle);
//  Debug.Log("pumpSupportAngle"+pumpSupportAngle);
//  Debug.Log("horizontalArmAngle"+horizontalArmAngle);
//  Debug.Log("horizontalSegemnt1Angle"+horizontalSegemnt1Angle);
//  Debug.Log("horizontalSegemnt2Angle"+horizontalSegemnt2Angle);

    }

        void CompensationNegative(ArticulationBody source, ArticulationBody target)
    {
        float rad= source.jointPosition[0];
        float angle = rad*Mathf.Rad2Deg;
        // Debug.Log($"Sursa: {source.name} | Radiani: {rad} | Grade: {angle}");
        var drive = target.xDrive;

        drive.target = -angle;

        target.xDrive = drive;


    }


    void CompensationPositive(ArticulationBody source, ArticulationBody target, float offset)
    {
        float angle =  source.jointPosition[0]*Mathf.Rad2Deg;

        var drive = target.xDrive;

        drive.target = angle+offset;

        target.xDrive = drive;


    }
    
}


