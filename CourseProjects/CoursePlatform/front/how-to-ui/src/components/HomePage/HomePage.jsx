import { observer } from "mobx-react-lite";
import { HomePageWrapper } from "./HomePage.styles";
import Tracker from "./Tracker/Tracker";
import CourseHolder from "./CourseHolder/CourseHolder";
import { useStore } from "../../store";
import { useEffect } from "react";
import SearchInput from "./SearchInput/SearchInput";

const HomePage = () => {

  const { summaryStore } = useStore();
  const { getSummaryCourses } = summaryStore;

  useEffect(() => {
    getSummaryCourses();
  }, []);

  return <HomePageWrapper>
    <Tracker/>
    <SearchInput/>
    <CourseHolder/>
  </HomePageWrapper>;
};

export default observer(HomePage);
