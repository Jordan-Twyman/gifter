import React from "react";
import { Card, CardImg, CardBody } from "reactstrap";

const Post = ({ post }) => {
  return (
    <Card className="m-4">
      <p className="text-left px-2">Posted by: {post.userProfile.name}</p>
      <CardImg top src={post.imageUrl} alt={post.title} />
      <CardBody>
        <p>
          <strong>{post.title}</strong>
        </p>
        <p>{post.caption}</p>
        <ul>
        {post.comments.map(pc => <li key={pc.id}> {pc.userProfile.name}: {pc.message}  </li>)}
        </ul>
      </CardBody>
    </Card>
  );
};

export default Post;