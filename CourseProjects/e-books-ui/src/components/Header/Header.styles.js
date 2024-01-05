import styled from "styled-components";

export const HeaderWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  min-height: 50px;
  background-color: white;
  padding-inline: 20px;
  box-shadow: rgba(0, 0, 0, 0.05) 0px 0px 0px 1px;
`;

export const ProductName = styled.div`
  font-weight: 600;
  font-size: 24px;
  cursor: pointer;
`;


export const ButtonsWrapper = styled.div`
  display: flex;
  .custom-btn
  {
    margin-left: 10px;
  }
`;
