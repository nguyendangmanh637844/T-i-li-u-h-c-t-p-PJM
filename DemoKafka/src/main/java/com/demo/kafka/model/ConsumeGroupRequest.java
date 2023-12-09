package com.demo.kafka.model;

import lombok.Data;

@Data
public class ConsumeGroupRequest {
    private String groupId;
    private String topic;
}
