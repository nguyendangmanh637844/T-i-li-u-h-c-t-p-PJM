package com.demo.mongodb.repositories;

import com.demo.mongodb.entities.User;
import org.springframework.data.mongodb.repository.MongoRepository;

import java.util.List;

public interface UserRepository extends MongoRepository<User, String> {
}
