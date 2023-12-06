package com.example.elk.repositories;

import com.example.elk.entities.Document;
import com.example.elk.models.ElasticsearchClientManager;
import lombok.extern.slf4j.Slf4j;
import org.elasticsearch.action.search.SearchRequest;
import org.elasticsearch.action.search.SearchResponse;
import org.elasticsearch.client.RequestOptions;
import org.elasticsearch.client.RestHighLevelClient;
import org.elasticsearch.core.TimeValue;
import org.elasticsearch.index.query.QueryBuilders;
import org.elasticsearch.search.builder.SearchSourceBuilder;
import org.elasticsearch.search.fetch.subphase.highlight.HighlightBuilder;
import org.elasticsearch.search.sort.SortBuilders;
import org.elasticsearch.search.sort.SortOrder;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
@Slf4j
public class ElasticsearchTemplate implements IElasticSearchTemplate {
    private final RestHighLevelClient client = ElasticsearchClientManager.getClient();
    @Override
    public SearchResponse searchByField(String fieldName, String value, String index) {
        try {
            SearchRequest searchRequest = new SearchRequest(index); // Thay "your_index" bằng tên index thực tế
            SearchSourceBuilder sourceBuilder = new SearchSourceBuilder();
            sourceBuilder.query(QueryBuilders.matchQuery(fieldName, value)); // Tìm kiếm tất cả các documents

            searchRequest.source(sourceBuilder);
            SearchResponse searchResponse = client.search(searchRequest, RequestOptions.DEFAULT);
            return searchResponse;
        } catch (Exception e) {
            log.error(e.getMessage());
            return null; // Trả về danh sách trống hoặc xử lý theo yêu cầu của bạn
        }
    }

    @Override
    public SearchResponse searchByCustomQuery(String value, String index) {
        SearchRequest searchRequest = new SearchRequest(index);
        SearchSourceBuilder searchSourceBuilder = new SearchSourceBuilder();

        // Thêm logic xử lý cho truy vấn tùy chỉnh
        // Ở đây sử dụng QueryBuilders để xây dựng truy vấn
        searchSourceBuilder.query(QueryBuilders.matchQuery("id", value));

        // Tùy chọn thêm: sắp xếp kết quả theo một trường cụ thể
        searchSourceBuilder.sort(SortBuilders.fieldSort("id").order(SortOrder.ASC));

        // Tùy chọn thêm: đánh dấu các từ khóa trong kết quả
        HighlightBuilder highlightBuilder = new HighlightBuilder();
        highlightBuilder.field("id");
        searchSourceBuilder.highlighter(highlightBuilder);

        // Tùy chọn thêm: giới hạn thời gian tối đa cho truy vấn
        searchSourceBuilder.timeout(TimeValue.timeValueSeconds(5));

        searchRequest.source(searchSourceBuilder);

        try {
            SearchResponse searchResponse = client.search(searchRequest, RequestOptions.DEFAULT);
            return searchResponse;
        } catch (Exception e) {
            // Xử lý exception nếu có
            log.atError().log(e.getMessage());
            return null;
        }
    }
}
