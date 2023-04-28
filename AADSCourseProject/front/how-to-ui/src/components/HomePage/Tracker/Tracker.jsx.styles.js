import styled from "styled-components";

export const TrackerWrapper = styled.div`
  width: 100%;
`;

export const StyledSquircle = styled.div`
  display: flex;

  width: 100%;
  height: 300px;
  /* Border color */
  background: black;

  ::before {
    /* Background color  */
    background: #fff;
  }
`;

export const LeftSide = styled.div`
  flex: 5;
  padding: 20px;
`;

export const LeftSideStyledSquircle = styled.div`
  display: flex;

  width: 100%;
  height: 100%;


  ::before {
    /* Background color  */
    background: #b3cfff;
  }
`;

export const RightSide = styled.div`
  flex: 8;
  padding: 20px;
  /* background-color: beige; */
  display: flex;
  flex-direction: column;
  justify-content: space-between;
`;

export const RightUpSide = styled.div``;
export const RightBottomSide = styled.div`
  width: 100%;
`;

export const Title = styled.div`
  font-size: 36px;
  font-weight: 600;
  margin-bottom: 10px;
`;

export const Description = styled.div``;

export const ProgressBarWrapper = styled.div`
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
`;

export const ProgressBarTitle = styled.div`
  font-size: 20px;
  text-align: center;
  margin-bottom: 5px;
`;

export const ProgressBar = styled.div`
  display: flex;
  .active {
    opacity: 1;
  }
  .first {
    border-start-start-radius: 50px;
  }
  .last {
    border-end-end-radius: 50px;
  }
`;

export const Bar = styled.div`
  flex: 1;
  padding: 2px;
  height: 10px;
  z-index: 10;
  background-color: #99e9d1;
  opacity: 0.3;
`;
