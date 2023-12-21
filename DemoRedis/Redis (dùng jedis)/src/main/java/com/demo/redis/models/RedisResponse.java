package com.demo.redis.models;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class RedisResponse<T> {

    private String message;
    private int status;
    private T data;
}
