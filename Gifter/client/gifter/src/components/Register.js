import React, { useContext, useEffect, useState } from "react";
import { UserProfileContext } from "../providers/UserProfileProvider";

export const Register = () => {
  // Create state variables for each form field
  const [email, setEmail] = useState("");
  const [name, setName] = useState("");
  const [bio, setBio] = useState("");
  const [imageurl, setImageurl] = useState("");

  // Import the register function from our context-- we'll use this when they click submit
  const { register } = useContext(UserProfileContext);

  // This function will run when the user has finished filling out the form and clicks submit
  const submitLoginForm = (e) => {
    e.preventDefault();
    register({ name, email, bio, imageurl });
  };

  return (
    <>
    <h2>Register</h2>
      <form>
        <input
          type="text"
          onChange={(e) => setEmail(e.target.value)}
          placeholder="email"
        />
        <input
          type="text"
          onChange={(e) => setName(e.target.value)}
          placeholder="name"
        />
        <input
          type="text"
          onChange={(e) => setBio(e.target.value)}
          placeholder="bio"
        />{" "}
        <input
          type="text"
          onChange={(e) => setImageurl(e.target.value)}
          placeholder="imageurl"
        />
        <button type="submit" onClick={submitLoginForm}>
          Register
        </button>
      </form>
    </>
  );
};
