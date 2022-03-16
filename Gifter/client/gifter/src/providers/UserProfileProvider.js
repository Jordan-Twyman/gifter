import React, { useState } from "react";

export const UserProfileContext = React.createContext();

export const UserProfileProvider = (props) => {
  const [userProfiles, setUserProfiles] = useState([]);

  const getAllUserProfiles = () => {
    return fetch("https://localhost:44325/api/UserProfile")
      .then((res) => res.json())
      .then(setUserProfiles);
  };

  const getUser = (id) => {
    return fetch(`https://localhost:44325/GetUserProfileWithPosts/${id}`).then((res) => res.json());
};

  return (
    <UserProfileContext.Provider value={{ userProfiles, getAllUserProfiles, getUser }}>
      {props.children}
    </UserProfileContext.Provider>
  );
};