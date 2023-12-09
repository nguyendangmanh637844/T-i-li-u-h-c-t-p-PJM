-Thêm vào pom.xml
    <!-- Logback -->
    <dependency>
        <groupId>net.logstash.logback</groupId>
        <artifactId>logstash-logback-encoder</artifactId>
        <version>7.4</version> <!-- Thay thế phiên bản phù hợp nhất -->
    </dependency>
-Tạo một file logback-spring.xml trong src/main/resources có nội dung như sau
    <configuration>
        <appender name="LOGSTASH" class="net.logstash.logback.appender.LogstashTcpSocketAppender">
            <destination>localhost:5044</destination> <!-- Địa chỉ của Logstash -->
            <encoder class="net.logstash.logback.encoder.LogstashEncoder">
                <fieldNames>
                    <timestamp>timestamp</timestamp>
                    <version>version</version>
                    <message>message</message>
                    <logger>loggerName</logger>
                    <thread>thread</thread>
                    <levelValue>level</levelValue>
                    <stackTrace>stacktrace</stackTrace>
                </fieldNames>
                <customFields>{"@metadata":{"index":"java-log"}}</customFields>
            </encoder>
        </appender>

        <root level="INFO">
            <appender-ref ref="LOGSTASH" />
        </root>
    </configuration>
-Trong file application.properties thêm cấu hình 
    # Cấu hình logstash
    logging.file.name=logs/myapp.log
    logging.level.net.logstash.logback.encoder=TRACE
    logging.logback.appender.console.Target=System.out
    logging.logback.appender.console.encoder=net.logstash.logback.encoder.LogstashEncoder
    logging.logback.appender.console.encoder.charset=UTF-8
    logging.logback.appender.console.encoder.fieldNames=@timestamp,version,logger_name,thread_name,message,log_level
    logging.logback.appender.console.encoder.fieldName.message=message
    logging.logback.appender.console.encoder.fieldName.level=log_level
    logging.logback.appender.console.encoder.fieldName.logger=logger_name
    logging.logback.appender.console.encoder.fieldName.thread=thread_name
    logging.logback.appender.console.encoder.includeContext=true
- Sử dụng: Dùng anotation @Slf4j để tạo một logger rồi dùng để log bình thường