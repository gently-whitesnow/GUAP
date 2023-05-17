import styled from "styled-components";

export const IconButtonWrapper = styled.button`
  background-color: ${(props) => (props.active ? props.color : "white")};
  color: ${(props) => (props.active ? "white" : "black")};
  height: 60px;
  width: 60px;
  border: none;

  transition: 0.1s;
  :hover {
    cursor: pointer;
    background-color: ${(props) => props.color};
    color: white;
    box-shadow: rgba(0, 0, 0, 0.1) 0px 1px 2px 0px;
    transform: scale(1.005);
    .course-tag {
      color: white;
    }
  }
`;
