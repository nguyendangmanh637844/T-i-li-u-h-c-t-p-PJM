package com.example.elk.controllers;

import com.example.elk.models.GetByFieldNameRequest;
import com.example.elk.repositories.IElasticSearchTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api")
public class MyController {
    @Autowired
    private IElasticSearchTemplate elasticsearchTemplate;

    @PostMapping("/getByFieldName")
    public ResponseEntity<?> getByFieldName(@RequestBody GetByFieldNameRequest request) {
        return ResponseEntity.ok(elasticsearchTemplate.searchByCustomQuery(request.getValue(), request.getIndex()));
    }
}
