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
  min-height: 300px;
`;

export const CourseLeftSide = styled.div`
  flex: 4;
  margin-left: 10px;
`;

export const CourseLeftSideImage = styled.div`
  display: flex;
  height: 100%;
  background-color: ${(props) => props.color};
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
