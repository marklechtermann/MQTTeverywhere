var mqtt = require('mqtt')

let hostname = "localhost"
if (process.argv.length > 2) {
    hostname = process.argv[2];
}

console.log(hostname)

var client = mqtt.connect(`mqtt://${hostname}`)

client.on('connect', function () {

    setInterval(() => {

        const temp1 = Math.random() * 5 + 25;
        const temp2 = Math.random() * 5 + 20;

        console.log(`Published: temperature/livingroom: ${temp1} `);
        console.log(`Published: temperature/kitchen: ${temp2} `);

        client.publish('temperature/livingroom', `${temp1}`)
        client.publish('temperature/kitchen', `${temp2}`);
    }, 500);
})
