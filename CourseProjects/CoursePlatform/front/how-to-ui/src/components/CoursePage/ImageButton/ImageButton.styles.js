import styled from "styled-components";

export const ImageButtonWrapper = styled.div`
  display: flex;
  height: 100%;
  background-color: ${(props) => props.color};
  position: relative;
  transition: all 0.18s ease-in-out;
  :hover {
    svg {
      transform: scale(1.3);
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
    cursor: pointer;
  }
  input {
    width: 0.1px;
    height: 0.1px;
    opacity: 0;
    overflow: hidden;
    position: absolute;
    z-index: -1;
  }
  label {
    cursor: pointer;
    z-index: 10;
    display: block;
    box-sizing: border-box;
    width: 100%;
  }
`;
export const ImageWrapper = styled.div`
  width: 100%;
  img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
`;
