import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Click from "./components/Click";
import ClickFunc from "./components/ClickFunc";
import ClassComponet from './components/ClassComponent';
import ClickRedux from "./components/clickRedux";
import Navbar from "./navbar";
function App() {
  return (
    <div className="App">
      <Router>
        <Navbar />
        <Routes>
        <Route path="/" element={<ClickFunc />} />
        <Route path="/about" element={<ClickRedux />} />
        <Route path="/contact" element={<ClassComponet />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
