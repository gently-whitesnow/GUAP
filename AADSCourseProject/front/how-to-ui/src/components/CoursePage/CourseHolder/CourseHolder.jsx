import { useStore } from "../../../store";
import CourseButton from "../CourseButton/CourseButton";
import {
  CourseHolderContent,
  CourseHolderWrapper,
  Course,
} from "./CourseHolder.styles";
import { observer } from "mobx-react-lite";

const CourseHolder = (props) => {
  const {  courseStore } = useStore();
  const {
    id,
    articles,
    updateArticle
  } = courseStore;

  let counter = 1;
  return (
    <CourseHolderWrapper>
      <CourseHolderContent>
        {articles?.map((article) => {
          return (
            <CourseButton
              counter={counter++}
              color={props.color}
              article={article}
              updateArticle={updateArticle}
            />
          );
        })}
      </CourseHolderContent>
    </CourseHolderWrapper>
  );
};

export default observer(CourseHolder);
