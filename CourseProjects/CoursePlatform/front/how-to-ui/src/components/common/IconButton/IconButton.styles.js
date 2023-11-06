import styled from "styled-components";

export const IconButtonWrapper = styled.button`
  background-color: ${(props) => (props.active ? props.color : "white")};
  color: ${(props) => (props.active ? "white" : "black")};
  height: ${(props) => props.size};
  width: ${(props) => props.size};
  border: none;

  transition: 0.1s;
  :hover {
    cursor: pointer;
    background-color: ${(props) => props.color};
    color: white;
    box-shadow: rgba(0, 0, 0, 0.1) 0px 1px 2px 0px;
    transform: scale(1.005);
    .read-tag {
      color: white;
    }
  }
`;
