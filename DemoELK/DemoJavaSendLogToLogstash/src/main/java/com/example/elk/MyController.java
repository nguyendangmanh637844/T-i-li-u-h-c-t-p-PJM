package com.example.elk;

import lombok.extern.slf4j.Slf4j;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api")
@Slf4j
public class MyController {
    @GetMapping
    public String hello() {
        log.atInfo().log("Hello World!");
        return "Hello World!";
    }
}
