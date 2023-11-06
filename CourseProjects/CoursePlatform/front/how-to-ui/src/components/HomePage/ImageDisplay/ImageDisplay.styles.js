import styled from "styled-components";

export const ImageDisplayWrapper = styled.div`
  display: flex;
  height: 100%;

  background-color: ${(props) => props.color[2]};
  position: relative;
  height: 260px;
`;
export const ImageWrapper = styled.div`
  width: 100%;
  img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
`;
