package com.demo.redis.services;

import com.demo.redis.models.RedisResponse;
import com.demo.redis.models.RedisSetInput;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;
import redis.clients.jedis.params.ScanParams;
import redis.clients.jedis.resps.ScanResult;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Service
public class RedisService {

    @Autowired
    private JedisPool jedisPool;

    private static final Logger LOGGER = LoggerFactory.getLogger(RedisService.class);

    // Hàm thêm dữ liệu vào Redis
    public RedisResponse<?> addData(RedisSetInput request) {
        try (Jedis jedis = jedisPool.getResource()) {
            jedis.set(request.getKey(), request.getValue());
            return RedisResponse.builder().message("Data added successfully").data(request).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error adding data to Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Hàm sửa dữ liệu trong Redis
    public RedisResponse<?> updateData(RedisSetInput request) {
        try (Jedis jedis = jedisPool.getResource()) {
            if (jedis.exists(request.getKey())) {
                jedis.set(request.getKey(), request.getValue());
            }
            return RedisResponse.builder().message("Data updated successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error updating data in Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Hàm xóa dữ liệu từ Redis
    public RedisResponse<?> deleteData(String key) {
        try (Jedis jedis = jedisPool.getResource()) {
            jedis.del(key);
            return RedisResponse.builder().message("Data deleted successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error deleting data from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Hàm lấy tất cả keys từ Redis
    public RedisResponse<?> getAllKeys() {
        try (Jedis jedis = jedisPool.getResource()) {
            var data = jedis.keys("*");
            return RedisResponse.builder().message("Data getted successfully").data(data).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error getting all keys from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Hàm lấy dữ liệu từ Redis theo pattern
    public RedisResponse<?> getDataByPattern(String pattern) {
        try (Jedis jedis = jedisPool.getResource()) {
            var data = jedis.keys(pattern);
            return RedisResponse.builder().message("Data getted successfully").data(data).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error getting data from Redis by pattern: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    public RedisResponse<?> deleteAllData() {
        try (Jedis jedis = jedisPool.getResource()) {
            jedis.flushAll();
            return RedisResponse.builder().message("All data deleted successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error deleting all data from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    public RedisResponse<?> deleteByPattern(String pattern) {
        try (Jedis jedis = jedisPool.getResource()) {
            ScanParams scanParams = new ScanParams().match(pattern);
            String cursor = "0";

            do {
                ScanResult<String> scanResult = jedis.scan(cursor, scanParams);
                List<String> keys = scanResult.getResult();

                for (String key : keys) {
                    jedis.del(key);
                    LOGGER.info("Deleted key: {}", key);
                }

                cursor = scanResult.getCursor();
            } while (!"0".equals(cursor));
        } catch (Exception e) {
            LOGGER.error("Error deleting keys by pattern {}: {}", pattern, e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
        return RedisResponse.builder().message("Data deleted successfully").status(200).build();
    }

    public RedisResponse<?> get(String key) {
        try (Jedis jedis = jedisPool.getResource()) {
            Object value = jedis.get(key);
            return RedisResponse.builder().message("Data getted successfully").data(value).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error getting data from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }

    }

    public RedisResponse<?> getAllData() {
        Map<String, String> allData = new HashMap<>();

        try (Jedis jedis = jedisPool.getResource()) {
            ScanParams scanParams = new ScanParams().count(100); // Số lượng keys muốn lấy mỗi lần quét
            String cursor = "0";

            do {
                ScanResult<String> scanResult = jedis.scan(cursor, scanParams);
                List<String> keys = scanResult.getResult();

                for (String key : keys) {
                    String value = jedis.get(key);
                    allData.put(key, value);
                }

                cursor = scanResult.getCursor();
            } while (!cursor.equals("0"));

        } catch (Exception e) {
            LOGGER.error("Error getting all data from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }

        return RedisResponse.builder().message("Data getted successfully").data(allData).status(200).build();
    }
}
