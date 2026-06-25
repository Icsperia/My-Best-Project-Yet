using UnityEngine;
using TMPro;
using System.IO;
using System.Data.Common;
public class JSONScript : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI tut;

    string jsonPath = "Assets/Robot Scripts/MiniTutorial/tutorial.json";


    void Start()
    {
        string readJson = File.ReadAllText(jsonPath);


        Tutorials basicTutorials = JsonUtility.FromJson<Tutorials>(readJson);
        string allTutorials = " ";
        foreach (var data in basicTutorials.BasicTutorial)
        {
            allTutorials += $"Move: {data.MoveAround[0]} \n";
            allTutorials += $"Move: {data.MoveAround[1]} \n";
            allTutorials += $"Move: {data.MoveAround[2]} \n";
            allTutorials += $"Move: {data.MoveAround[3]} \n";

            allTutorials += $"MoveRobot: {data.MoveRobot[0]} \n";
            allTutorials += $"MoveRobot: {data.MoveRobot[1]} \n";
            allTutorials += $"MoveRobot: {data.MoveRobot[2]} \n";
            allTutorials += $"MoveRobot: {data.MoveRobot[3]} \n";
            allTutorials += $"MoveRobot: {data.MoveRobot[4]} \n";
            allTutorials += $"MoveRobot: {data.MoveRobot[5]} \n";
            allTutorials += $"MoveRobot: {data.MoveRobot[6]} \n";
            allTutorials += $"MoveRobot: {data.MoveRobot[7]} \n";
            allTutorials += $"MoveRobot: {data.MoveRobot[8]} \n";

        }

        tut.text = allTutorials;
    }
    [System.Serializable]
    public class Tutorial
    {
        public string[] MoveAround;
        public string[] MoveRobot;
    }

    public class Tutorials
    {
        public Tutorial[] BasicTutorial;

    }
}
