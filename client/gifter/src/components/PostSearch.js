import React, { useContext } from "react"
import { PostContext } from "../providers/PostProvider"

export const PostSearch = () => {
  const { setSearchTerms } = useContext(PostContext)

  return (
    <>
    <big className="postSeach">
      <input type="text"
        className="input--wide"
        onKeyUp={(event) => setSearchTerms(event.target.value)}
        placeholder="Search for a post... " /></big>
    </>
  )
}