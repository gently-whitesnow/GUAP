import { observer } from "mobx-react-lite";
import {
  CourseHolderContent,
  CourseHolderWrapper,
} from "./CourseHolder.styles";
import CourseCard from "../CourseCard/CourseCard";
import theme from "../../../theme";
import { useStore } from "../../../store";
import { useEffect } from "react";

const CourseHolder = () => {

  const { summaryStore } = useStore();
  const {  summaryData } = summaryStore;


  return (
    <CourseHolderWrapper>
      <CourseHolderContent>
        {summaryData.courses?.map((data)=>{
          return (
            <CourseCard
              title={data.title}
              id={data.id}
              color={
                theme.CardColors[
                  Math.floor(Math.random() * theme.CardColors.length)
                ]
              }
            />
          );
        })}
      </CourseHolderContent>
    </CourseHolderWrapper>
  );
};

export default observer(CourseHolder);
