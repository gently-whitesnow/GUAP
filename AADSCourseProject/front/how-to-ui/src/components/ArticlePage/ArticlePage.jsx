import {
  ArticlePageAuthor,
  ArticlePageButtonsWrapper,
  ArticlePageContent,
  ArticlePageDecorator,
  ArticlePageTitle,
  ArticlePageTitleWrapper,
  ArticlePageWrapper,
} from "./ArticlePage.styles";
import { observer } from "mobx-react-lite";
import theme from "../../theme";
import MarkdownHandler from "./MarkdownHandler/MarkdownHandler";
import Button from "../common/Button/Button";
import OneClickButton from "../common/OneClickButton/OneClickButton";
import { useEffect } from "react";
import { useStore } from "../../store";
import { useParams } from "react-router-dom";

const ArticlePage = () => {
  const { colorStore, articleStore, viewStore, stateStore } = useStore();
  const { currentColorTheme } = colorStore;
  const { isLoading } = stateStore;

  const { courseId, articleId } = useParams();
  const { getArticle, article, setArticleIsViewed } = articleStore;

  const { addApprovedView } = viewStore;

  useEffect(() => {
    getArticle(courseId, articleId);
  }, []);

  const onReadedClickHandler = () => {
    setArticleIsViewed(true);
    addApprovedView(article.courseId, article.id);
  };

  return (
    <ArticlePageWrapper>
      <ArticlePageContent>
        <ArticlePageTitleWrapper>
          <ArticlePageTitle>{article.title}</ArticlePageTitle>
          <ArticlePageAuthor>{article.author?.name}</ArticlePageAuthor>
        </ArticlePageTitleWrapper>
        <ArticlePageDecorator color={currentColorTheme}>
          <MarkdownHandler color={currentColorTheme} />
        </ArticlePageDecorator>
        <ArticlePageButtonsWrapper>
          {article.id != undefined && !isLoading ? (
            <OneClickButton
              content="Прочитана"
              onClick={onReadedClickHandler}
              active={article.isViewed}
              color={currentColorTheme}
            />
          ) : null}
        </ArticlePageButtonsWrapper>
      </ArticlePageContent>
    </ArticlePageWrapper>
  );
};

export default observer(ArticlePage);
