import { useStore } from "../../../store";
import ArticleButton from "../ArticleButton/ArticleButton";
import {
  CourseHolderContent,
  CourseHolderWrapper,
  Course,
} from "./CourseHolder.styles";
import { observer } from "mobx-react-lite";

const CourseHolder = (props) => {
  const {  courseStore } = useStore();
  const {
    articles,
    upsertArticle,
    newArticle,
    deleteArticle
  } = courseStore;

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
            />
          );
        })}
        {newArticle!==undefined?<ArticleButton
              color={props.color}
              article={newArticle}
              upsertArticle={upsertArticle}
              deleteArticle={deleteArticle}
            />:null}
      </CourseHolderContent>
    </CourseHolderWrapper>
  );
};

export default observer(CourseHolder);
