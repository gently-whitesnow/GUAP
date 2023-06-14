import { ImageButtonWrapper, ImageWrapper } from "./ImageButton.styles";
import { observer } from "mobx-react-lite";
import { ReactComponent as ImageIcon } from "../../../icons/image.svg";
import { useEffect, useState } from "react";
import { ReactComponent as IconTrash } from "../../../icons/trash.svg";

const ImageButton = (props) => {
  const [img, setImg] = useState(null);

  const onChangeHandler = () => {
    if (!props.imageRef.current?.files[0]) return;
    let url = URL.createObjectURL(props.imageRef.current?.files[0]);
    setImg(url);
    props.setIsCourseEditing(true);
  };

  useEffect(() => {
    setImg(props.image);
  }, [props.image]);

  const clearImageHandler = () => {
    if (props.imageRef.current?.value) {
      props.imageRef.current.value = "";
    }
    setImg(null);
    if (
      props.image &&
      !props.isCourseEditing &&
      !props.imageRef.current?.value
    ) {
      props.setIsCourseEditing(true);
    }
  };

  const getImageComponent = () => {
    if (props.isAuthor) {
      return (
        <>
          {!img ? (
            <>
              <ImageIcon />
              <label for="myimage" className="chous"></label>
            </>
          ) : (
            <ImageWrapper>
              <IconTrash onClick={clearImageHandler} />
              <img src={img}></img>
            </ImageWrapper>
          )}
          <input
            type="file"
            className="my"
            id="myimage"
            name="myimage"
            ref={props.imageRef}
            accept="image/png, image/gif, image/jpeg, image/jpg"
            onChange={onChangeHandler}
          />
        </>
      );
    }
    return (
      <ImageWrapper>
        <img src={img}></img>
      </ImageWrapper>
    );
  };

  return (
    <ImageButtonWrapper color={props.color} isAuthor={props.isAuthor}>
      {getImageComponent()}
    </ImageButtonWrapper>
  );
};

export default observer(ImageButton);
