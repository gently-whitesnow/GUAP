import {
  IconButtonWrapper
} from "./IconButton.styles";

const IconButton = (props) => {
  return (
    <IconButtonWrapper color={props.color} onClick={props.onClick} active={props.active} size={props.size}>
      {props.children}
    </IconButtonWrapper>
  );
};

export default IconButton;
