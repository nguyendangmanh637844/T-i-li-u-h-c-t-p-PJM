package com.demo.redis.controllers;

import com.demo.redis.entities.User;
import com.demo.redis.models.RedisResponse;
import com.demo.redis.services.RedisServiceHset;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("api/hset")
@RequiredArgsConstructor
public class RedisHsetController {
    private final RedisServiceHset redisService;

    @PostMapping("/add")
    public ResponseEntity<?> addData(@RequestBody User redisRequest) {
        RedisResponse<?> response = redisService.addDataV2(redisRequest, "id");
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok(response.getData());
    }

    @GetMapping("/get")
    public ResponseEntity<?> getData() {
        RedisResponse<?> response = redisService.getAllHashData();
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok(response.getData());
    }
}
