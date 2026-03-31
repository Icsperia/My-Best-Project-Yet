import socket
import network
import time
import uctypes, gc
import Hiwonder_wifi_ble as HW_wb

from Key import Key
from Led import LED
from USBDevice import *
from Buzzer import Buzzer
from espmax import ESPMax
from PWMServo import PWMServo
from micropython import const
from machine import Pin, ADC, Timer
from RobotControl import RobotControl
from SuctionNozzle import SuctionNozzle
from BusServo import BusServo, have_got_servo_pos
from esp32MqttSubscriber import esp32MQTTSubscriber

from connectToWifiEsp32 import esp32Wifi
from udpSocketEsp32 import udpSocketEsp32
from MQTTEsp32 import MQTTEsp32

print("Please wait...")
key = Key()
led = LED()
buzzer = Buzzer()
pwm = PWMServo()
pwm.work_with_time()
bus_servo = BusServo()
robot = RobotControl()
arm = ESPMax(bus_servo)
nozzle = SuctionNozzle()
pos_servo_1 = BusServo()
pos_servo_2 = BusServo()
pos_servo_3 = BusServo()

ble = HW_wb.Hiwonder_wifi_ble(HW_wb.MODE_BLE_SLAVE, name = 'MaxArm')
ble.set_led_key_io(led=26,key=25)

#Wifi si udp socket
connectToWifi = esp32Wifi()
#MQTTEsp = MQTTEsp32()
MQTTEspSubs = esp32MQTTSubscriber()
#udpEsp32 = udpSocketEsp32()

connectToWifi.connect("Redmi Note 12 Pro 5G","12345678")
#udpEsp32.connectUDP(' 192.168.29.61', 5005)

#MQTTEsp.identificationInfo("MaxArm",'10.206.197.112',1883)
MQTTEspSubs.identificationInfoSubs("MaxArm",'10.206.197.112',1883)
MQTTEspSubs.connectToBroker()
MQTTEspSubs.sub("rotativeBase/topic")

#MQTTEsp.connectToBroker()
#/////////////////////////
arm.go_home()
print_en = True
nozzle_st = False
nozzle_angle = 0
buzzer.setBuzzer(80)
(x,y,z) = arm.ORIGIN
move_sleep = time.ticks_ms()
nozzle.set_angle(nozzle_angle)


#  
# # def MouseHandle():
#   global x,y,z,nozzle_st
#   global nozzle_angle,move_sleep
  
#   BUTTON_L = 0x01
#   BUTTON_R = 0x02
#   BUTTON_M = 0x04
  
#   msg = USBDevice.get_mouse_msg()
  
#   if time.ticks_ms() >= move_sleep: 
#     if msg == False:
#       return 
#     mouse_msg = uctypes.struct(uctypes.addressof(bytes(msg))
#               ,{"button": uctypes.UINT8 | 5,'move_X': uctypes.INT8 | 6
#               , 'move_Y': uctypes.INT8 | 7,'wheel': uctypes.INT8 | 8})

#     if mouse_msg.button & BUTTON_M != 0: # 
#       nozzle.off()
#       arm.go_home(1500)
#       nozzle_angle = 0
#       nozzle_st = False
#       (x,y,z) = arm.ORIGIN
#       buzzer.setBuzzer(80)
#       nozzle.set_angle(nozzle_angle)
#       move_sleep = time.ticks_ms() + 1500
      
#     elif mouse_msg.wheel != 0: # Z
#       dz = -mouse_msg.wheel*2
#       if arm.set_position((x, y, z+dz), 20):
#         z += dz
#       move_sleep = time.ticks_ms() + 30
    
#     else:
#       if abs(mouse_msg.move_X) > abs(mouse_msg.move_Y):
#         if mouse_msg.button & BUTTON_L != 0: # 
#           nozzle_angle += int(mouse_msg.move_X/3)
#           if nozzle_angle > 90:nozzle_angle = 90
#           if nozzle_angle < -90:nozzle_angle = -90
#           nozzle.set_angle(nozzle_angle)
#           move_sleep = time.ticks_ms() + 30
          
#         else:
#           dx = -int(mouse_msg.move_X/8) # X
#           if arm.set_position((x+dx, y, z), 20):
#             x += dx
#           move_sleep = time.ticks_ms() + 30
        
#       elif abs(mouse_msg.move_X) < abs(mouse_msg.move_Y):
#         if mouse_msg.button & BUTTON_R != 0: # 
#           if time.ticks_ms() >= move_sleep:
#             nozzle_st = bool(1 - nozzle_st)
#             if nozzle_st:nozzle.on()
#             else:nozzle.off()
#             move_sleep = time.ticks_ms() + 300
          
