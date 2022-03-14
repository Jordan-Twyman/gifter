import React, { useState } from "react";

export const PostContext = React.createContext();

export const PostProvider = (props) => {
  const [posts, setPosts] = useState([]);

  const getAllPosts = () => {
    return fetch("https://localhost:44325/api/Post")
      .then((res) => res.json())
      .then(setPosts);
  };

  const addPost = (post) => {
    return fetch("https://localhost:44325/api/Post", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(post),
    })
    .then(response => response.json())
  };

  return (
    <PostContext.Provider value={{ posts, getAllPosts, addPost }}>
      {props.children}
    </PostContext.Provider>
  );
};