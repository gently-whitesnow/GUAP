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
import { useEffect } from "react";
import { useStore } from "../../store";
import { useParams } from "react-router-dom";

const ArticlePage = () => {
  const { colorStore, articleStore, viewStore } = useStore();
  const { getColorTheme } = colorStore;

  const { courseId, articleId } = useParams();
  const { setIsLoading, getArticle, article, setArticleIsViewed } =
    articleStore;

  const { addApprovedView } = viewStore;

  useEffect(() => {
    setIsLoading(true);
    getArticle(courseId, articleId);
  }, []);

  const onReadedClickHandler = () => {
    setArticleIsViewed(true);
    addApprovedView(article.courseId, article.id);
  };

  return (
    <ArticlePageWrapper>
      <ArticlePageContent>
        <ArticlePageDecorator color={getColorTheme()}>
          <MarkdownHandler color={getColorTheme()} />
        </ArticlePageDecorator>
        <ArticlePageButtonsWrapper>
          {article != undefined ? (
            <OneClickButton
              content="Прочитана"
              onClick={onReadedClickHandler}
              active={article.isViewed}
              color={getColorTheme()}
            />
          ) : null}
        </ArticlePageButtonsWrapper>
      </ArticlePageContent>
    </ArticlePageWrapper>
  );
};

export default observer(ArticlePage);
