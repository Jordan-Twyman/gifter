import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { PostContext } from "../providers/PostProvider";

export const PostForm = () => {
    const {addPost} = useContext(PostContext);

    let currentTimestamp = Date.now();

    let date = new Intl.DateTimeFormat('en-US', { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', second: '2-digit' }).format(currentTimestamp)

    const [post, setPost] = useState({
        userProfileId: 1,
        title:"", 
        imageUrl:"",
        caption:"",
        dateCreated: date
        });

        

    const handleControlledInputChange = (e) => {
        const newPost = { ...post }
        newPost[e.target.id] = e.target.value
        setPost(newPost)
    }

    const handleClickSavePost = (e) => {
        e.preventDefault();

        addPost(post)
        
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