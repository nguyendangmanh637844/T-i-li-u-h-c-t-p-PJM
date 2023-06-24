import { createStore } from "redux";
//=========Khởi tạo cửa hàng
//Bước 3: tạo ra mặc định cho kiện hàng
const initialState = { name: "test", age: 20 };
// bước 2: tạo nhân viên: nhân viên cần có các kiện hàng (state) để gia công và các yêu cầu (action) với kiện hàng đó.
// Dựa vào các yêu cầu nhân viên sẽ đưa ra các hành động tương ứng
const reducer = (state = initialState, action) => {
  if (action.type === "reName") {
    return { ...state, name: action.newName };
  }
  return state;
};
//Buowcs1: tạo store, trong store cần có nhân viên
const store = createStore(reducer);

//====người dùng đưa ra các yêu cầu cho cửa hàng
//Bước 1: khởi tạo yêu cầu
var action = { type: "reName", newName: "Mạnh1" };
//Buowcs2: gửi yêu cầu đến cửa hàng
store.dispatch(action);
//Bước 3; lấy ra state
const { name } = store.getState();
console.log("🚀 ~ file: redux.js:23 ~ name:", name);
