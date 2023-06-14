import CourseHolder from "./CourseHolder/CourseHolder";
import {
  AuthorsWrapper,
  CourseHeader,
  CourseHeaderContent,
  CourseHeaderWrapper,
  CourseLeftSide,
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
import Textarea from "../common/Textarea/Textarea";
import ImageButton from "./ImageButton/ImageButton";

const CoursePage = () => {
  const imageInputRef = useRef(null);
  const { colorStore, courseStore, stateStore } = useStore();
  const { currentColorTheme } = colorStore;
  const {isLoading} = stateStore;
  const {
    getCourse,
    isCourseEditing,
    title,
    description,
    setTitle,
    setDescription,
    isAuthor,
    setIsCourseEditing,
    courseActionError,
    setCourseActionError,
    upsertCourse,
    id,
    image,
    deleteCourse,
    setCourseCreate,
    contributors,
  } = courseStore;
  const { courseId } = useParams();

  const navigate = useNavigate();

  useEffect(() => {
    if (courseId === "create") {
      setCourseCreate();
      return;
    }
    clearImageHandler();
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
    clearImageHandler();
  };

  const clearImageHandler = () => {
    if (imageInputRef.current?.value) {
      imageInputRef.current.value = '';
    }
  };

  const onCourseSaveClickHandler = () => {
    upsertCourse(
      id,
      title,
      description,
      imageInputRef.current?.files[0],
      (id) => {
        if (id !== undefined) {
          navigate(`/${id}`);
        }
      }
    );
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
            <ImageButton
              color={currentColorTheme}
              isAuthor={isAuthor}
              imageRef={imageInputRef}
              isCourseEditing={isCourseEditing}
              setIsCourseEditing={setIsCourseEditing}
              image={image}
            />
          </CourseLeftSide>
          <CourseRightSide>
            <>
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
                            disabled={isLoading}
                          >
                            <IconCheck />
                          </IconButton>
                          <IconButton
                            color={theme.colors.red}
                            onClick={onCourseDeleteClickHandler}
                            active
                            size={"50px"}
                            disabled={isLoading}
                          >
                            <IconTrash />
                          </IconButton>
                        </IconButtonsWrapper>
                        <IconButton
                          color={currentColorTheme}
                          onClick={onCourseEditClickHandler}
                          size={"50px"}
                          disabled={isLoading}
                        >
                          <IconEdit />
                        </IconButton>
                      </>
                    ) : (
                      <IconButton
                        color={currentColorTheme}
                        onClick={onCourseEditClickHandler}
                        size={"50px"}
                        disabled={isLoading}
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
            </>
            <AuthorsWrapper color={currentColorTheme}>
              {contributors?.map((e) => e.name).join(", ")}
            </AuthorsWrapper>
          </CourseRightSide>
        </CourseHeaderContent>
      </CourseHeader>
      <CourseHolder color={currentColorTheme} />
    </CoursePageWrapper>
  );
};

export default observer(CoursePage);
