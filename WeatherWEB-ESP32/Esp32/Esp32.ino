#include <WiFi.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>
#include<MQ135.h>
#include <DHT.h>
#include <stdlib.h>


// WiFi
const char *ssid = "nDung1930";
const char *password = "NDung2001@";

// MQTT Broker
const char *mqtt_broker = "broker.emqx.io";
const char *mqtt_username = "admin";
const char *mqtt_password = "1234";
const int mqtt_port = 1883;
const char *DataSensor = "esp32/DataSensor";

// Pin of Sensor
#define PIN_MQ135 35
#define  DHTPIN  15
#define RAIN_SENSOR   34
#define  DHTTYPE  DHT22

// size of buffer
#define BUFFER_SIZE 10

//Variable Sensor
struct SensorData {
  float humidity;
  float temperature;
  int AirSensor;
  int RAIN_SENSOR_Value;
};
SensorData buffer[BUFFER_SIZE];
int bufferIndex = 0;

//JsonSend - send to Broker
String jsondata = "";

// JsonReceive - receive to Broker
String JsonReceive = "";

const unsigned long sendInterval = 60000;
unsigned long lastSendTime = 0;



WiFiClient espClient;
PubSubClient client(espClient);
DHT dht(DHTPIN, DHTTYPE);

bool wifiConnected = false;
void ConnectToWifi() {
  WiFi.begin(ssid, password);
  while (!wifiConnected) {
    delay(500);
    Serial.println("Connecting to WiFi..");
    if (WiFi.status() == WL_CONNECTED) {
      wifiConnected = true;
    }
  }
  Serial.println("Connected to the WiFi network");
}

void ConnectToBorker() {
  client.setServer(mqtt_broker, mqtt_port);
  client.setCallback(callback);
  while (!client.connected()) {
    String client_id = "clientId-F0SIVDycNX";
    Serial.printf("The client %s connects to the public mqtt broker: ", client_id.c_str());
    if (client.connect(client_id.c_str(), mqtt_username, mqtt_password)) {
      Serial.println("Mqtt broker connected");
    } else {
      Serial.print("failed with state: ");
      Serial.print(client.state());
      delay(2000);
    }
  }
}

void setup() {
  Serial.begin(115200);
  dht.begin();
  pinMode(RAIN_SENSOR, INPUT);
  pinMode(PIN_MQ135, INPUT );
  ConnectToWifi();
  ConnectToBorker();

}

//function callback: receive data from broker
void callback(char *topic, byte *payload, unsigned int length) {
  Serial.print("Message arrived in topic: ");
  Serial.println(topic);
  String topicStr = topic;
  for (int i = 0; i < length; i++) {
    JsonReceive += (char) payload[i];
  }
  Serial.print("JsonReceive: ");
  Serial.println(JsonReceive);
  Serial.println("-----------------------");
  JsonReceive = "";
}



void getValueOfSensor() {
  SensorData data;
  data.humidity  = dht.readHumidity();
  data.temperature = dht.readTemperature();
  data.RAIN_SENSOR_Value = digitalRead(RAIN_SENSOR);
  data.AirSensor = analogRead(PIN_MQ135);

  
  buffer[bufferIndex] = data;
  bufferIndex = (bufferIndex + 1) % BUFFER_SIZE;
}

void send_data() {
  getValueOfSensor();

  int latestIndex = (bufferIndex - 1 + BUFFER_SIZE) % BUFFER_SIZE;  
  SensorData latestData = buffer[latestIndex]; 

  DynamicJsonDocument jsonBuffer(BUFFER_SIZE);
  JsonObject json = jsonBuffer.to<JsonObject>();
  json["humidity"] = latestData.humidity;
  json["temperature"] = latestData.temperature;
  json["rain"] = latestData.RAIN_SENSOR_Value;
  json["air"] = latestData.AirSensor;

  String jsondata;
  serializeJson(json, jsondata);
  client.publish(DataSensor, jsondata.c_str(), true);
  Serial.println(jsondata);
  bufferIndex = 0;
}

void loop() {
  client.loop();
  if (millis() - lastSendTime >= sendInterval) {
    send_data();
    lastSendTime = millis();
  }
}
