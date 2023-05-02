import { observer } from "mobx-react-lite";
import { HomePageWrapper } from "./HomePage.styles";
import Tracker from "./Tracker/Tracker";
import CourseHolder from "./CourseHolder/CourseHolder";

const HomePage = () => {
  return <HomePageWrapper>
    <Tracker/>
    <CourseHolder/>
  </HomePageWrapper>;
};

export default observer(HomePage);
