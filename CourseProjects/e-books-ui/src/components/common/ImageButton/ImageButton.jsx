import { ImageButtonWrapper, ImageWrapper } from "./ImageButton.styles";
import { observer } from "mobx-react-lite";
import ImageIcon from "../../../images/image.svg?react";
import { useEffect, useState } from "react";
import IconTrash from "../../../images/trash.svg?react";

const ImageButton = (props) => {
  const [img, setImg] = useState(null);

  const onChangeHandler = () => {
    if (!props.imageRef.current?.files[0]) return;
    let url = URL.createObjectURL(props.imageRef.current?.files[0]);
    setImg(url);
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
    return (
      <>
        {!img ? (
          <>
          <img src={ImageIcon} />
            <label for="myimage" className="chous"></label>
          </>
        ) : (
          <ImageWrapper>
            <img src={IconTrash} onClick={clearImageHandler}/>
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

    // return (
    //   <ImageWrapper>
    //     <img src={img}></img>
    //   </ImageWrapper>
    // );
  };

  return (
    <ImageButtonWrapper color={props.color} isAuthor={props.isAuthor}>
      {getImageComponent()}
    </ImageButtonWrapper>
  );
};

export default observer(ImageButton);
