import styled from "styled-components";

export const ArticlePageWrapper = styled.div`
  background-color: white;
  display: flex;
  justify-content: center;
`;

export const ArticlePageContent = styled.div`
  margin-top: 30px;
  max-width: 800px;
  padding: 20px;
  margin-bottom: 200px;
  font-size: 18px;
`;

export const ArticlePageButtonsWrapper = styled.div`
  margin-top: 100px;
  display: flex;
  justify-content: start;
`;

export const ArticlePageDecorator = styled.div`
  h1 {
    color: ${(props) => props.color};
  }
  h2 {
    color: ${(props) => props.color};
  }
  h3 {
    color: ${(props) => props.color};
  }
  h4 {
    color: ${(props) => props.color};
  }
  h5 {
    color: ${(props) => props.color};
  }
  h6 {
    color: ${(props) => props.color};
  }

  strong {
    box-shadow: 0px 3px ${(props) => props.color};
  }
  a {
    color: ${(props) => props.color};
  }
  img {
    max-width: 640px;
  }

  td {
    border-top: 1px solid #ddd;
    background-color: #f9f9f9;
  }
  pre {
    display: block;
    padding: 9.5px;
    margin: 0 0 10px;
    font-size: 13px;
    line-height: 1.42857143;
    color: #333;
    word-break: break-all;
    word-wrap: break-word;
    background-color: #f5f5f5;
    border: 1px solid #ccc;
  }
  .language-js {
    display: block;
    overflow-x: auto;
    padding: 0.5em;
    color: #333;
    background: #f8f8f8;
  }
`;
