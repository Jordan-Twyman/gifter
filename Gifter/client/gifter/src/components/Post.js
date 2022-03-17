import React, { useContext, useEffect } from "react";
import { Card, CardImg, CardBody } from "reactstrap";
import { Link, useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { PostContext } from "../providers/PostProvider";
import { UserProfileContext } from "../providers/UserProfileProvider";

const Post = ({ post }) => {

 

  // useEffect(() => {
  //   console.log("useEffect", postId)
  //   getUser(postId)
  //   }, [])

 

  return (
    <Card className="m-4">
      
      <p className="text-left px-2">Posted by: <Link to={`/users/${post.userProfileId}`}>{post.userProfile.name}</Link></p>
      
      <CardImg top src={post.imageUrl} alt={post.title} />
      <CardBody>
        <p>
        <Link to={`/posts/${post.id}`}>
        <strong>{post.title}</strong>
       </Link>
        </p>
        <p>{post.caption}</p>
        <ul>
        {post.comments.map(pc => <li key={pc.id}> {pc.userProfile.name}: {pc.message}  </li>)}
        </ul>
        
      {/* <button onClick={() => {
    navigate(`/posts/edit/${post.id}`)
}}>Edit</button> */}
      </CardBody>
    </Card>
  );
};

export default Post;