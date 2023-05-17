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

  textarea {
    box-sizing: border-box;
    overflow: hidden;
    resize: none;
    background-color: white;
    margin: 0px;
  }
`;

export const CourseHeaderWrapper = styled.div`
  display: flex;
`;

export const IconButtonsWrapper = styled.div`
  display: flex;
`;

export const CourseTitle = styled.textarea`
  font-size: 36px;
  font-weight: 600;
  margin-bottom: 10px;
  color: ${(props) => props.color};
  width: 100%;
  height: 60px;
  ${(props) => (props.disabled ? "border:none" : "")}
`;
export const CourseDescription = styled.textarea`
  font-size: 18px;
  width: 100%;
  height: 100%;
  color: black;
  ${(props) => (props.disabled ? "border:none" : "")}
`;
