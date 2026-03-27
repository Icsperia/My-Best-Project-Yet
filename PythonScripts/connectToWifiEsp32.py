import network
import time 

class esp32Wifi:
    def connect(self,nerworkssID, nerworkPassword):
        ssid= nerworkssID
        password = nerworkPassword
        wlan = network.WLAN(network.STA_IF)
        wlan.active = True
        wlan.connect(ssid, password)
        while wlan.connected() == False:
            print('Waiting for connection...')
            time.sleep(1)
        print('Connected on {ip}'.format(ip = wlan.ifconfig()[0]))