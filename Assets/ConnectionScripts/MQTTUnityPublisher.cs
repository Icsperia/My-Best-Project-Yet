using UnityEngine;
using MQTTnet;
using MQTTnet.Client;
using System.Threading.Tasks;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using System;
using Mono.Cecil.Cil;
using System.Collections;




//To do
public class MQTTUnityPublisher : MonoBehaviour
{

public ArticulationBody rotativeBase;
public IMqttClient  MqttClient;

public float angleInterval;

public float delay ;
private float publishTime;


public float lastAngle;
async void Start()
    {
       
        await Main();
    }

async void Update()
    {
    if (MqttClient != null && MqttClient.IsConnected  && Time.time >= publishTime)
        {
         publishTime = Time.time+delay;
              await SendData();  
        }


    }

     async Task Main()
    {

        var mqttFactory = new MqttFactory();
        MqttClient = mqttFactory.CreateMqttClient();
        var options = new MqttClientOptionsBuilder()
        .WithTcpServer("10.206.197.112",1883)
        .WithClientId("PublisherUnity")
        .Build();
        await MqttClient.ConnectAsync(options);
        Debug.Log("Publisher Connected");
      
     


    }

    async Task SendData()
    {
        float rotativeBaseAngle = rotativeBase.jointPosition[0];
        float neg = -rotativeBaseAngle*750.0f;

        if (Math.Abs(neg - lastAngle) > angleInterval)
        {
                string data = neg.ToString("F2");
        var message = new MqttApplicationMessageBuilder()
        .WithTopic("rotativeBase/topic")
        .WithPayload(data)
        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce)
        .Build();
         await MqttClient.PublishAsync(message);
         lastAngle = neg;
         Debug.Log("Message Published"+neg);
        }
    
     
  


    }

async void Disconnect()
    {
                await MqttClient.DisconnectAsync();
                Debug.Log("Disconnected");
    }

}
