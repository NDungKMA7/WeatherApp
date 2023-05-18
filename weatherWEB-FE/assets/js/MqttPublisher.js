let mqttClient;
var topic = "esp32/DataSensor";
window.addEventListener("load", (event) => {
  connectToBroker();
  mqttClient.subscribe(topic, { qos: 0 });
});

function connectToBroker() {
  const clientId = "client" + Math.random().toString(36).substring(7);

  const host = "ws://broker.emqx.io:8083/mqtt";

  const options = {
    keepalive: 60,
    clientId: clientId,
    protocolId: "MQTT",
    protocolVersion: 4,
    clean: true,
    reconnectPeriod: 1000,
    connectTimeout: 30 * 1000,
  };

  mqttClient = mqtt.connect(host, options);

  mqttClient.on("error", (err) => {
    console.log("Error: ", err);
    mqttClient.end();
  });

  mqttClient.on("reconnect", () => {
    console.log("Reconnecting...");
  });

  mqttClient.on("connect", () => {
    console.log("Client connected:" + clientId);
  });

  // Received
  mqttClient.on("message", (topic, message, packet) => {
    SaveData(message.toString());
  });
}

function SaveData(str){
  axios({
    method: 'post',
    url: 'https://localhost:7296/Home/SaveData',
    data: {
      "body": str,
    }
  }).then((res) => { 
    console.log(res);
  })
  .catch((err) => { 
    console.log(err);
  });;
}
