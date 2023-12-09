package com.example.elk.entities;

import lombok.*;

import java.util.List;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class Source {
    private String timestamp;
    private String version;
    private String loggerName;
    private String message;
    private String thread;
    @Singular
    private List<Object> tags;
    private String type;
    private float level;
}
