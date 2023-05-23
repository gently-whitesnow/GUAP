import React from "react";
import { useNavigate } from "react-router-dom";

export const NavigateToAuthorize = () => {
  const navigate = useNavigate();
  navigate("/auth");
};

export const NavigateToHome = () => {
  console.log("NavigateToHome");
  const navigate = useNavigate();
  navigate("/asda");
};

