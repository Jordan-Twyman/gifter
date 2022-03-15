import React, { useContext, useEffect, useState } from "react";
import { PostContext } from "../providers/PostProvider";
import Post from "./Post";

const PostList = () => {
  const { posts, getAllPosts, searchPosts, searchTerms } = useContext(PostContext);
  const [ filteredPosts, setFiltered ] = useState([])

  useEffect(() => {
    getAllPosts()
  }, []);

  useEffect(() => {
   searchTerms !== "" ? searchPosts(searchTerms) : getAllPosts();
  }, [searchTerms, posts])

  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="cards-column">
          {posts.map((post) => (
            <Post key={post.id} post={post} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default PostList;