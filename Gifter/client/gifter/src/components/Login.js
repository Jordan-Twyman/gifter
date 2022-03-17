import React, { useContext, useEffect, useState } from "react";
import { UserProfileContext } from "../providers/UserProfileProvider";

export const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { login } = useContext(UserProfileContext);

  const submitLoginForm = (e) => {
    e.preventDefault();
    login({ email, password });
  };

  return (
    <>
      <>
        <h2>Log In</h2>
        <form>
          <input
            type="text"
            onChange={(e) => setEmail(e.target.value)}
            name="email"
          />
          <input
            type="password"
            onChange={(e) => setPassword(e.target.value)}
            name="password"
          />
          <button type="submit" onClick={submitLoginForm}>
            Log In
          </button>
        </form>
      </>
    </>
  );
};
