import React from "react";
import { PostProvider } from "./providers/PostProvider";
import PostList from "./components/PostList";
import { PostForm } from "./components/PostForm";

function App() {
  return (
    <div className="App">
      <PostProvider>
        <PostForm />
        <PostList />
      </PostProvider>
    </div>
  );
}

export default App;
