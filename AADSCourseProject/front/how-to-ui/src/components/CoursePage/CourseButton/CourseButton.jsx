import {
  CourseButtonContent,
  CourseButtonWrapper,
  CourseTag,
  CourseTitle,
  CourseToolsWrapper,
} from "./CourseButton.styles";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router";
import IconButton from "../../common/IconButton/IconButton";
import { ReactComponent as IconEdit } from "../../../icons/pen-edit.svg";
import { ReactComponent as IconCheck } from "../../../icons/check.svg";
import { ReactComponent as IconTrash } from "../../../icons/trash.svg";
import theme from "../../../theme";
import { useState } from "react";
import { IconButtonsWrapper } from "../CoursePage.styles";

const CourseButton = (props) => {
  const [isArticleEditing, setisArticleEditing] = useState(
    props.article?.isArticleEditing
  );
  const [title, setTitle] = useState(props.article?.title);
  const [path, setPath] = useState(props.article?.path);
  const navigate = useNavigate();
  const onClickHandler = () => {
    navigate(props.article?.full_path);
  };

  const onEditClickHandler = () => {
    if (!isArticleEditing) {
      setisArticleEditing(!isArticleEditing);
      return;
    }
    setisArticleEditing(!isArticleEditing);
    props.updateArticle(props.article.id, title, path);
  };

  return (
    <CourseButtonWrapper>
      <CourseButtonContent onClick={onClickHandler} color={props.color}>
        <CourseTitle>
          {props.counter} - {title ?? "C++ для сишарпников"}{" "}
        </CourseTitle>
        <CourseToolsWrapper>
          {props.article?.isRead && !isArticleEditing ? (
            <CourseTag className="course-tag">Прочитана</CourseTag>
          ) : null}
        </CourseToolsWrapper>
      </CourseButtonContent>
      {props.article?.isAuthor ? (
        isArticleEditing ? (
          <IconButtonsWrapper>
            <IconButton
              color={theme.colors.red}
              onClick={onEditClickHandler}
              active
            >
              <IconTrash />
            </IconButton>
            <IconButton
              color={theme.colors.green}
              onClick={onEditClickHandler}
              active
            >
              <IconCheck />
            </IconButton>
          </IconButtonsWrapper>
        ) : (
          <IconButton color={props.color} onClick={onEditClickHandler}>
            <IconEdit />
          </IconButton>
        )
      ) : null}
    </CourseButtonWrapper>
  );
};

export default observer(CourseButton);
