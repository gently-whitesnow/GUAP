import styled from "styled-components";

export const TestModuleWrapper = styled.div`
  transition: 0.3s;
  border: solid ${(props) => (props.complete ? "2px" : "0.5px")} ${(props) => props.complete ? "rgb(0, 212, 102);" : "rgba(220, 227, 227, 1)"};
  padding: 10px;
  border-radius: 4px;
`;
