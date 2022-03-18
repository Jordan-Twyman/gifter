import React, { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { UserProfileContext, UserProfileProvider } from "../providers/UserProfileProvider";

const Header = () => {

  return (
    
    <nav className="navbar navbar-expand navbar-dark bg-info">
      <Link to="/" className="navbar-brand">
        GiFTER
      </Link>

      <ul className="navbar-nav mr-auto">
      
        <li className="nav-item">
          <Link to="/" className="nav-link">
            Feed
          </Link>
        </li>
        <li className="nav-item">
          <Link to="/posts/add" className="nav-link">
            New Post
          </Link>
        </li>
      
          
      </ul>
    </nav>

  );
};

export default Header;