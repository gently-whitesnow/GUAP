import CourseHolder from "./CourseHolder/CourseHolder";
import {
  CourseHeader,
  CourseHeaderContent,
  CourseHeaderWrapper,
  CourseLeftSide,
  CourseLeftSideImage,
  CoursePageWrapper,
  CourseRightSide,
  IconButtonsWrapper,
} from "./CoursePage.styles";
import { observer } from "mobx-react-lite";
import { useStore } from "../../store";
import { useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import IconButton from "../common/IconButton/IconButton";
import { ReactComponent as IconEdit } from "../../icons/pen-edit.svg";
import { ReactComponent as IconCheck } from "../../icons/check.svg";
import { ReactComponent as IconTrash } from "../../icons/trash.svg";
import theme from "../../theme";
import ErrorLineHandler from "../common/ErrorLineHandler/ErrorLineHandler";
import { TextareaWrapper } from "../common/Textarea/Textarea.styles";
import Textarea from "../common/Textarea/Textarea";

const CoursePage = () => {
  const { colorStore, courseStore } = useStore();
  const { currentColorTheme } = colorStore;
  const {
    getCourse,
    isCourseEditing,
    title,
    description,
    setTitle,
    setDescription,
    isAuthor,
    setIsCourseContributor,
    setIsCourseEditing,
    courseActionError,
    setCourseActionError,
    upsertCourse,
    id,
    image,
    deleteCourse,
    setCourseCreate,
  } = courseStore;
  const { courseId } = useParams();

  const navigate = useNavigate();

  useEffect(() => {
    if (courseId === "create") {
      setCourseCreate();
      return;
    }
    getCourse(courseId);
  }, []);

  const onTitleChangeHandler = (e) => {
    setTitle(e.target.value);
  };

  const onCourseDescriptionChangeHandler = (e) => {
    setDescription(e.target.value);
  };

  const onCourseEditClickHandler = () => {
    setIsCourseEditing(!isCourseEditing);
  };

  const onCourseSaveClickHandler = () => {
    upsertCourse(id, title, description, image, (id) => {
      if (id !== undefined) {
        console.log(id);
        navigate(`/${id}`);
      }
    });
  };

  const onCourseDeleteClickHandler = () => {
    deleteCourse(id, (result) => {
      if (result) {
        navigate(`/`);
      }
    });
  };

  return (
    <CoursePageWrapper>
      <CourseHeader>
        <CourseHeaderContent>
          <CourseLeftSide>
            <CourseLeftSideImage color={currentColorTheme} />
          </CourseLeftSide>
          <CourseRightSide>
            <ErrorLineHandler
              error={courseActionError}
              setActionError={setCourseActionError}
            >
              <CourseHeaderWrapper>
                <Textarea
                  color={currentColorTheme}
                  value={title}
                  disabled={!isCourseEditing}
                  onChange={(e) => onTitleChangeHandler(e)}
                  maxLength={70}
                  height={"100px"}
                  placeholder={"Введите название курса"}
                />

                {isAuthor ? (
                  isCourseEditing ? (
                    <>
                      <IconButtonsWrapper>
                        <IconButton
                          color={theme.colors.green}
                          onClick={onCourseSaveClickHandler}
                          active
                          size={"50px"}
                        >
                          <IconCheck />
                        </IconButton>
                        <IconButton
                          color={theme.colors.red}
                          onClick={onCourseDeleteClickHandler}
                          active
                          size={"50px"}
                        >
                          <IconTrash />
                        </IconButton>
                      </IconButtonsWrapper>
                      <IconButton
                        color={currentColorTheme}
                        onClick={onCourseEditClickHandler}
                        size={"50px"}
                      >
                        <IconEdit />
                      </IconButton>
                    </>
                  ) : (
                    <IconButton
                      color={currentColorTheme}
                      onClick={onCourseEditClickHandler}
                      size={"50px"}
                    >
                      <IconEdit />
                    </IconButton>
                  )
                ) : null}
              </CourseHeaderWrapper>
            </ErrorLineHandler>
            <Textarea
              value={description}
              disabled={!isCourseEditing}
              onChange={(e) => onCourseDescriptionChangeHandler(e)}
              maxLength={600}
              fontsize={"18px"}
              fontweight={"400"}
              height={"100%"}
              placeholder={"Введите описание курса"}
            />
          </CourseRightSide>
        </CourseHeaderContent>
      </CourseHeader>
      <CourseHolder color={currentColorTheme} />
    </CoursePageWrapper>
  );
};

export default observer(CoursePage);
