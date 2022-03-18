import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { UserProfileContext } from "../providers/UserProfileProvider";


export const Logout = () => {
const { logout } = useContext(UserProfileContext)

  const submitLogout = (e) => {
    e.preventDefault();
    logout();
    window.location.reload(false);
  };


  return (
    <button type="submit" onClick={submitLogout}>
    Logout
  </button>);
  }