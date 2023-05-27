import styled from "styled-components";

export const AnswerWrapper = styled.div`
  .right {
    background-color: rgb(0, 212, 102);
  }

  .wrong {
    background-color: rgba(255, 106, 106, 1);
  }
`;

export const AnswerContent = styled.div`
  min-height: 30px;
  margin-bottom: 10px;
  display: flex;

  align-items: center;

  cursor: pointer;

  :hover {
    background-color: rgba(220, 227, 227, 0.2);
    div .checkbox {
      background: rgba(220, 227, 227, 0.2);
    }
    .checkbox .right {
      background: rgb(0, 212, 102);
    }
    .checkbox .wrong {
      background: rgba(255, 106, 106, 1);
    }
  }
`;

// not use checkbox, because it hard customizable
export const Checkbox = styled.div`
  height: 20px;
  width: 20px;
  margin-right: 10px;
  border: solid 0.5px ${(props) => props.color};
  ${(props) => (props.isCirceledAnswer ? "border-radius: 50%" : "")};
`;
