import { observer } from "mobx-react-lite";

import { TestModuleWrapper } from "./TestModule.styles";
import Answer from "./Answer/Answer";
import { useEffect, useState } from "react";
import { useStore } from "../../../store";

const TestModule = (props) => {
  const [answers, setAnswers] = useState(0);

  const { testStore } = useStore();
  const {
    getCounterValue
  } = testStore;

  useEffect(() => {
    setAnswers(getAnswers());
    
  }, []);

  const rightsCount = props.rightAnswers.length;
  const isCirceledAnswer = rightsCount === 1;

  const getAnswers = () => {
    let answers = props.rightAnswers.map((text) => {
      return (
        <Answer
          color={props.color}
          content={text}
          isTrue={true}
          testModuleCounterId={props.testModuleCounterId}
          isCirceledAnswer={isCirceledAnswer}
        />
      );
    });
    answers.push(
      props.wrongAnswers.map((text) => {
        return (
          <Answer
            color={props.color}
            content={text}
            isTrue={false}
            testModuleCounterId={props.testModuleCounterId}
            isCirceledAnswer={isCirceledAnswer}
          />
        );
      })
    );
    return shuffle(answers).map((e) => {
      return e;
    });
  };

  return (
    <TestModuleWrapper
      complete={getCounterValue(props.testModuleCounterId) === rightsCount}
      color={props.color}
    >
      {answers}
    </TestModuleWrapper>
  );
};

export default observer(TestModule);

export function shuffle(arr) {
  var j, temp;
  for (var i = arr.length - 1; i > 0; i--) {
    j = Math.floor(Math.random() * (i + 1));
    temp = arr[j];
    arr[j] = arr[i];
    arr[i] = temp;
  }
  return arr;
}
