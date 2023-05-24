import { useState } from "react";
import { OneClickButtonWrapper } from "./OneClickButton.styles";

const OneClickButton = (props) => {
  const [clicked, setClicked] = useState(props.active);

  const onClickHandler = () => {
    if (clicked) return;
    setClicked(true);
    props.onClick();
  };
  return (
    <OneClickButtonWrapper
      onClick={onClickHandler}
      active={props.active}
      color={props.color}
    >
      {props.content}
    </OneClickButtonWrapper>
  );
};

export default OneClickButton;
