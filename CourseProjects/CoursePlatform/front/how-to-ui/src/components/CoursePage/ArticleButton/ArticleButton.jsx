import {
  ArticleButtonContent,
  ArticleButtonWrapper,
  ArticleTag,
  ArticleTitle,
  ArticleToolsWrapper,
  IconButtonsWithUploaderWrapper,
} from "./ArticleButton.styles";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router";
import IconButton from "../../common/IconButton/IconButton";
import { ReactComponent as IconEdit } from "../../../icons/pen-edit16.svg";
import { ReactComponent as IconCheck } from "../../../icons/check16.svg";
import { ReactComponent as IconTrash } from "../../../icons/trash16.svg";
import theme from "../../../theme";
import { useRef, useState } from "react";
import { IconButtonsWrapper } from "../CoursePage.styles";
import Textarea from "../../common/Textarea/Textarea";
import ErrorLineHandler from "../../common/ErrorLineHandler/ErrorLineHandler";
import FileUploader from "../FileUploader/FileUploader";
import { useEffect } from "react";

const ArticleButton = (props) => {
  const fileInputRef = useRef(null);

  const [isArticleEditing, setIsArticleEditing] = useState(
    props.article?.isArticleEditing
  );
  const [error, setError] = useState();
  const [title, setTitle] = useState(props.article?.title);

  useEffect(() => {
    setTitle(props.article?.title);
  }, [props.article?.title]);

  const navigate = useNavigate();
  const onClickHandler = () => {
    if (isArticleEditing || props.article?.id === undefined) {
      return;
    }
    navigate(`/${props.article?.courseId}/${props.article?.id}`);
  };

  const onEditClickHandler = () => {
    setIsArticleEditing(!isArticleEditing);
  };

  const onDeleteClickHandler = () => {
    if (props.article.isNewArticle) {
      props.deleteArticle();
    }

    props.deleteArticle(props.article.courseId, props.article.id, (error) => {
      setError(error);
      if (!error) setIsArticleEditing(false);
    });
  };

  const onSaveClickHandler = () => {
    props.upsertArticle(
      props.article.id,
      props.article.courseId,
      title,
      fileInputRef.current?.files[0],
      props.article.isNewArticle,
      (error) => {
        setError(error);
        if (!error) setIsArticleEditing(false);
      }
    );
  };

  return (
    <ErrorLineHandler error={error} setActionError={setError}>
      <ArticleButtonWrapper ref={props.innerRef}>
        <ArticleButtonContent
          onClick={onClickHandler}
          color={props.color}
          isArticleEditing={isArticleEditing}
        >
          {isArticleEditing ? (
            <Textarea
              value={title}
              disabled={false}
              onChange={(e) => setTitle(e.target.value)}
              maxLength={100}
              height={"60px"}
              fontsize={"24px"}
              placeholder={"Введите название страницы"}
            />
          ) : (
            <ArticleTitle>{title}</ArticleTitle>
          )}

          <ArticleToolsWrapper>
            {props.article?.isViewed && !isArticleEditing ? (
              <ArticleTag className="read-tag">Прочитана</ArticleTag>
            ) : null}
          </ArticleToolsWrapper>
        </ArticleButtonContent>
        {props.article?.isAuthor ? (
          isArticleEditing ? (
            <>
              <IconButtonsWithUploaderWrapper>
                <FileUploader color={props.color} fileInputRef={fileInputRef} />
                <IconButtonsWrapper>
                  <IconButton
                    color={theme.colors.green}
                    onClick={onSaveClickHandler}
                    active
                    size={"30px"}
                    disabled={props.isLoading}
                  >
                    <IconCheck />
                  </IconButton>
                  <IconButton
                    color={theme.colors.red}
                    onClick={onDeleteClickHandler}
                    active
                    size={"30px"}
                    disabled={props.isLoading}
                  >
                    <IconTrash />
                  </IconButton>
                </IconButtonsWrapper>
              </IconButtonsWithUploaderWrapper>
              <IconButtonsWrapper>
                <IconButton
                  color={props.color}
                  onClick={onEditClickHandler}
                  size={"30px"}
                  disabled={props.isLoading}
                >
                  <IconEdit />
                </IconButton>
              </IconButtonsWrapper>
            </>
          ) : (
            <IconButtonsWrapper>
              <IconButton
                color={props.color}
                onClick={onEditClickHandler}
                size={"30px"}
                disabled={props.isLoading}
              >
                <IconEdit />
              </IconButton>
            </IconButtonsWrapper>
          )
        ) : null}
      </ArticleButtonWrapper>
    </ErrorLineHandler>
  );
};

export default observer(ArticleButton);
