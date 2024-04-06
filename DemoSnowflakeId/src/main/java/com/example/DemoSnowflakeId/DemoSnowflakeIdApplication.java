package com.example.DemoSnowflakeId;

import com.example.DemoSnowflakeId.util.GenerateSnowflakeId;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class DemoSnowflakeIdApplication {

	public static void main(String[] args) {
		long id = GenerateSnowflakeId.generateId(1, 1);
		System.out.println(id);
		SpringApplication.run(DemoSnowflakeIdApplication.class, args);
	}
}
