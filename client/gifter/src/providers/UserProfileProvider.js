import React, { useState } from "react";

export const UserProfileContext = React.createContext();

export const UserProfileProvider = (props) => {
  const [userProfiles, setUserProfiles] = useState([]);

  const getAllUserProfiles = () => {
    return fetch("https://localhost:44325/api/UserProfile")
      .then((res) => res.json())
      .then(setUserProfiles);
  };

  return (
    <UserProfileContext.Provider value={{ userProfiles, getAllUserProfiles }}>
      {props.children}
    </UserProfileContext.Provider>
  );
};