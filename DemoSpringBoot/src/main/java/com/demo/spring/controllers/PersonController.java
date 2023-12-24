package com.demo.spring.controllers;

import com.demo.spring.models.Person;
import com.demo.spring.services.ISevice;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("api/v1/person")

public class PersonController {
    @Autowired
    @Qualifier("demoService")
    private ISevice<Person, Integer> _service;

    @GetMapping("/get")
    public ResponseEntity<?> getAllPerson() {
        return ResponseEntity.ok().body(_service.getAll());
    }

    @GetMapping("/get/{id}")
    public ResponseEntity<?> getPerson(@PathVariable Integer id) {
        if (id == null) {
            return ResponseEntity.notFound().build();
        }
        Person person = _service.getPerson(id);
        if (person == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok().body(person);
    }

    @PostMapping("create")
    public ResponseEntity<?> createPerson(@RequestBody Person person) {
        _service.addPerson(person);
        return ResponseEntity.ok().body(person);
    }

    @PutMapping("update")
    public ResponseEntity<?> updatePerson(@RequestBody Person person) {
        _service.updatePerson(person);
        return ResponseEntity.ok().body(person);
    }

    @PostMapping("delete")
    public ResponseEntity<?> deletePerson(@RequestBody Person person) {
        _service.deletePerson(person);
        return ResponseEntity.ok().build();
    }
}
