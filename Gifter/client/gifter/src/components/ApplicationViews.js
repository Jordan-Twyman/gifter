import React from "react";
import {
    BrowserRouter as Router,
    Route,
    Routes
  } from "react-router-dom";
  import PostList from "./PostList";
import { PostForm } from "./PostForm";
import PostDetails from "./PostDetails";
import UserPosts from "./UserPosts";

const ApplicationViews = () => {
  return (
    <Routes>


      <Route path="/" element={<PostList />} />
      

      <Route path="/posts/add" element={<PostForm />} />
        
      

      <Route path="/posts/:id" element={<PostDetails />} />

      <Route path="/users/:id" element={<UserPosts />} />

    </Routes>
  );
};

export default ApplicationViews;