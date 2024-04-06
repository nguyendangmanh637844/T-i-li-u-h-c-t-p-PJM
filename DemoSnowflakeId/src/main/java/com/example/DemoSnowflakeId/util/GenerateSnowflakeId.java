package com.example.DemoSnowflakeId.util;

import cn.ipokerface.snowflake.SnowflakeIdGenerator;
import lombok.experimental.UtilityClass;

@UtilityClass
public class GenerateSnowflakeId {

    public long generateId(long datacenterId, long machineId) {
//        datacenterId: Định danh cho trung tâm dữ liệu (data center). Nếu bạn có nhiều trung tâm dữ liệu, mỗi trung tâm sẽ có một datacenterId riêng biệt. Trong ví dụ của bạn, datacenterId được thiết lập là 1L, ngụ ý rằng máy chủ này thuộc vào trung tâm dữ liệu với ID là 1.
//        machineId: Định danh cho máy chủ (hoặc instance). Nếu có nhiều máy chủ trong cùng một trung tâm dữ liệu, mỗi máy chủ sẽ có một machineId riêng biệt. Trong ví dụ của bạn, machineId được thiết lập là 1L, ngụ ý rằng máy chủ này có ID là 1 trong trung tâm dữ liệu.
        SnowflakeIdGenerator idGenerator = new SnowflakeIdGenerator(datacenterId, machineId);
        long snowflakeId = idGenerator.nextId();
        return snowflakeId;
    }
}
