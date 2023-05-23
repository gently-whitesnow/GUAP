import { observer } from "mobx-react-lite";
import { HomePageWrapper } from "./HomePage.styles";
import Tracker from "./Tracker/Tracker";
import CourseHolder from "./CourseHolder/CourseHolder";
import { useStore } from "../../store";
import { useEffect } from "react";

const HomePage = () => {

  const { summaryStore } = useStore();
  const { getSummaryCourses, setIsLoading } = summaryStore;

  useEffect(() => {
    setIsLoading(true);
    getSummaryCourses();
  }, []);

  return <HomePageWrapper>
    <Tracker/>
    <CourseHolder/>
  </HomePageWrapper>;
};

export default observer(HomePage);
