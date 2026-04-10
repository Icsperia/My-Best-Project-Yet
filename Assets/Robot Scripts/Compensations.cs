using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;

public class Compensations : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public ArticulationBody horizontalArm;
   public ArticulationBody upDownSegment;
   public ArticulationBody pumpSupport1;
   
   public ArticulationBody verticalArm;

   public ArticulationBody horizontalSegment1;
   
   public float offset1 = 0f;
    public float offset2 = 0f;
    public float offset = 0f;

    public float pumpOffset = 0f;
    public float leaningIntensity = -1.1f;
    public float leaningIntensityPump = -1.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

 void FixedUpdate()
     {
        float anglehorizontalArm = horizontalArm .jointPosition[0] * Mathf.Rad2Deg;
        float anglehorizontalSegment1 = horizontalSegment1.jointPosition[0] * Mathf.Rad2Deg;
        float anglePump = pumpSupport1.jointPosition[0]*Mathf.Rad2Deg;
        float angleVertica2 = verticalArm.jointPosition[0] * Mathf.Rad2Deg;
        float angleUpDown = upDownSegment.jointPosition[0] * Mathf.Rad2Deg;
         float angleUpDown1= upDownSegment.jointPosition[0];
      
        if (upDownSegment != null && verticalArm != null && horizontalArm != null)
    {
       
  
        float angleV =   verticalArm.jointPosition[0];
        float angleVertical = angleV* Mathf.Rad2Deg;
        var driveH = horizontalArm.xDrive;
        // Debug.Log($"Sursa: { verticalArm} | Radiani: { angleV} | Grade: {angleVertical }");
        float totalBaseAngle = -(angleUpDown + angleVertical);

   
       // if (angleVertical > 0)
       // {
            
          // driveH.target = totalBaseAngle   -angleVertical ;
       // }
       // else
       // {
           driveH.target = totalBaseAngle ;
       // }

        horizontalArm.xDrive = driveH;
    }


    

        var driveP = pumpSupport1.xDrive;
        float sumaRotatiiParinti = anglehorizontalArm  + anglehorizontalSegment1;
   

        driveP.target =(2*anglehorizontalSegment1)-anglehorizontalArm-angleVertica2;

         pumpSupport1.xDrive = driveP;
 
        
   
     }
        void CompensationNegative(ArticulationBody source, ArticulationBody target, float offset)
    {
        float angle =  source.jointPosition[0]*Mathf.Rad2Deg;

        var drive = target.xDrive;

        drive.target = -angle+offset;

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


//     if ( horizontalSegment1 != null && horizontalArm != null)
    // {
      
    //     float anglehorizontalArm = horizontalArm .jointPosition[0] * Mathf.Rad2Deg;
    //     float anglehorizontalSegment1 = verticalArm.jointPosition[0] * Mathf.Rad2Deg;
        
    //     var driveH = horizontalArm.xDrive;

    //     float totalBaseAngle = -(anglehorizontalArm+ anglehorizontalSegment1);

    //     // 3. Aplicăm logica de prag (If) pe baza mișcării VerticalArm
    //     if (anglehorizontalSegment1 > pumpOffset)
    //     {
    //         float diff = -anglehorizontalSegment1 - pumpOffset;
    //         driveH.target = totalBaseAngle + offset1 + (diff * leaningIntensityPump);
    //     }
    //     else
    //     {
    //         driveH.target = totalBaseAngle + offset1;
    //     }

    //     horizontalArm.xDrive = driveH;
    // }
//    if  ( horizontalSegment1 != null && horizontalArm != null)
   //{
        // Citim toate unghiurile părinților
       