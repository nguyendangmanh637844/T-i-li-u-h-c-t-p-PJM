package com.demo.redis.controllers;

import com.demo.redis.models.GetDataByPatternRequest;
import com.demo.redis.models.RedisRequest;
import com.demo.redis.models.RedisResponse;
import com.demo.redis.models.RedisSetInput;
import com.demo.redis.services.RedisService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/redis")
public class RedisSetController {

    @Autowired
    private RedisService redisService;

    @PostMapping("/add")
    public ResponseEntity<String> addData(@RequestBody RedisRequest redisRequest) {
        RedisResponse<?> response = redisService.addData(new RedisSetInput(redisRequest.getKey(), redisRequest.getValue()));
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok("Data added to Redis successfully.");
    }

    @PutMapping("/update")
    public ResponseEntity<String> updateData(@RequestBody RedisRequest redisRequest) {
        RedisResponse<?> response = redisService.updateData(new RedisSetInput(redisRequest.getKey(), redisRequest.getValue()));
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok("Data in Redis updated successfully.");
    }


    @GetMapping("/keys")
    public ResponseEntity<?> getAllKeys() {
        RedisResponse<?> response = redisService.getAllKeys();
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok(response.getData());
    }

    @GetMapping("/data")
    public ResponseEntity<?> getAllData() {
        RedisResponse<?> response = redisService.getAllData();
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok(response.getData());
    }

    @PostMapping("/keys/pattern")
    public ResponseEntity<?> getDataByPattern(@RequestBody GetDataByPatternRequest request) {
        RedisResponse<?> response = redisService.getDataByPattern(request.getPattern());
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok(response.getData());
    }

    @GetMapping("/get/{key}")
    public ResponseEntity<?> getDataByKey(@PathVariable String key) {
        RedisResponse<?> response = redisService.get(key);
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok(response.getData());
    }

    @GetMapping("/delete-all")
    public ResponseEntity<String> deleteAllData() {
        RedisResponse<?> response = redisService.deleteAllData();
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok("All data deleted from Redis successfully.");
    }

    @PostMapping("/delete-by-pattern")
    public ResponseEntity<String> deleteDataByPattern(@RequestBody GetDataByPatternRequest request) {
        RedisResponse<?> response = redisService.deleteByPattern(request.getPattern());
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok("Data deleted from Redis successfully.");
    }

    @DeleteMapping("/delete")
    public ResponseEntity<String> deleteData(@RequestParam String key) {
        RedisResponse<?> response = redisService.deleteData(key);
        if (response.getStatus() != 200) {
            return ResponseEntity.internalServerError().body(response.getMessage());
        }
        return ResponseEntity.ok("Data deleted from Redis successfully.");
    }
}
