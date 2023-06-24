import { Component } from "react";
class Message extends Component {
  render() {
    return <div>This is a Message {this.props.name} - {this.props.age}</div>;
  }
}
export default Message;
