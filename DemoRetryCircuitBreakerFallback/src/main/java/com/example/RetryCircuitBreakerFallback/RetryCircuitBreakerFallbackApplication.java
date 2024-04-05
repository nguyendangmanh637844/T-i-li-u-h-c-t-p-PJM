package com.example.RetryCircuitBreakerFallback;

import com.example.RetryCircuitBreakerFallback.utils.ResilienceUtil;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import java.util.Random;
import java.util.function.Supplier;

@SpringBootApplication
public class RetryCircuitBreakerFallbackApplication {

	public static void main(String[] args) {
		SpringApplication.run(RetryCircuitBreakerFallbackApplication.class, args);
		Supplier<Integer> numberSupplier = () -> {
			// Simulate getting a random number, but sometimes throw an exception
			Random random = new Random();
			if (random.nextInt(10) < 11) { // 30% chance of throwing an exception
				throw new RuntimeException("Failed to get random number");
			}
			return random.nextInt(10) + 1;
		};

		Supplier<Integer> fallbackSupplier = () -> {
			// Provide a default number
			return 5;
		};

		int number = ResilienceUtil.executeWithFallback(numberSupplier, fallbackSupplier);
		System.out.println("Number: " + number);
	}
}