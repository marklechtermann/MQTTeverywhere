FROM gradle:jdk13 as build

WORKDIR /app
ADD . .

RUN gradle shadowJar

FROM adoptopenjdk:13.0.2_8-jre-hotspot-bionic

WORKDIR /app

COPY --from=build /app/build/libs/mqttjava-all.jar /app

ENTRYPOINT ["java", "-jar", "mqttjava-all.jar"]