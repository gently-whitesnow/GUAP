import React from "react";
import { ThemeProvider } from "styled-components";
import GlobalStyles from "./globalStyles.jsx";
import Content from "./components/Content";
import theme from "./theme";

const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <GlobalStyles />
      <Content />
    </ThemeProvider>
  );
};

export default App;

