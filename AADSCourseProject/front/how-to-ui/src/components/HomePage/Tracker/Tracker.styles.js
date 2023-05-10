import styled from "styled-components";
import theme from "../../../theme.js";

export const TrackerWrapper = styled.div`
  display: flex;
  justify-content: center;
  background-color: white;
  width: 100%;
`;

export const TrackerBody = styled.div`
  margin-top: 30px;
  display: flex;
  height: 400px;
  cursor: pointer;
  max-width: 1148px;

  :hover {
    .continue-button {
      opacity: 0.5;
    }
  }
`;

export const LeftSide = styled.div`
  flex: 5;
  margin-left: 10px;
`;

export const LeftSideImage = styled.div`
  display: flex;

  height: 100%;
  background-color: ${(props) => props.color};
`;

export const RightSide = styled.div`
  flex: 8;
  margin-right: 10px;
  padding: 20px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
`;

export const RightUpSide = styled.div``;
export const RightBottomSide = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

export const Title = styled.div`
  font-size: 36px;
  font-weight: 600;
  margin-bottom: 10px;
  color: ${(props) => props.color};
`;

export const Description = styled.div`
  font-size: 18px;
`;

export const ContinueButton = styled.div`
  font-size: 48px;
  width: 80%;
  text-align: center;
  color: ${(props) => props.color};
  opacity: 0.1;
  transition: 0.4s;
  text-shadow: rgba(255, 255, 255, 0.5) 0px 3px 3px;
`;
