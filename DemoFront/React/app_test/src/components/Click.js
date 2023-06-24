import { Component } from "react";
class Click extends Component {
  constructor() {
    super();
    this.state = {
      count: 0,
    };
  }
  render() {
    return (
      <div>
        <div>Trang chủ: {this.state.count}</div>
        <button onClick={() => this.click()}>click</button>
      </div>
    );
  }
  click() {
    this.setState({ count: this.state.count + 1 });
  }
}
export default Click;
