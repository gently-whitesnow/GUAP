import { useNavigate } from "react-router-dom";

export const NavigateToAuthorize = () => {

  const navigate = useNavigate();
  navigate("/auth");
}