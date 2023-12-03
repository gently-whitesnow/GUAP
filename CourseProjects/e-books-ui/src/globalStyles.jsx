import { createGlobalStyle } from "styled-components";

const GlobalStyles = createGlobalStyle`

  #page {
    
  }
  html, body {
    margin:0;
    padding:0;
    height:100vh; 

    font-family: Inter, system-ui, Avenir, Helvetica, Arial, sans-serif;
    line-height: 1.5;
    font-weight: 400;

    font-synthesis: none;
    text-rendering: optimizeLegibility;

    background-color: #f4f7fb;
}

  #root {
    min-height: 1024px;
  }

`;

export default GlobalStyles;
