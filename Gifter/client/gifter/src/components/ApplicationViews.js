import React, { useContext } from "react";
import {
    BrowserRouter as Router,
    Route,
    Routes,
    useNavigate
  } from "react-router-dom";
  import PostList from "./PostList";
import { PostForm } from "./PostForm";
import PostDetails from "./PostDetails";
import UserPosts from "./UserPosts";
import { PostProvider } from "../providers/PostProvider";
import { UserProfileContext, UserProfileProvider } from "../providers/UserProfileProvider";
import { Logout } from "./Logout";

const ApplicationViews = () => {


  return (
    <UserProfileProvider>
    <PostProvider>
    <Routes>
       

      <Route path="/" element={<><PostList /> <Logout /></>} />
      

      <Route path="/posts/add" element={<PostForm />} />
        
      

      <Route path="/posts/:id" element={<PostDetails />} />

      <Route path="/users/:id" element={<UserPosts />} />

      

    </Routes>
     </PostProvider>
     </UserProfileProvider>
  );
};

export default ApplicationViews;