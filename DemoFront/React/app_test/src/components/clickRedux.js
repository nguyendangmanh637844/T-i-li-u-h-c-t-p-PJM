import { increase } from "../actions/click";
import { useDispatch, useSelector } from "react-redux";

function ClickRedux() {
  const count = useSelector((state) => state.count);
  const dispatch = useDispatch();
  const clickIncrease = () => {
    dispatch(increase());
  };
  return (
    <div>
      <h1>Click Redux: {count}</h1>
      <button onClick={clickIncrease}>Click me</button>
    </div>
  );
}
export default ClickRedux;
