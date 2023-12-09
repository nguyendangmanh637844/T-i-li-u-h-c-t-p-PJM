package com.example.elk.models;

import org.apache.http.HttpHost;
import org.apache.http.auth.AuthScope;
import org.apache.http.auth.UsernamePasswordCredentials;
import org.apache.http.impl.client.BasicCredentialsProvider;
import org.elasticsearch.client.RestClient;
import org.elasticsearch.client.RestHighLevelClient;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

@Component
public class ElasticsearchClientManager {
    @Value("${elasticsearch.host}")
    private String ELASTICSEARCH_HOST;
    @Value("${elasticsearch.port}")
    private int ELASTICSEARCH_PORT;
    @Value("${elasticsearch.scheme}")
    private String ELASTICSEARCH_SCHEME;
    @Value("${elasticsearch.username}")
    private String ELASTICSEARCH_USERNAME;
    @Value("${elasticsearch.password}")
    private String ELASTICSEARCH_PASSWORD;
    private RestHighLevelClient restHighLevelClient;

    public RestHighLevelClient getClient() {
        if (restHighLevelClient == null) {
            restHighLevelClient = new RestHighLevelClient(
                    RestClient.builder(new HttpHost(ELASTICSEARCH_HOST, ELASTICSEARCH_PORT, ELASTICSEARCH_SCHEME))
                            .setHttpClientConfigCallback(httpClientBuilder -> httpClientBuilder
                                    .setDefaultCredentialsProvider(new BasicCredentialsProvider() {{
                                        setCredentials(AuthScope.ANY, new UsernamePasswordCredentials(ELASTICSEARCH_USERNAME, ELASTICSEARCH_PASSWORD));
                                    }})
                            )
            );
        }
        return restHighLevelClient;
    }

    public void closeClient() {
        if (restHighLevelClient != null) {
            try {
                restHighLevelClient.close();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }
}
