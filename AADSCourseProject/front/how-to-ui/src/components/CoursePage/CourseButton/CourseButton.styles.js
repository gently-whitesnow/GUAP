import styled from "styled-components";
import theme from "../../../theme";

export const CourseButtonWrapper = styled.div`
  min-height: 50px;

  padding: 5px 30px;

  margin-bottom: 15px;
  display: flex;
  align-items: center;
  justify-content: space-between;

  cursor: pointer;

  background-color: white;

  transition: 0.1s;
  :hover {
    background-color: ${(props) => props.color};
    color: white;
    box-shadow: rgba(0, 0, 0, 0.1) 0px 1px 2px 0px;
    transform: scale(1.005);
    .course-tag{
      color: white;
    }
  }
`;

export const CourseTitle = styled.div`
  font-size: 20px;
  font-weight: 600;
`;

export const CourseTag = styled.div`
  font-size: 18px;
  font-weight: 500;
`;