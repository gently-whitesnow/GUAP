import { createGlobalStyle } from "styled-components";

const GlobalStyles = createGlobalStyle`
@font-face {
  font-family: 'Raleway';
  src: url('./fonts/Raleway-VariableFont_wght.ttf') format('truetype');
  font-weight: normal;
  font-style: normal;
}

  #page {
    
   

  }
  html, body {
    margin:0;
    padding:0;
    height:100vh; 
    font-family: 'Raleway', sans-serif;
    background-color: whitesmoke;

    textarea{
      font-family: 'Raleway', sans-serif;
    }
}


  #root {
    min-height: 1024px;
  }

`;

export default GlobalStyles;
