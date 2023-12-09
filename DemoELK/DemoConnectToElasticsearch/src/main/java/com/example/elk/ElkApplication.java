package com.example.elk;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.data.elasticsearch.repository.config.EnableElasticsearchRepositories;

@SpringBootApplication
@EnableElasticsearchRepositories(basePackages = "com.example.elk.repositories")
public class ElkApplication {

	public static void main(String[] args) {
		SpringApplication.run(ElkApplication.class, args);
	}

}
