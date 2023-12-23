package com.demo.websocket.websockethandlers;

import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;
import org.springframework.web.socket.handler.TextWebSocketHandler;

public class MyWebSocketHandler extends TextWebSocketHandler {
    //    TextWebSocketHandler là một lớp cơ sở mà bạn có thể mở rộng để xử lý tin nhắn văn bản trên WebSocket.
//    Phương thức quan trọng để thực hiện là handleTextMessage.
    @Override
    public void handleTextMessage(WebSocketSession session, TextMessage message) throws Exception {
        // Xử lý tin nhắn từ client
        String payload = message.getPayload();
        // Thực hiện xử lý tùy ý

        // Gửi tin nhắn về client
        session.sendMessage(new TextMessage("Server eceived: " + payload));
    }
}
//public class MyBinaryWebSocketHandler extends BinaryWebSocketHandler {
//BinaryWebSocketHandler được sử dụng để xử lý tin nhắn nhị phân.
//    @Override
//    protected void handleBinaryMessage(WebSocketSession session, BinaryMessage message) throws Exception {
//        // Xử lý tin nhắn nhị phân từ client
//        byte[] payload = message.getPayload().array();
//        // Thực hiện xử lý tùy ý
//        // Gửi phản hồi về client nếu cần
//        session.sendMessage(new BinaryMessage(payload));
//    }
//}


//public class MyWebSocketHandler extends AbstractWebSocketHandler {
//AbstractWebSocketHandler là một lớp trừu tượng cung cấp một loạt các phương thức rỗng để bạn có thể override theo nhu cầu.
//    @Override
//    protected void handleTextMessage(WebSocketSession session, TextMessage message) throws Exception {
//        // Xử lý tin nhắn văn bản từ client
//    }
//
//    @Override
//    protected void handleBinaryMessage(WebSocketSession session, BinaryMessage message) throws Exception {
//        // Xử lý tin nhắn nhị phân từ client
//    }
//}
