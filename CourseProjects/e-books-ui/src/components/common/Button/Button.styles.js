import styled from "styled-components";
import colorThemes from "../../../colorThemes";

export const ButtonWrapper = styled.button`
  text-transform: uppercase;
  font-size: 14px;
  border: 1px ${(props) => props.color ?? colorThemes.colors.blue} solid;
  border-radius: 30px;
  min-height: 40px;
  background-color: ${(props) => props.background ?? "transparent"};
  color: ${(props) => props.color ?? colorThemes.colors.blue};
  padding-inline: 10px;
  &:hover {
    background-color: ${(props) => props.color ?? colorThemes.colors.blue};
    color: white;
    cursor: pointer;
  }
`;
