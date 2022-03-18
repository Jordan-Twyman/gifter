import React, { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ListGroup } from "reactstrap";
import { PostContext } from "../providers/PostProvider";
import { UserProfileContext } from "../providers/UserProfileProvider";
import Post from "./Post";

const PostList = () => {
  const { posts, getAllPosts, searchPosts, searchTerms } = useContext(PostContext);
  const { getUser } = useContext(UserProfileContext);
  const [user, setUser] = useState();
  const { id } = useParams();
  const { setSearchTerms } = useContext(PostContext);
  const currentUser = JSON.parse(localStorage.getItem("gifterUser"));
  const currentUserId = currentUser.id

  useEffect(() => {
    getUser(currentUserId).then(setUser)
  }, []);

  // useEffect(() => {
  //  searchTerms !== "" ? searchPosts(searchTerms) : getAllPosts();
  // })
  
  if (!user) {
    return "";
  }
  return (
    <>
    {/* <big className="postSeach">
      <input type="text"
        className="input--wide"
        onKeyUp={(event) => setSearchTerms(event.target.value)}
        placeholder="Search for a post... " /></big> */}
    
    <div className="container">
      <div className="row justify-content-center">
        <div className="col-sm-12 col-lg-6">
          <ListGroup>
            {user.posts?.map((p) => (
              <Post key={p.id} post={p} />
            ))}
          </ListGroup>
        </div>
      </div>
    </div>
    </>
  );
};
// const [user, setUser] = useState();
// const { getUser } = useContext(UserProfileContext);
// 

// useEffect(() => {
//   getUser(id).then(setUser);
// }, []);

// if (user) {
//   return null;
// }

// return (
//   <div className="container">
//     <div className="row justify-content-center">
//       <div className="col-sm-12 col-lg-6">
//         <ListGroup>
//           {user.posts.map((p) => (
//             <Post key={p.id} post={p} />
//           ))}
//         </ListGroup>
//       </div>
//     </div>
//   </div>
// );
// };
export default PostList;