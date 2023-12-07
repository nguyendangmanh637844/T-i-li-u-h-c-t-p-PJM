package com.demo.kafka.model;

import lombok.Data;

@Data
public class SendMessageRequest {
    private String topic;
    private String message;
}
