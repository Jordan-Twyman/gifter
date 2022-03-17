import React, { useState } from "react";
import { BrowserRouter as Router, Navigate, Route, Routes } from "react-router-dom";
import ApplicationViews from "./components/ApplicationViews";
import { PostProvider } from "./providers/PostProvider";
import Header from "./components/Header";
import { UserProfileProvider } from "./providers/UserProfileProvider";
import { Login } from "./components/Login";
import { Register } from "./components/Register";

function App() {
  const [loggedin, setLoggedin] = useState(false);

  const changeState = (bool) => setLoggedin(bool);

  if (localStorage.getItem("gifterUser")) {
  return (
    <div className="App">
      <Router>
          <Header />
          <ApplicationViews />
      </Router>
    </div>
  );
} else {
  return (
    <UserProfileProvider>
  <Router>
    <Login />
    <Register />
  </Router>
  </UserProfileProvider>
    

  );
}
}

export default App;