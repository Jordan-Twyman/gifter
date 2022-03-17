import React, { useState } from "react";

export const UserProfileContext = React.createContext();

export const UserProfileProvider = (props) => {
  const [userProfiles, setUserProfiles] = useState([]);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const getCurrentUser = () => {
    const currentUser = localStorage.getItem("gifterUser");

    return currentUser;
  };
  

  const getAllUserProfiles = () => {
    return fetch("https://localhost:44325/api/UserProfile")
      .then((res) => res.json())
      .then(setUserProfiles);
  };

  const getUser = (id) => {
    return fetch(`https://localhost:44325/GetUserProfileWithPosts/${id}`).then((res) => res.json());
};
const login = (userObject) => {
  
  fetch(`https://localhost:44325/GetByEmail/${userObject.email}`)
    .then((r) => r.json())
    .then((userObjFromDB) => {

      localStorage.setItem("gifterUser", JSON.stringify(userObjFromDB));
      setIsLoggedIn(true)
      ;
    })
};

const register = (userObject) => {
  fetch("https://localhost:44325/Register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(userObject),
  })
    .then((response) => response.json())
    .then((userObject) => {
      localStorage.setItem("gifterUser", JSON.stringify(userObject));
    });
};

const logout = () => {
  localStorage.clear();
  setIsLoggedIn(false);
};

  return (
    <UserProfileContext.Provider value={{ userProfiles,register, login, logout, getAllUserProfiles, getUser }}>
      {props.children}
    </UserProfileContext.Provider>
  );
};