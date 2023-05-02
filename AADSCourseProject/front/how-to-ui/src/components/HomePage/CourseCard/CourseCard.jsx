import { observer } from "mobx-react-lite";
import {
  CourseCardWrapper,
  CardTitle,
  CardImage,
  CourseCardContent,
} from "./CourseCard.styles";
import { useNavigate } from "react-router";

const CourseCard = (props) => {
  const navigate = useNavigate();
  const onClickHandler = () => {
    navigate("/ast");
  };

  return (
    <CourseCardWrapper  color={props.color}  onClick={onClickHandler}>
      <CourseCardContent>
        <CardImage  color={props.color}  className="card-image"></CardImage>
        <CardTitle>{props.title?props.title:"C++ для сишарпников"}</CardTitle>
      </CourseCardContent>
    </CourseCardWrapper>
  );
};

export default observer(CourseCard);
