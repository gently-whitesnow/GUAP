import {
  IconButtonWrapper
} from "./IconButton.styles";

const IconButton = (props) => {
  return (
    <IconButtonWrapper color={props.color} onClick={props.onClick} active={props.active}>
      {props.children}
    </IconButtonWrapper>
  );
};

export default IconButton;
