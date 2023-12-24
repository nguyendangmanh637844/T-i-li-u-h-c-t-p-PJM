import React, { useState, useEffect } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

const MessageInput = () => {
  const [connection, setConnection] = useState(null);
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);
  const CHANNEL = {
    Channel1: 0,
    Channel2: 1,
  };
  const CHANNEL_NAME = {
    Channel1: "CHANNEL_1",
    Channel2: "CHANNEL_2",
  };
  useEffect(() => {
    // Tạo kết nối khi component được mount
    const newConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5229/demo-hub") // Điều chỉnh URL hub tương ứng với server của bạn
      .withAutomaticReconnect()
      .build();
    // Lắng nghe sự kiện từ hub
    //lắng nghe từ Channel1
    newConnection.on(CHANNEL_NAME.Channel1, (user, message) => {
      setMessages((prevMessages) => [...prevMessages, `(${CHANNEL_NAME.Channel1}) ${user}: ${message}`]);
    });
    //lắng nghe từ Channel2
    newConnection.on(CHANNEL_NAME.Channel2, (user, message) => {
      setMessages((prevMessages) => [...prevMessages, `(${CHANNEL_NAME.Channel2}) ${user}: ${message}`]);
    });

    setConnection(newConnection);

    return () => {
      // Đóng kết nối khi component bị unmount
      if (newConnection.state === "Connected") {
        newConnection.stop();
      }
    };
  }, []);

  useEffect(() => {
    // Kết nối tới hub khi kết nối được thiết lập
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("Connected to the hub");
        })
        .catch((error) => {
          console.error("Error connecting to the hub:", error);
        });
    }
  }, [connection]);

  const handleInputChange = (e) => {
    setMessage(e.target.value);
  };

  const sendMessage = () => {
    // Kiểm tra xem tin nhắn có giá trị hay không
    if (message.trim() !== "" && connection) {
      const SEND_MODE = {
        SendToAll: 0,
        SendToCaller: 1,
        SendToOther: 2,
      };
      const functionInHub = "SendMessage";
      const paramsToInvokeFuntion = {
        User: "React",
        Message: message,
        SendModes: SEND_MODE.SendToAll,
        Channels: CHANNEL.Channel1,
      };
      // Gửi tin nhắn lên server thông qua hub
      connection.invoke(functionInHub, paramsToInvokeFuntion);

      // Xóa nội dung của ô văn bản sau khi gửi
      setMessage("");
    }
  };

  return (
    <div>
      <input
        type="text"
        value={message}
        onChange={handleInputChange}
        placeholder="Nhập tin nhắn..."
      />
      <button onClick={sendMessage}>Gửi</button>
      <div>
        <ul>
          {messages.map((msg, index) => (
            <li key={index}>{msg}</li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default MessageInput;
