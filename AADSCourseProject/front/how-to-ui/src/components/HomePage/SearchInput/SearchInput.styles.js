import styled from "styled-components";

export const SearchInputHolder = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 40px;
`;

export const SearchInputWrapper = styled.div`
  position: relative;
  svg {
    top: 5px;
    right: 8px;
    position: absolute;
    color: ${(props) => props.color};
  }
`;

export const Input = styled.input.attrs({ type: "text" })`
  padding-left: 15px;
  font-size: 24px;
  height: 40px;
  width: 600px;
  margin-top: auto;
  outline: none;
  border: none;
  box-shadow: rgba(0, 0, 0, 0.1) 0px 1px 2px 0px;
  :focus {
    color: ${(props) => props.color};
  }
`;
