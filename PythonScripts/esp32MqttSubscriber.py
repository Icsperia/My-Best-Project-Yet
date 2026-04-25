import socket
import time
from umqttsimple import MQTTClient


class esp32MQTTSubscriber:

        def __init__(self):

            self.client = None
            self.fbaseAngle = 0.0
            self.verticalArmAngle=0.0
            self.upDownAngle=0.0
            
        
        
        def identificationInfoSubs(self,CLIENT_ID, MQTT_SERVER, MQTT_PORT):
                self.client = MQTTClient(CLIENT_ID, MQTT_SERVER, MQTT_PORT)
                self.client.set_callback(self.CallBack)
        def CallBack(self, topic, msg):
            try:
        
                topic_str = topic.decode()
      
                msg_str = msg.decode().strip()
        
       
                print("MQTT RECEIVE -> Topic: {} | Msg: [{}]".format(topic_str, msg_str))
        
                msgReceived = float(msg_str)
        
                if topic_str == "rotativeBase/topic":
                    self.fbaseAngle = msgReceived 
                elif topic_str == "verticalArm/topic":
                    self.verticalArmAngle = msgReceived 
                elif topic_str == "upDownSegment/topic":
                    self.upDownAngle = msgReceived 

            except ValueError:
                print("Eroare conversie float: Nu pot converti '{}'".format(msg))
            except Exception as e:
                print("Eroare neprevăzută în CallBack:", e)
          
                      
               

        
      
        def connectToBroker(self):
                try:
                        print('Connect to Mqtt broker...')
                        self.client.connect()
                        print('Connected')
                except Exception as e:
                        print('Failed to connect',e)
                        time.sleep(2)
                        self.connectToBroker()
        
        def sub(self,topic):
            
                self.client.subscribe(topic)
            
#                 while True:
#                         if True:
#                                 self.client.wait_msg()
#                         else:
#                                 self.client.check_msg()
#                                 time.sleep(1)
# 

              

               
               

