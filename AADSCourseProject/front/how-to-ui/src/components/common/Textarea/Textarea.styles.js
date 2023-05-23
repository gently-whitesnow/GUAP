import styled from "styled-components";

export const TextareaWrapper = styled.div`
  width: 100%;
  height: 100%;
  textarea {
  box-sizing: border-box;
  overflow: hidden;
  resize: none;
  background-color: transparent;
  margin: 0px;
}
`;

export const TextareaContent = styled.textarea`
  width: 100%;
  font-size: ${(props) =>props.fontsize ? props.fontsize : "36px"};
  font-weight: ${(props) =>props.fontweight ? props.fontweight : 600};
  margin-bottom: 10px;
  color: ${(props) => props.color};
  height: ${(props) => props.height};
  ${(props) => (props.disabled ? "border:none" : "")}
`;




