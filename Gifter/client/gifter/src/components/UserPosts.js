import React, { useEffect, useContext, useState } from "react";
import { ListGroup, ListGroupItem } from "reactstrap";
import { UserProfileContext } from "../providers/UserProfileProvider";
import { useParams } from "react-router-dom";
import Post from "./Post";
import PostDetails from "./PostDetails";
import { PostContext } from "../providers/PostProvider";

const UserPosts = () => {
  const [user, setUser] = useState();
  const { getUser } = useContext(UserProfileContext);
  const { id } = useParams();

  useEffect(() => {
    getUser(id).then(setUser);
  }, []);

  if (!user) {
    return null;
  }

  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="col-sm-12 col-lg-6">
          <ListGroup>
            {user.posts.map((p) => (
              <Post key={p.id} post={p} />
            ))}
          </ListGroup>
        </div>
      </div>
    </div>
  );
};

export default UserPosts;