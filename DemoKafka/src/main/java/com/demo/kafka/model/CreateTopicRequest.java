package com.demo.kafka.model;

import lombok.Data;

@Data
public class CreateTopicRequest {
    private String topic;
    private int partitions;
    private short replicationFactor;
}
