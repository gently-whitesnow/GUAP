import CourseHolder from "./CourseHolder/CourseHolder";
import {
  CourseDescription,
  CourseHeader,
  CourseHeaderContent,
  CourseHeaderWrapper,
  CourseLeftSide,
  CourseLeftSideImage,
  CoursePageWrapper,
  CourseRightSide,
  CourseTitle,
  IconButtonsWrapper,
} from "./CoursePage.styles";
import { observer } from "mobx-react-lite";
import { useStore } from "../../store";
import { useEffect, useRef } from "react";
import { useParams } from "react-router-dom";
import IconButton from "../common/IconButton/IconButton";
import { ReactComponent as IconEdit } from "../../icons/pen-edit.svg";
import { ReactComponent as IconCheck } from "../../icons/check.svg";
import { ReactComponent as IconTrash } from "../../icons/trash.svg";
import theme from "../../theme";
import { adjustFontSize } from "../../helpers/textareaHelper";
import ErrorLineHandler from "../common/ErrorLineHandler/ErrorLineHandler";

const CoursePage = () => {
  const textareaRef = useRef(null);
  const { colorStore, courseStore } = useStore();
  const { getColorTheme } = colorStore;
  const {
    getCourse,
    courseData,
    setIsLoading,
    isCourseEditing,
    title,
    description,
    setTitle,
    setDescription,
    isCourseContributor,
    setIsCourseEditing,
    courseActionError,
    setCourseActionError,
    upsertCourse,
    id,
    path,
    image,
    deleteCourse
  } = courseStore;
  const { course } = useParams();

  useEffect(() => {
    setIsLoading(true);
    getCourse(course);
    adjustFontSize(textareaRef, 5);
  }, []);

  useEffect(() => {
    adjustFontSize(textareaRef, 2);
  }, [title]);

  const onTitleChangeHandler = (e) => {
    setTitle(e.target.value);
  };

  const onCourseEditClickHandler = () => {
    setIsCourseEditing(!isCourseEditing);
  };

  const onCourseSaveClickHandler = () => {
    upsertCourse(id, title, description, path, image)
  };

  const onCourseDeleteClickHandler = () => {
    deleteCourse(id)
  };

  return (
    <CoursePageWrapper>
      <CourseHeader>
        <CourseHeaderContent>
          <CourseLeftSide>
            <CourseLeftSideImage color={getColorTheme()} />
          </CourseLeftSide>
          <CourseRightSide>
            <ErrorLineHandler error={courseActionError} setActionError={setCourseActionError}>
              <CourseHeaderWrapper>
                <CourseTitle
                  ref={textareaRef}
                  color={getColorTheme()}
                  value={title}
                  disabled={!isCourseEditing}
                  onChange={(e) => onTitleChangeHandler(e)}
                  maxLength={100}
                />

                {isCourseContributor ? (
                  isCourseEditing ? (
                    <IconButtonsWrapper>
                      <IconButton
                        color={theme.colors.red}
                        onClick={deleteCourse}
                        active
                      >
                        <IconTrash />
                      </IconButton>
                      <IconButton
                        color={theme.colors.green}
                        onClick={onCourseSaveClickHandler}
                        active
                      >
                        <IconCheck />
                      </IconButton>
                    </IconButtonsWrapper>
                  ) : (
                    <IconButton
                      color={getColorTheme()}
                      onClick={onCourseEditClickHandler}
                    >
                      <IconEdit />
                    </IconButton>
                  )
                ) : null}
              </CourseHeaderWrapper>
            </ErrorLineHandler>
            <CourseDescription
              value={description}
              disabled={!isCourseEditing}
              onChange={(e) => setDescription(e.target.value)}
              maxLength={700}
            />
          </CourseRightSide>
        </CourseHeaderContent>
      </CourseHeader>
      <CourseHolder
        color={getColorTheme()}
        articles={courseData.course?.articles}
      />
    </CoursePageWrapper>
  );
};

export default observer(CoursePage);
