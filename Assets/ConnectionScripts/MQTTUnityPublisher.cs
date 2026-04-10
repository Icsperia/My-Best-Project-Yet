using UnityEngine;
using MQTTnet;
using MQTTnet.Client;
using System.Threading.Tasks;
using System;

public class MQTTUnityPublisher : MonoBehaviour
{
    public ArticulationBody rotativeBase;
    public ArticulationBody verticalArm;
    public ArticulationBody upDownSegment;

    public string brokerIp = "10.206.197.112";
    public int brokerPort = 1883;

    private IMqttClient mqttClient;
    
    [Header("Setari Miscare")]
    public float sensitivity = 500f; // Ajusteaza viteza de raspuns
    public float publishDelay = 0.05f; // 20 mesaje pe secunda
    public int deadzone = 1; // Ignora miscari ultra-mici

    private float lastRawX, lastRawY, lastRawZ;
    private float nextPublishTime;

    async void Start()
    {
        await ConnectToBroker();
       
        lastRawX = rotativeBase.jointPosition[0];
        lastRawY = verticalArm.jointPosition[0];
        lastRawZ = upDownSegment.jointPosition[0];
    }

    async void Update()
    {
        if (mqttClient != null && mqttClient.IsConnected && Time.time >= nextPublishTime)
        {
            nextPublishTime = Time.time + publishDelay;
            await SendRelativeData();
        }
    }

    async Task ConnectToBroker()
    {
        var factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(brokerIp, brokerPort)
            .WithClientId("UnityPublisher_Relative")
            .Build();

        await mqttClient.ConnectAsync(options);
        Debug.Log("MQTT: Conectat la Broker pentru miscari RELATIVE");
    }

    async Task SendRelativeData()
    {
        
        float curX = rotativeBase.jointPosition[0];
        float curY = verticalArm.jointPosition[0];
        float curZ = upDownSegment.jointPosition[0];

      
        int deltaX = Mathf.RoundToInt((curX - lastRawX) * sensitivity);
        int deltaY = Mathf.RoundToInt((curY - lastRawY) * sensitivity);
        int deltaZ = Mathf.RoundToInt(-(curZ - lastRawZ) * sensitivity);

    
        if (Mathf.Abs(deltaX) >= deadzone) 
        {
            await PublishInt("rotativeBase/topic", deltaX);
            lastRawX = curX;
        }

        if (Mathf.Abs(deltaY) >= deadzone)
        {
            await PublishInt("verticalArm/topic", deltaY);
            lastRawY = curY;
        }

        if (Mathf.Abs(deltaZ) >= deadzone)
        {
            await PublishInt("upDownSegment/topic", deltaZ);
            lastRawZ = curZ;
        }
    }

    async Task PublishInt(string topic, int value)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(value.ToString())
            .Build();
        await mqttClient.PublishAsync(message);
    }
}