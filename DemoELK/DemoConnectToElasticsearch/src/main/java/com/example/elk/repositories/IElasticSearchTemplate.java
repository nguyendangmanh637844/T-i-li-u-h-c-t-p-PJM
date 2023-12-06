package com.example.elk.repositories;

import com.example.elk.entities.Document;
import org.elasticsearch.action.search.SearchResponse;
import org.springframework.stereotype.Repository;

import java.util.List;
@Repository
public interface IElasticSearchTemplate {
    SearchResponse searchByField(String fieldName, String value, String index);
    SearchResponse searchByCustomQuery(String queryString, String index);
}
