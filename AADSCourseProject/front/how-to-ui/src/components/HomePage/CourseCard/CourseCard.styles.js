import styled from "styled-components";

export const CourseCardWrapper = styled.div`
  
  /* height: 400px; */
  width: 340px;
  margin: 20px;
  transition: 0.5s;

  :hover {
    cursor: pointer;
    .card-image{
      background-color: ${(props) => props.color[1]};
    }
    
  }
`;

export const CourseCardContent = styled.div`
  display: flex;
  flex-direction: column;
  height:100%;
`;

export const CardTitle = styled.div`
  font-size: 26px;
  font-weight: 600;
  margin-top: 15px;
  margin-bottom: 25px;
`;
