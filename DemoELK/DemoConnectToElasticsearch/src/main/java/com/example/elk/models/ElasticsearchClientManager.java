package com.example.elk.models;

import org.apache.http.HttpHost;
import org.apache.http.auth.AuthScope;
import org.apache.http.auth.UsernamePasswordCredentials;
import org.apache.http.impl.client.BasicCredentialsProvider;
import org.elasticsearch.client.RestClient;
import org.elasticsearch.client.RestHighLevelClient;

public class ElasticsearchClientManager {

    private static final String ELASTICSEARCH_HOST = "localhost";
    private static final int ELASTICSEARCH_PORT = 9200;
    private static final String ELASTICSEARCH_SCHEME = "http";  // Use "https" if your Elasticsearch is secured with HTTPS
    private static final String ELASTICSEARCH_USERNAME = "elastic";
    private static final String ELASTICSEARCH_PASSWORD = "w2IFvLrZAkRmwlDQoTmB";

    private static RestHighLevelClient restHighLevelClient;

    public static RestHighLevelClient getClient() {
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

    public static void closeClient() {
        if (restHighLevelClient != null) {
            try {
                restHighLevelClient.close();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }
}
