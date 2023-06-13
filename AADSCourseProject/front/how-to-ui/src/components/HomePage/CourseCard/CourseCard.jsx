import { observer } from "mobx-react-lite";
import {
  CourseCardWrapper,
  CardTitle,
  CourseCardContent,
} from "./CourseCard.styles";
import { useNavigate } from "react-router";
import ImageDisplay from "../ImageDisplay/ImageDisplay";

const CourseCard = (props) => {
  const navigate = useNavigate();
  const onClickHandler = () => {
    navigate(`/${props.id}`);
  };

  return (
    <CourseCardWrapper  color={props.color}  onClick={onClickHandler}>
      <CourseCardContent>
        <ImageDisplay color={props.color} image={props.image}></ImageDisplay>
        <CardTitle>{props.title}</CardTitle>
      </CourseCardContent>
    </CourseCardWrapper>
  );
};

export default observer(CourseCard);
