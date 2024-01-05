import React from "react";
import ReactDOM from "react-dom/client";
import Root from "./components/Root/Root.jsx";
import GlobalStyles from "./globalStyles.jsx";

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <GlobalStyles />
    <Root />
  </React.StrictMode>
);
