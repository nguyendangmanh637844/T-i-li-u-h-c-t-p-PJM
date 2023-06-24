import React from 'react';
import { Link } from 'react-router-dom';

function Navbar() {
  return (
    <nav>
      <ul>
        <li>
          <Link to="/">Trang chủ</Link>
        </li>
        <li>
          <Link to="/about">Giới thiệu</Link>
        </li>
        <li>
          <Link to="/contact">Liên hệ</Link>
        </li>
      </ul>
    </nav>
  );
}

export default Navbar;
