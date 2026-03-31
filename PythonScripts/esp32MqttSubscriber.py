import socket
import time
from umqttsimple import MQTTClient


class esp32MQTTSubscriber:

        def __init__(self):
        
            self.client = None
            self.fbase_angle = 0.0
        
        
        def identificationInfoSubs(self,CLIENT_ID, MQTT_SERVER, MQTT_PORT):
                self.client = MQTTClient(CLIENT_ID, MQTT_SERVER, MQTT_PORT)
                self.client.set_callback(self.CallBack)
        def CallBack(self, topic, msg):
            try:
                data = msg.decode()
        
                
                for i in data.split():
                    try:
           
                        self.fbaseAngle = float(i)
                        print(self.fbaseAngle)
                    except ValueError:
                 
                        continue 
            except Exception as e:
                print("Eroare neprevazuta in CallBack:", e)
        
               
               

        
      
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

              

               
               
