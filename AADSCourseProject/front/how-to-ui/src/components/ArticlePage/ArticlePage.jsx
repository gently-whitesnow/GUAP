import {
  ArticlePageButtonsWrapper,
  ArticlePageContent,
  ArticlePageDecorator,
  ArticlePageWrapper,
} from "./ArticlePage.styles";
import { observer } from "mobx-react-lite";
import theme from "../../theme";
import MarkdownHandler from "./MarkdownHandler/MarkdownHandler";
import Button from "../common/Button/Button";
import OneClickButton from "../common/OneClickButton/OneClickButton";
import { useState } from "react";
import { useStore } from "../../store";
import { useNavigate } from "react-router-dom";


const ArticlePage = () => {
  const [readed, setReaded] = useState(false);

  const { colorStore } = useStore();
  const {
    getColorTheme
  } = colorStore;

  const navigate = useNavigate();
  const onClickHandler = () => {
    navigate("/ast/reabase");
  };

  return (
    <ArticlePageWrapper>
      <ArticlePageContent>
        <ArticlePageDecorator color={getColorTheme()}>
          <MarkdownHandler path="/example.md" color={getColorTheme()} />
        </ArticlePageDecorator>
        <ArticlePageButtonsWrapper>
          <OneClickButton content="Прочитана" onClick={()=>setReaded(!readed)} active={readed} color={getColorTheme()}/>

          <Button content="Следующая статья" onClick={onClickHandler}/>
        </ArticlePageButtonsWrapper>
      </ArticlePageContent>
    </ArticlePageWrapper>
  );
};

export default observer(ArticlePage);
