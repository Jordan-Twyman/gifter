import React, { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ListGroup } from "reactstrap";
import { PostContext } from "../providers/PostProvider";
import { UserProfileContext } from "../providers/UserProfileProvider";
import Post from "./Post";

const PostList = () => {
//   const { posts, getAllPosts, searchPosts, searchTerms } = useContext(PostContext);
//   const { setSearchTerms } = useContext(PostContext)

//   useEffect(() => {
//     getAllPosts()
//   }, []);

//   useEffect(() => {
//    searchTerms !== "" ? searchPosts(searchTerms) : getAllPosts();
//   })

//   return (
//     <>
//     <big className="postSeach">
//       <input type="text"
//         className="input--wide"
//         onKeyUp={(event) => setSearchTerms(event.target.value)}
//         placeholder="Search for a post... " /></big>
    
//     <div className="container">
//       <div className="row justify-content-center">
//         <div className="cards-column">
//           {posts.map((post) => (
//             <Post key={post.id} post={post} />
//           ))}
//         </div>
//       </div>
//     </div>
//     </>
//   );
// };
const [user, setUser] = useState();
const { getUser } = useContext(UserProfileContext);
const { id } = useParams();

useEffect(() => {
  getUser(id).then(setUser);
}, []);

if (user) {
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
export default PostList;