import { ImageDisplayWrapper, ImageWrapper } from "./ImageDisplay.styles";
import { observer } from "mobx-react-lite";

const ImageDisplay = ({image}) => {
  console.log(image);
  return (
    <ImageDisplayWrapper>
      <ImageWrapper>
        {image ? <img src={image}></img> : null}
      </ImageWrapper>
    </ImageDisplayWrapper>
  );
};

export default observer(ImageDisplay);
