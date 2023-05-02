import { observer } from "mobx-react-lite";
import {
  CourseHolderContent,
  CourseHolderWrapper,
} from "./CourseHolder.styles";
import CourseCard from "../CourseCard/CourseCard";
import theme from "../../../theme";

const CourseHolder = () => {
  return (
    <CourseHolderWrapper>
      <CourseHolderContent>
        <CourseCard
          title="Основы аста для начинающих новичков джунов и всех подобных"
          color={
            theme.CardColors[
              Math.floor(Math.random() * theme.CardColors.length)
            ]
          }
        />
        {[1, 2, 3, 4, 5, 6, 7, 8, 9].map((answer, i) => {
          return (
            <CourseCard
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
