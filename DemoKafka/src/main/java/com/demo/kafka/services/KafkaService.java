package com.demo.kafka.services;

import com.demo.kafka.model.ResultResponse;
import org.apache.kafka.clients.admin.AdminClient;
import org.apache.kafka.clients.admin.AdminClientConfig;
import org.apache.kafka.clients.admin.NewTopic;
import org.apache.kafka.clients.consumer.Consumer;
import org.apache.kafka.clients.consumer.ConsumerConfig;
import org.apache.kafka.clients.consumer.KafkaConsumer;
import org.apache.kafka.clients.producer.KafkaProducer;
import org.apache.kafka.clients.producer.Producer;
import org.apache.kafka.clients.producer.ProducerConfig;
import org.apache.kafka.clients.producer.ProducerRecord;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Service;

import java.util.Collections;
import java.util.Properties;

@Service
public class KafkaService {
    @Value("${spring.kafka.bootstrap-servers}")
    private String bootstrapServers;

    // Producer
    public ResultResponse sendMessage(String topic, String message) {
        Properties props = new Properties();
        props.put(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, bootstrapServers);
        props.put(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringSerializer");
        props.put(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringSerializer");

        try (Producer<String, String> producer = new KafkaProducer<>(props)) {
            ProducerRecord<String, String> record = new ProducerRecord<>(topic, message);
            producer.send(record);
            return ResultResponse.builder().message("Message sent to Kafka topic: " + topic).status(200).build();
        }
        catch (Exception e) {
            return ResultResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Consumer
    @KafkaListener(topics = "${spring.kafka.topic}")
    public void listenMessage(String message) {
        System.out.println("Received message: " + message);
    }

    // Admin functions
    public ResultResponse createTopic(String topic, int partitions, short replicationFactor) {
        Properties props = new Properties();
        props.put(AdminClientConfig.BOOTSTRAP_SERVERS_CONFIG, bootstrapServers);

        try (AdminClient adminClient = AdminClient.create(props)) {
            NewTopic newTopic = new NewTopic(topic, partitions, replicationFactor);
            adminClient.createTopics(Collections.singletonList(newTopic));
            return ResultResponse.builder().message("Topic created: " + topic).status(200).build();
        }
        catch (Exception e) {
            return ResultResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    public ResultResponse deleteTopic(String topic) {
        Properties props = new Properties();
        props.put(AdminClientConfig.BOOTSTRAP_SERVERS_CONFIG, bootstrapServers);

        try (AdminClient adminClient = AdminClient.create(props)) {
            adminClient.deleteTopics(Collections.singletonList(topic));
            return ResultResponse.builder().message("Topic deleted: " + topic).status(200).build();
        }
        catch (Exception e) {
            return ResultResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Consumer group functions
    public Consumer<String, String> createConsumer(String groupId) {
        Properties props = new Properties();
        props.put(ConsumerConfig.BOOTSTRAP_SERVERS_CONFIG, bootstrapServers);
        props.put(ConsumerConfig.GROUP_ID_CONFIG, groupId);
        props.put(ConsumerConfig.KEY_DESERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringDeserializer");
        props.put(ConsumerConfig.VALUE_DESERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringDeserializer");

        return new KafkaConsumer<>(props);
    }

    public void closeConsumer(Consumer<String, String> consumer) {
        consumer.close();
    }
}
