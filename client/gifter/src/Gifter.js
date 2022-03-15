import React from "react";
import { PostProvider } from "./providers/PostProvider";
import PostList from "./components/PostList";
import { PostForm } from "./components/PostForm";
import { PostSearch } from "./components/PostSearch";

function App() {
  return (
    <div className="App">
      <PostProvider>
        <PostForm />
        <PostSearch />
        <PostList />
      </PostProvider>
    </div>
  );
}

export default App;
