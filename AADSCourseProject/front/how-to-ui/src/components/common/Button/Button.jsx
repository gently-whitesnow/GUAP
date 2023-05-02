import {
  ButtonWrapper
} from "./Button.styles";

const Button = (props) => {
  return (
    <ButtonWrapper onClick={props.onClick}>
      {props.content}
    </ButtonWrapper>
  );
};

export default Button;
