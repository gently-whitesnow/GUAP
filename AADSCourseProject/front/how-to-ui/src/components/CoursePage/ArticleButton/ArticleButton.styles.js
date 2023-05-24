import styled from "styled-components";

export const ArticleButtonWrapper = styled.div`
  display: flex;
  margin-bottom: 15px;
  height: 60px;
  background-color: white;
`;

export const ArticleButtonContent = styled.div`
  margin-right: 2px;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;

  cursor: ${(props) => (props.isArticleEditing ? 'default' : 'pointer')};

  background-color: white;

  transition: 0.1s;
  ${(props) =>
    !props.isArticleEditing &&
    `
    :hover {
      background-color: ${props.color};
      color: white;
      box-shadow: rgba(0, 0, 0, 0.1) 0px 1px 2px 0px;
      transform: scale(1.005);
      div {
        color: white;
      }
      textarea {
        color: white;
      }
    }
  `}
  
`;

export const ArticleTitle = styled.div`
  padding-left: 30px;
  font-size: 20px;
  font-weight: 600;
`;

export const ArticleTag = styled.div`
  font-size: 18px;
  font-weight: 500;
  margin-right: 10px;
`;

export const ArticleToolsWrapper = styled.div`
  display: flex;
  align-items: center;
  height: 100%;
`;

export const IconButtonsWithUploaderWrapper = styled.div`
  display: flex;
`;
