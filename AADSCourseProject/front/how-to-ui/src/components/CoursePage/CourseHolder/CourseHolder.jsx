import CourseButton from "../CourseButton/CourseButton";
import {
  CourseHolderContent,
  CourseHolderWrapper,
  Course,
} from "./CourseHolder.styles";
import { observer } from "mobx-react-lite";

const CourseHolder = (props) => {
  let counter = 1;
  return (
    <CourseHolderWrapper>
      <CourseHolderContent>
      <CourseButton counter={counter++} active={true} color={props.color}/>
        {[1, 2, 3, 4, 5, 6, 7, 8, 9].map((answer, i) => {
          return <CourseButton counter={counter++} color={props.color} />;
        })}
      </CourseHolderContent>
    </CourseHolderWrapper>
  );
};

export default observer(CourseHolder);
