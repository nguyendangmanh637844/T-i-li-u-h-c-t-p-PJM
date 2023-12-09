package com.example.elk.entities;

import lombok.*;
import org.springframework.data.annotation.Id;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@org.springframework.data.elasticsearch.annotations.Document(indexName = "java")
public class Document {
    private String _index;
    @Id
    private String _id;
    private float _score;
    private Source _sourceObject;
}
