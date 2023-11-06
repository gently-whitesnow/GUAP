import { observer } from "mobx-react-lite";

import { AnswerContent, AnswerWrapper, Checkbox } from "./Answer.styles";
import { useState } from "react";

import { useStore } from "../../../../store";

const Answer = (props) => {
  const [checked, setChecked] = useState(false);

  const { testStore } = useStore();
  const { increaseCounter } = testStore;

  const onClickHandler = () => {
    if (checked) return;
    setChecked(true);
    if (props.isTrue) increaseCounter(props.testModuleCounterId);
  };

  return (
    <AnswerWrapper>
      <AnswerContent onClick={onClickHandler} color={props.color}>
        <Checkbox
          className={
            (checked ? (props.isTrue ? "right" : "wrong") : "") + " checkbox"
          }
          color={props.color}
          isCirceledAnswer={props.isCirceledAnswer}
        />
        {props.content}
      </AnswerContent>
    </AnswerWrapper>
  );
};

export default observer(Answer);
