import socket
import time
import gc
from umqttsimple import MQTTClient



#MQTT_SERVER = '192.168.29.61'
#CLIENT_ID = 'MaxArm'
TOPIC_PUBLISH = 'robot/coodonate'
#MQTT_PORT = 1883
#unityAddPort= ("192.168.137.1", 5005)
# bufferSize = 1024
# udpEsp32SocketClient = socket.socket(
# family= socket.AF_INET,
# type = socket.SOCK_DGRAM)

class MQTTEsp32:
       
        def identificationInfo(self,CLIENT_ID, MQTT_SERVER, MQTT_PORT):
                self.client = MQTTClient(CLIENT_ID, MQTT_SERVER, MQTT_PORT)
        
        def connectToBroker(self):
                try:
               
                        print('Connect to Mqtt broker...')
                        self.client.connect()
                        print('Connected')
                except Exception as e:
                        print('Failed to connect',e)
                        time.sleep(2)
                        self.connectToBroker()

        def sendCoordonateAndAngle(self, xNozzleCoordonate, yNozzleCoordonate,zNozzleCoordonate,pos_servo_1,pos_servo_2,pos_servo_3, nozzleAngle):

            
                leftServoAngle = pos_servo_1.get_position(1)
                rightServoAngle = pos_servo_2.get_position(2)
                rotativeServoAngle = pos_servo_3.get_position(3)

                coodonatesAndAngles = "{},{},{},{},{},{},{}".format(
                xNozzleCoordonate, 
                yNozzleCoordonate, 
                zNozzleCoordonate, 
                leftServoAngle, 
                rightServoAngle, 
                rotativeServoAngle, 
                nozzleAngle
        )
               #udpEsp32SocketClient.sendto( coodonatesAndAngles.encode('utf-8'), unityAddPort)
                try:
                        self.client.publish(TOPIC_PUBLISH,coodonatesAndAngles)
                except Exception as e:
                        print('Connection lost, reconnecting')
                        self.connectToBroker()
          