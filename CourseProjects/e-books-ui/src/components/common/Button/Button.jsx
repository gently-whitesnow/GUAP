import { ButtonWrapper } from "./Button.styles";
import { observer } from "mobx-react-lite";

const Button =  ({ content, onClick, color, background }) => {
  return (
    <ButtonWrapper className="custom-btn" color={color} background={background} onClick={onClick}>{content}</ButtonWrapper>
  );
};

export default observer(Button);
