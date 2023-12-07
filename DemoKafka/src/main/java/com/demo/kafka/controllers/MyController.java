package com.demo.kafka.controllers;

import com.demo.kafka.model.ConsumeGroupRequest;
import com.demo.kafka.model.CreateTopicRequest;
import com.demo.kafka.model.ResultResponse;
import com.demo.kafka.model.SendMessageRequest;
import com.demo.kafka.services.KafkaService;
import org.apache.kafka.clients.consumer.Consumer;
import org.apache.kafka.clients.consumer.ConsumerRecords;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.Duration;
import java.util.Collections;

@RestController
@RequestMapping("api/kafka")
public class MyController {
    @Autowired
    private KafkaService kafkaService;

    // Endpoint để gửi message đến Kafka
    @PostMapping("/send")
    public ResponseEntity<?> sendMessage(@RequestBody SendMessageRequest request) {
        String topic = request.getTopic();
        String message = request.getMessage();
        ResultResponse resultResponse = kafkaService.sendMessage(topic, message);
        return ResponseEntity.ok(resultResponse);
    }

    // Endpoint để nghe message từ Kafka (sử dụng @KafkaListener)
    // (Chú ý: @KafkaListener sẽ tự động chạy khi có message đến)
    @GetMapping("/listen")
    public String listenMessage() {
        return "Waiting for messages...";
    }

    // Endpoint để tạo topic
    @PostMapping("/create-topic")
    public ResponseEntity<?> createTopic(@RequestBody CreateTopicRequest request) {
        String topic = request.getTopic();
        int partitions = request.getPartitions();
        short replicationFactor = request.getReplicationFactor();
        ResultResponse resultResponse = kafkaService.createTopic(topic, partitions, replicationFactor);
        return ResponseEntity.ok(resultResponse);
    }

    // Endpoint để xóa topic
    @DeleteMapping("/delete-topic/{topic}")
    public ResponseEntity<?> deleteTopic(@PathVariable String topic) {
        ResultResponse resultResponse = kafkaService.deleteTopic(topic);
        return ResponseEntity.ok(resultResponse);
    }

    // Endpoint để sử dụng Consumer group
    // Endpoint để sử dụng Consumer group
    @PostMapping("/consume-group")
    public ResponseEntity<?> consumeMessages(@RequestBody ConsumeGroupRequest request) {
        String groupId = request.getGroupId();
        String topic = request.getTopic();

        try (Consumer<String, String> consumer = kafkaService.createConsumer(groupId)) {
            consumer.subscribe(Collections.singletonList(topic));

            // Nghe message với consumer group
            ConsumerRecords<String, String> records = consumer.poll(Duration.ofMillis(1000));
            records.forEach(record -> System.out.println("Received message from " + groupId + ": " + record.value()));
            return ResponseEntity.ok(records);
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }
}
