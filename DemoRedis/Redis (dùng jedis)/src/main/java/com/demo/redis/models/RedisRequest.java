package com.demo.redis.models;

import lombok.Data;

@Data
public class RedisRequest {
    private String key;
    private String value;
}
