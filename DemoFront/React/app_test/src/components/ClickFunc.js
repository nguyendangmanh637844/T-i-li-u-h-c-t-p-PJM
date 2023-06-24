import { useState } from "react";
function ClickFunc() {
  const [count, setCouter] = useState(0);
  const increase = () => {
    setCouter(count + 1);
  };
  return (
    <div>
      <div>
        Liên hệ: {count} 
      </div>
      <button onClick={increase}>Click</button>
    </div>
  );
}

export default ClickFunc;
