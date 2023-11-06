import styled from "styled-components";

export const HeaderWrapper = styled.div`
  position: sticky;
  top: 0;
  background-color: rgba(255, 255, 255, 0.9);
  display: flex;
  justify-content: center;
  z-index: 100;
  transition: 0.2s;
  box-sizing: border-box;
  border-bottom: ${(props)=> props.isLoading?`6px solid ${props.color}`:`0px solid ${props.color}`}
`;

export const HeaderContent = styled.div`
  width: 100%;
  max-width: 1240px;
  display: flex;
  align-items: center;
  justify-content: space-between;

  font-family: "Raleway", sans-serif !important;

  height: 55px;
  box-shadow: rgba(0, 0, 0, 0) 0px 1px 3px;

  top: 0px;
`;

export const Title = styled.div`
  font-size: 36px;
  font-weight: 600;
`;

export const Colorimetr = styled.div`
margin-left: 30px;
  cursor: pointer;
  height: 30px;
  width: 30px;
  border-radius: 100%;
  background-color: ${(props) => props.color};
  opacity: 0.3;
  :hover {
    opacity: 1;
  }
`;
export const ColorimetrWrapper = styled.div`
  display: flex;
  align-items: center;
`;
