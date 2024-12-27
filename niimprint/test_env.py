import click
from PIL import Image
import serial

print("Click version:", click.__version__)
print("Pillow version:", Image.__version__)
print("PySerial version:", serial.VERSION)
