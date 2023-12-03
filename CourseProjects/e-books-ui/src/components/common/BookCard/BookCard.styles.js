import styled from "styled-components";

export const BookCardWrapper = styled.div`
  width: 200px;
  height: 300px;
  background-color: white;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  &:hover {
    cursor: pointer;
  }
`;

export const ImageWrapper = styled.div`
  background-color: #cfd8fe;
  height: 100%;
`;

export const TitleWrapper = styled.div`
font-size: 14px;
  height: 58px;
  padding: 5px;
`;
