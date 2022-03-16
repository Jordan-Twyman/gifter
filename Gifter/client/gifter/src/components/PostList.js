import React, { useContext, useEffect, useState } from "react";
import { PostContext } from "../providers/PostProvider";
import Post from "./Post";

const PostList = () => {
  const { posts, getAllPosts, searchPosts, searchTerms } = useContext(PostContext);
  const [ filteredPosts, setFiltered ] = useState([])
  const { setSearchTerms } = useContext(PostContext)

  useEffect(() => {
    getAllPosts()
  }, []);

  useEffect(() => {
   searchTerms !== "" ? searchPosts(searchTerms) : getAllPosts();
  }, [searchTerms, posts])

  return (
    <>
    <big className="postSeach">
      <input type="text"
        className="input--wide"
        onKeyUp={(event) => setSearchTerms(event.target.value)}
        placeholder="Search for a post... " /></big>
    
    <div className="container">
      <div className="row justify-content-center">
        <div className="cards-column">
          {posts.map((post) => (
            <Post key={post.id} post={post} />
          ))}
        </div>
      </div>
    </div>
    </>
  );
};

export default PostList;