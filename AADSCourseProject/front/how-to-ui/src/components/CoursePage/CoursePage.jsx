import CourseHolder from "./CourseHolder/CourseHolder";
import {
  CourseDescription,
  CourseHeader,
  CourseHeaderContent,
  CourseLeftSide,
  CourseLeftSideImage,
  CoursePageWrapper,
  CourseRightSide,
  CourseTitle,
} from "./CoursePage.styles";
import { observer } from "mobx-react-lite";
import theme from "../../theme";
import { useStore } from "../../store";

const CoursePage = () => {
  const { colorStore } = useStore();
  const { getColorTheme } = colorStore;

  return (
    <CoursePageWrapper>
      <CourseHeader>
        <CourseHeaderContent>
          <CourseLeftSide>
            <CourseLeftSideImage color={getColorTheme()}/>
          </CourseLeftSide>
          <CourseRightSide>
            <CourseTitle color={getColorTheme()}>Title title title title</CourseTitle>
            <CourseDescription>
              Перед вами учебник по основам языка C++. При его создании мы
              вдохновлялись специализацией «Искусство разработки на современном
              C++» на «Курсере», созданной при поддержке Яндекса, и курсом
              «Основы и методология программирования», который читался в
              2014–2021 годах на факультете компьютерных наук ВШЭ.
            </CourseDescription>
          </CourseRightSide>
        </CourseHeaderContent>
      </CourseHeader>
      <CourseHolder color={getColorTheme()}/>
    </CoursePageWrapper>
  );
};

export default observer(CoursePage);
