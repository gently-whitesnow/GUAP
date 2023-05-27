import styled from "styled-components";

export const OneClickButtonWrapper = styled.button`
  display: flex;
  align-items: center;
  font-weight: 200;
  font-size: 18px;
  height: 35px;
  background-color: ${(props) => (props.active ? props.color : "transparent")};
  color: ${(props) => (props.active ? "white" : "black")};

  box-shadow: 0px 4px whitesmoke;
  border: none;
  cursor: pointer;
  transition: 0.6s;
  :hover {
    box-shadow: 0px -35px whitesmoke inset;
    color: ${(props) => (props.active ? props.color : "transparent")};
  }
`;
