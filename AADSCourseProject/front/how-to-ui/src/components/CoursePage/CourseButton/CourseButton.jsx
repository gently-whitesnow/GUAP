import {
  CourseButtonWrapper,
  CourseTag,
  CourseTitle,
} from "./CourseButton.styles";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router";

const CourseButton = (props) => {
  const navigate = useNavigate();
  const onClickHandler = () => {
    navigate("/ast/reabase");
  };
  return (
    <CourseButtonWrapper onClick={onClickHandler} color={props.color}>
      <CourseTitle>{props.counter} - Статья про то про это </CourseTitle>
      {props.active ? <CourseTag className="course-tag">Прочитана</CourseTag> : null}
    </CourseButtonWrapper>
  );
};

export default observer(CourseButton);
