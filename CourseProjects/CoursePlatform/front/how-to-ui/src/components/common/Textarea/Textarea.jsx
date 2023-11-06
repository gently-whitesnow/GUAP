import { useEffect, useRef } from "react";
import { TextareaContent, TextareaWrapper } from "./Textarea.styles";
import {
  clearTextAreaContent
} from "../../../helpers/textareaHelper";

const Textarea = (props) => {
  const onChangeHandler = (e) => {
    e.target.value = clearTextAreaContent(e.target.value)
    props.onChange(e);
  };

  return (
    <TextareaWrapper>
      <TextareaContent
        color={props.color ?? "black"}
        value={props.value}
        disabled={props.disabled}
        onChange={onChangeHandler}
        maxLength={props.maxLength}
        height={props.height}
        fontsize={props.fontsize}
        fontweight={props.fontweight}
        placeholder={props.placeholder}
      ></TextareaContent>
    </TextareaWrapper>
  );
};

export default Textarea;
