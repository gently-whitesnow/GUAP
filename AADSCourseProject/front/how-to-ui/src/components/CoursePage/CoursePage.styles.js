import styled from "styled-components";

export const CoursePageWrapper = styled.div``;

export const CourseHeader = styled.div`
  background: white;
  display: flex;
  justify-content: center;
`;

export const CourseHeaderContent = styled.div`
  margin-top: 30px;
  display: flex;
  max-width: 1148px;
  width: 100%;
  height: 300px;
`;

export const CourseLeftSide = styled.div`
  flex: 4;
  margin-left: 10px;
`;

export const CourseLeftSideImage = styled.div`
  display: flex;
  height: 100%;
  background-color: ${(props) => props.color};
  position: relative;
  :hover {
    cursor: pointer;
    svg {
      transform: scale(1.5);
    }
  }
  svg {
    transform: scale(1.2);
    color: white;
    position: absolute;
    left: 0;
    right: 0;
    top: 0;
    bottom: 0;
    margin: auto;
  }
`;

export const CourseRightSide = styled.div`
  flex: 10;
  margin-right: 10px;
  padding: 20px;
  display: flex;
  flex-direction: column;

  width: 100%;
`;

export const CourseHeaderWrapper = styled.div`
  display: flex;
`;

export const IconButtonsWrapper = styled.div`
  height: 100%;
  background-color: white;
  display: flex;
  flex-direction: column;
`;

export const AuthorsWrapper = styled.div`
  opacity: 0.6;
  color: ${(props) => props.color};
`;
