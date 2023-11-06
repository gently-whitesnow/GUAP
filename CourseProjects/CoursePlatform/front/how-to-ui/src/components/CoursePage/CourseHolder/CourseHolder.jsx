import { useRef } from "react";
import { useStore } from "../../../store";
import ArticleButton from "../ArticleButton/ArticleButton";
import {
  CourseHolderContent,
  CourseHolderWrapper,
  Course,
} from "./CourseHolder.styles";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";

const CourseHolder = (props) => {
  const newArticleRef = useRef();

  const { courseStore, stateStore } = useStore();
  const { isLoading } = stateStore;
  const { articles, upsertArticle, newArticle, deleteArticle, setNewArticle } =
    courseStore;

  useEffect(() => {
    newArticleRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [newArticle]);

  return (
    <CourseHolderWrapper>
      <CourseHolderContent>
        {articles?.map((article) => {
          return (
            <ArticleButton
              color={props.color}
              article={article}
              upsertArticle={upsertArticle}
              deleteArticle={deleteArticle}
              isLoading={isLoading}
            />
          );
        })}
        {newArticle !== undefined ? (
          <ArticleButton
            innerRef={newArticleRef}
            color={props.color}
            article={newArticle}
            upsertArticle={upsertArticle}
            deleteArticle={() => setNewArticle(undefined)}
            isLoading={isLoading}
          />
        ) : null}
      </CourseHolderContent>
    </CourseHolderWrapper>
  );
};

export default observer(CourseHolder);
