package com.demo.redis.utils;

import com.demo.redis.models.RedisHsetInput;
import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import lombok.experimental.UtilityClass;

import java.lang.reflect.Field;
import java.util.HashMap;
import java.util.Map;

@UtilityClass
public class ConvertUtils {
    private static final Gson GSON = new Gson();

    public RedisHsetInput getRedisHsetInput(Object obj, String primaryKeyFieldName) throws IllegalAccessException {
        if (obj == null) {
            throw new IllegalArgumentException("Object for conversion cannot be null");
        }

        RedisHsetInput redisHsetInput = new RedisHsetInput();

        Field primaryKeyField = getField(obj.getClass(), primaryKeyFieldName);
        primaryKeyField.setAccessible(true);
        Object primaryKeyValue = primaryKeyField.get(obj);

        if (primaryKeyValue == null) {
            throw new IllegalStateException("Primary key field value cannot be null");
        }

        String key = obj.getClass().getSimpleName() + ":" + primaryKeyValue.toString();
        redisHsetInput.setKey(key);
        redisHsetInput.setHash(convertObjectToMap(obj));

        return redisHsetInput;
    }

    private Map<String, String> convertObjectToMap(Object obj) {
        Map<String, String> map = new HashMap<>();
        JsonObject jsonObject = GSON.toJsonTree(obj).getAsJsonObject();

        for (Map.Entry<String, JsonElement> entry : jsonObject.entrySet()) {
            JsonElement element = entry.getValue();
            if (element.isJsonPrimitive()) {
                if (element.getAsJsonPrimitive().isNumber()) {
                    // Kiểm tra và chuyển đổi số nguyên và số thực
                    Number num = element.getAsNumber();
                    // Nếu số này là số nguyên (không có phần thập phân)
                    if (num.doubleValue() == num.longValue()) {
                        map.put(entry.getKey(), String.valueOf(num.longValue()));
                    } else {
                        map.put(entry.getKey(), String.valueOf(num.doubleValue()));
                    }
                } else {
                    map.put(entry.getKey(), element.getAsString());
                }
            } else {
                map.put(entry.getKey(), element.toString());
            }
        }

        return map;
    }

    private Field getField(Class<?> clazz, String fieldName) {
        try {
            Field field = clazz.getDeclaredField(fieldName);
            field.setAccessible(true);
            return field;
        } catch (NoSuchFieldException e) {
            throw new RuntimeException("Field not found: " + fieldName, e);
        }
    }
}
