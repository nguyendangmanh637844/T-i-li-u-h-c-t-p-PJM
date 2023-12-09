package com.example.elk.models;

import lombok.Data;

@Data
public class GetByFieldNameRequest {
    private String fieldName;
    private String value;
    private String index;
}
