import {
  IconButtonWrapper
} from "./IconButton.styles";

const IconButton = (props) => {
  return (
    <IconButtonWrapper disabled={props.disabled} color={props.color} onClick={props.onClick} active={props.active} size={props.size}>
      {props.children}
    </IconButtonWrapper>
  );
};

export default IconButton;
