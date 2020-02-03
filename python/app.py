import paho.mqtt.client as mqtt
import sys

print(f'Starting ..')


def on_connect(client, userdata, flags, rc):
    print(f'Connect to {host} on port 1883 ...')
    client.subscribe("temperature/+")


def on_message(client, userdata, msg):
    print(f'Received: {msg.topic} {msg.payload}')


host = "localhost"
if len(sys.argv) > 1:
    host = sys.argv[1]

print(f'Try to connect to {host} on port 1883 ...')
client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message
client.connect(host, 1883, 60)
client.loop_forever()