#         else:
#           if mouse_msg.button & BUTTON_L == 0:  # Y
#             dy = int(-mouse_msg.move_Y/8)
#             if arm.set_position((x, y+dy, z), 20):
#               y += dy
#             move_sleep = time.ticks_ms() + 30


# ble_buf = bytearray()
# ble_data = bytearray()
# # 
# def ble_callback():
#   global ble_buf
#   global ble_data
  
#   ble_buf += ble.ble_read() # 
#   ble_data = ble_buf
#   ble_buf = bytearray()

# ble.ble_rx_irq(ble_callback)  

# # 

#   global ble_data,reset_sleep,print_en
#   global x,y,z, nozzle_angle, move_sleep
  
#   de = 3
#   if time.ticks_ms() >= move_sleep:
#     if ble.mode == HW_wb.MODE_BLE_SLAVE:
#       ble_data_len = len(ble_data)
#       if ble_data_len >= 4:
#         if ble_data[0] == 0x55 and ble_data[1] == 0x55:
#           if ble_data[2] == ble_data_len - 2:
#             cmd = ble_data[3]
#             if cmd == 0x23: # XYZdef BleHandle():
#               if ble_data[4] < 0x80: dx = ble_data[4] 
#               else: dx = (ble_data[4] - 256)

#               if ble_data[5] < 0x80: dy = ble_data[5]
#               else: dy = (ble_data[5] - 256)
             
#               if ble_data[6] < 0x80: dz = ble_data[6]
#               else: dz = (ble_data[6] - 256)
              
#               if dx== -1 and dy== -1 and dz== -1: # 
#                 arm.go_home(1500)
#                 nozzle.off()
#                 buzzer.setBuzzer(80)
#                 nozzle_angle = 0
#                 nozzle.set_angle(0)
#                 (x,y,z) = arm.ORIGIN
#                 move_sleep = time.ticks_ms() + 1500
                
#               else: # 
#                 if arm.set_position((x+dx*de,y+dy*de,z+dz*de), 20): # ，
#                   x += dx*de 
#                   y += dy*de
#                   z += dz*de
#                   print_en = True
#                 else:  # 
#                   if print_en:
#                     buzzer.setBuzzer(20)
#                     print_en = False
                  
#                 move_sleep = time.ticks_ms() + 30
              
#             if cmd == 0x24:# 
#               if ble_data[4] < 0x80: da = ble_data[4] 
#               else: da = (ble_data[4] - 256)
#               nozzle_angle += da*3
#               nozzle_angle = 90 if nozzle_angle > 90 else nozzle_angle
#               nozzle_angle = -90 if nozzle_angle < -90 else nozzle_angle
#               nozzle.set_angle(nozzle_angle, 80)
#               move_sleep = time.ticks_ms() + 30
              
#             elif cmd == 0x25:# 
#               st = ble_data[4]
#               if st == 1: nozzle.on()
#               elif st == 0: nozzle.off()
#               move_sleep = time.ticks_ms() + 300
              
#             elif cmd == 0x06:#
#               name = ble_data[4]
#               times = ble_data[5]
#               if times == 0:times = 100000
#               robot.runActionGroup(str(name), times)
              
#             elif cmd == 0x07:#
#               robot.stopActionGroup()
    
#   ble_data = bytearray()


def main(t):
  global xDelta,y,z, move_sleep, nozzle_angle
  #，
  
  gc.collect()
  key.run_loop()
  USBDevice.run_loop()
  
  # BleHandle()
  # MouseHandle()

  if key.down_up(): # key1100
    if time.ticks_ms() >= move_sleep:
      robot.runActionGroup("100")
      move_sleep = time.ticks_ms() + 1500
    
  if key.down_long(): # key1
    if time.ticks_ms() >= move_sleep:
      robot.stopActionGroup()
      move_sleep = time.ticks_ms() + 300


print("Start")
# tim = Timer(2)
#  tim.init(period=15, mode=Timer.PERIODIC, callback=main)


threshold  = 0.01
angleInterval = 0
while True:
  
    if MQTTEspSubs.client:
        MQTTEspSubs.client.check_msg()
        
    xDelta = MQTTEspSubs.fbaseAngle
   
    if time.ticks_ms() >= move_sleep:
        
          if arm.set_position((x + xDelta, y, z), 20):
              x += xDelta
             
              xDelta=0
              MQTTEspSubs.fbaseAngle = 0 #
                

          move_sleep = time.ticks_ms() + 30
   
    time.sleep(0.005)














