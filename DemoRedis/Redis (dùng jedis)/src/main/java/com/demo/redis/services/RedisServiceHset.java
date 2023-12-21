package com.demo.redis.services;

import com.demo.redis.models.RedisHsetInput;
import com.demo.redis.models.RedisResponse;
import com.demo.redis.utils.ConvertUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;

@Service
public class RedisServiceHset {

    @Autowired
    private JedisPool jedisPool;

    private static final Logger LOGGER = LoggerFactory.getLogger(RedisServiceHset.class);

    // Thêm hoặc cập nhật dữ liệu hash
    public RedisResponse<?> addOrUpdateHash(String key, Map<String, String> hash) {
        try (Jedis jedis = jedisPool.getResource()) {
            jedis.hset(key, hash);
            return RedisResponse.builder().message("Hash added/updated successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error adding/updating hash in Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    //Hàm thêm dữ liệu vào Redis nhưng truyền vào Object tự sinh key và value
    public RedisResponse<?> addDataV2(Object object, String primaryKey) {
        try (Jedis jedis = jedisPool.getResource()) {
            RedisHsetInput input = ConvertUtils.getRedisHsetInput(object, primaryKey);
            jedis.hset(input.getKey(), input.getHash());
            return RedisResponse.builder().message("Data added successfully").data(input).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error adding data to Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Lấy dữ liệu từ hash
    public RedisResponse<?> getHash(String key) {
        try (Jedis jedis = jedisPool.getResource()) {
            Map<String, String> value = jedis.hgetAll(key);
            return RedisResponse.builder().message("Hash retrieved successfully").data(value).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error getting hash from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Cập nhật một field cụ thể trong hash
    public RedisResponse<?> updateHashField(String key, String field, String value) {
        try (Jedis jedis = jedisPool.getResource()) {
            jedis.hset(key, field, value);
            return RedisResponse.builder().message("Field updated successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error updating hash field in Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Xóa một field cụ thể trong hash
    public RedisResponse<?> deleteFromHash(String key, String field) {
        try (Jedis jedis = jedisPool.getResource()) {
            jedis.hdel(key, field);
            return RedisResponse.builder().message("Field deleted from hash successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error deleting field from hash in Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Xóa toàn bộ hash
    public RedisResponse<?> deleteHash(String key) {
        try (Jedis jedis = jedisPool.getResource()) {
            jedis.del(key);
            return RedisResponse.builder().message("Hash deleted successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error deleting hash from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Lấy tất cả các key của hash
    public RedisResponse<?> getAllHashKeys() {
        try (Jedis jedis = jedisPool.getResource()) {
            Set<String> data = jedis.keys("*");
            return RedisResponse.builder().message("Hash keys retrieved successfully").data(data).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error getting hash keys from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Lấy tất cả các field và giá trị trong một hash
    public RedisResponse<?> getAllFieldsAndValuesInHash(String key) {
        try (Jedis jedis = jedisPool.getResource()) {
            Map<String, String> data = jedis.hgetAll(key);
            return RedisResponse.builder().message("Hash fields and values retrieved successfully").data(data).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error getting hash fields and values from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }


    // Xóa tất cả dữ liệu trong Redis
    public RedisResponse<?> deleteAllData() {
        try (Jedis jedis = jedisPool.getResource()) {
            Set<String> keys = jedis.keys("*");
            for (String key : keys) {
                jedis.del(key);
            }
            return RedisResponse.builder().message("All data deleted successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error deleting all data from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Lấy dữ liệu hash theo mẫu
    public RedisResponse<?> getHashesByPattern(String pattern) {
        Map<String, Map<String, String>> allHashes = new HashMap<>();

        try (Jedis jedis = jedisPool.getResource()) {
            Set<String> keys = jedis.keys(pattern);
            for (String key : keys) {
                Map<String, String> hashData = jedis.hgetAll(key);
                allHashes.put(key, hashData);
            }
            return RedisResponse.builder().message("Hashes retrieved successfully").data(allHashes).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error getting hashes by pattern from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Xóa dữ liệu hash theo mẫu
    public RedisResponse<?> deleteHashesByPattern(String pattern) {
        try (Jedis jedis = jedisPool.getResource()) {
            Set<String> keys = jedis.keys(pattern);
            for (String key : keys) {
                jedis.del(key);
                LOGGER.info("Deleted hash: {}", key);
            }
            return RedisResponse.builder().message("Hashes deleted successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error deleting hashes by pattern {}: {}", pattern, e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Lấy tất cả dữ liệu hash
    public RedisResponse<?> getAllHashData() {
        Map<String, Map<String, String>> allHashData = new HashMap<>();

        try (Jedis jedis = jedisPool.getResource()) {
            Set<String> keys = jedis.keys("*");
            for (String key : keys) {
                Map<String, String> hashData = jedis.hgetAll(key);
                allHashData.put(key, hashData);
            }
            return RedisResponse.builder().message("All hash data retrieved successfully").data(allHashData).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error getting all hash data from Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Lấy một giá trị cụ thể từ hash
    public RedisResponse<?> getHashValue(String key, String field) {
        try (Jedis jedis = jedisPool.getResource()) {
            if (!jedis.exists(key)) {
                return RedisResponse.builder().message("Key does not exist").status(404).build();
            }
            String value = jedis.hget(key, field);
            if (value == null) {
                return RedisResponse.builder().message("Field not found").status(404).build();
            }
            return RedisResponse.builder().message("Field retrieved successfully").data(value).status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error getting field from hash in Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Xóa một trường cụ thể trong hash
    public RedisResponse<?> deleteHashField(String key, String field) {
        try (Jedis jedis = jedisPool.getResource()) {
            Long result = jedis.hdel(key, field);
            if (result == 0) {
                return RedisResponse.builder().message("Field not found or key does not exist").status(404).build();
            }
            return RedisResponse.builder().message("Field deleted successfully").status(200).build();
        } catch (Exception e) {
            LOGGER.error("Error deleting field from hash in Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }

    // Kiểm tra trường có tồn tại trong hash không
    public RedisResponse<?> hashFieldExists(String key, String field) {
        try (Jedis jedis = jedisPool.getResource()) {
            boolean exists = jedis.hexists(key, field);
            return RedisResponse.builder().message(exists ? "Field exists" : "Field does not exist").status(exists ? 200 : 404).build();
        } catch (Exception e) {
            LOGGER.error("Error checking if field exists in hash in Redis: {}", e.getMessage());
            return RedisResponse.builder().message(e.getMessage()).status(500).build();
        }
    }
}
