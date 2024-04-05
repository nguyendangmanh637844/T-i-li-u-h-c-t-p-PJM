package com.example.RetryCircuitBreakerFallback.utils;

import io.github.resilience4j.circuitbreaker.CircuitBreaker;
import io.github.resilience4j.circuitbreaker.CircuitBreakerConfig;
import io.github.resilience4j.ratelimiter.RateLimiter;
import io.github.resilience4j.ratelimiter.RateLimiterConfig;
import io.github.resilience4j.retry.Retry;
import io.github.resilience4j.retry.RetryConfig;
import lombok.experimental.UtilityClass;

import java.time.Duration;
import java.util.function.Supplier;

@UtilityClass
public class ResilienceUtil {
    private static final ResilienceConfig RESILIENCE_CONFIG = new ResilienceConfig();

    public static <T> T executeWithResilience(Supplier<T> supplier) {
        Supplier<T> decoratedSupplier = RESILIENCE_CONFIG.decorateSupplier(supplier);
        return decoratedSupplier.get();
    }

    public static <T> T executeWithFallback(Supplier<T> supplier, Supplier<T> fallbackSupplier) {
        Supplier<T> decoratedSupplier = RESILIENCE_CONFIG.decorateSupplier(supplier);
        try {
            return decoratedSupplier.get();
        } catch (Exception e) {
            return fallbackSupplier.get();
        }
    }

    private static class ResilienceConfig {
        private final int MAX_RETRIES = 3;
        private final Duration WAIT_DURATION = Duration.ofSeconds(2);
        private final Duration CIRCUIT_BREAKER_TIMEOUT = Duration.ofSeconds(60);
        private final int RATE_LIMIT = 10;
        private final Duration RATE_LIMIT_TIMEOUT = Duration.ofMinutes(1);

        private final CircuitBreakerConfig circuitBreakerConfig;
        private final RateLimiterConfig rateLimiterConfig;
        private final RetryConfig retryConfig;

        public ResilienceConfig() {
            circuitBreakerConfig = CircuitBreakerConfig.custom()
                    .failureRateThreshold(50)
                    .waitDurationInOpenState(CIRCUIT_BREAKER_TIMEOUT)
                    .build();
            rateLimiterConfig = RateLimiterConfig.custom()
                    .limitForPeriod(RATE_LIMIT)
                    .limitRefreshPeriod(RATE_LIMIT_TIMEOUT)
                    .timeoutDuration(Duration.ofSeconds(5))
                    .build();
            retryConfig = RetryConfig.custom()
                    .maxAttempts(MAX_RETRIES)
                    .waitDuration(WAIT_DURATION)
                    .build();
        }

        public <T> Supplier<T> decorateSupplier(Supplier<T> supplier) {
            CircuitBreaker circuitBreaker = CircuitBreaker.of("myCircuitBreaker", circuitBreakerConfig);
            RateLimiter rateLimiter = RateLimiter.of("myRateLimiter", rateLimiterConfig);
            Retry retry = Retry.of("myRetry", retryConfig);

            Supplier<T> decoratedSupplier = Retry.decorateSupplier(retry, supplier);
            decoratedSupplier = RateLimiter.decorateSupplier(rateLimiter, decoratedSupplier);
            decoratedSupplier = CircuitBreaker.decorateSupplier(circuitBreaker, decoratedSupplier);
            return decoratedSupplier;
        }
    }
}