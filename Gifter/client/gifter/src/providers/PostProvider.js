import React, { useState } from "react";

export const PostContext = React.createContext();

export const PostProvider = (props) => {
  const [posts, setPosts] = useState([]);
  const [ searchTerms, setSearchTerms ] = useState("");
  

  const getAllPosts = () => {
    return fetch("https://localhost:44325/api/Post/GetWithComments")
      .then((res) => res.json())
      .then(setPosts);
  };

  const getPost = (id) => {
    return fetch(`https://localhost:44325/GetPostIdWithComments/${id}`).then((res) => res.json());
};

  const searchPosts = () => {
    return fetch(`https://localhost:44325/api/Post/search?q=${searchTerms}&sortDesc=true`)
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
    .then(getAllPosts)
  };

  const deletePost = (post) => {
    return fetch(`https://localhost:44325/api/post/${post.id}`, {
        method: "DELETE"
    })
        
  }

  return (
    <PostContext.Provider value={{ posts, getPost, deletePost, getAllPosts, addPost, searchTerms, setSearchTerms, searchPosts }}>
      {props.children}
    </PostContext.Provider>
  );
};