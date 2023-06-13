import { ImageDisplayWrapper, ImageWrapper } from "./ImageDisplay.styles";
import { observer } from "mobx-react-lite";

const ImageDisplay = (props) => {
  return (
    <ImageDisplayWrapper color={props.color}>
      <ImageWrapper>
        {props.image ? <img src={props.image}></img> : null}
      </ImageWrapper>
    </ImageDisplayWrapper>
  );
};

export default observer(ImageDisplay);
