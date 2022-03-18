import React, { useContext, useState } from "react";
import { PostContext } from "../providers/PostProvider";
import Post from "./Post";
import { useNavigate } from "react-router-dom";

export const PostForm = () => {
    const {addPost} = useContext(PostContext);
const currentUser = JSON.parse(localStorage.getItem("gifterUser"));
  const currentUserId = currentUser.id
  

    const [post, setPost] = useState({
        userProfileId: currentUserId,
        title:"", 
        imageUrl:"",
        caption:""        
    });

    const navigate = useNavigate();

    const handleControlledInputChange = (e) => {
        const newPost = { ...post }
        newPost[e.target.id] = e.target.value
        setPost(newPost)
    }

    const handleClickSavePost = (e) => {
        e.preventDefault();

        addPost(post).then(() => navigate('/'));

        
        
    }

    return (
        <form className="postForm">
            <h2 className="postForm_title">New Post</h2>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="title">Post Title:</label>
                    <input value={post.title} type="text" id="title" onChange={handleControlledInputChange} required autoFocus className="form-control" placeholder="title"/>
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="caption">Post Caption:</label>
                    <input value={post.caption} type="text" id="caption" onChange={handleControlledInputChange} required  className="form-control" placeholder="caption"/>
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="image">Post Image:</label>
                    <input value={post.imageUrl} type="text" id="imageUrl" onChange={handleControlledInputChange}   className="form-control" placeholder="image"/>
                </div>
            </fieldset>
            <button className="btn btn-primary"
        onClick={handleClickSavePost}>
        Save Post
      </button>
        </form>
    )
}