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
  min-height: 300px;
`;

export const CourseTitle = styled.div`
  font-size: 36px;
  font-weight: 600;
  margin-bottom: 10px;
  color: ${(props) => props.color};
`;
export const CourseDescription = styled.div`
  font-size: 18px;
`;

export const CourseLeftSide = styled.div`
  flex: 5;
  margin-left: 10px;
`;

export const CourseLeftSideImage = styled.div`
  display: flex;
  height: 100%;
  background-color: ${(props) => props.color};
`;

export const CourseRightSide = styled.div`
  flex: 8;
  margin-right: 10px;
  padding: 20px;
  display: flex;
  flex-direction: column;
`;
