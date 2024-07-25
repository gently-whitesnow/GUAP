import styled from "styled-components";
import Popup from "reactjs-popup";

export const BookPopupWrapper = styled.div`
  position: absolute;
`;

export const StyledPopup = styled(Popup)`
  // use your custom style for ".popup-overlay"
  &-overlay {
    background-color: rgba(0, 0, 0, 0.5);
  }
  // use your custom style for ".popup-content"
  &-content {
  }
`;

export const BookContentWrapper = styled.div`
  width: 1024px;
  height: 800px;
  background-color: white;
`;

export const HeaderWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  /* height: 40px; */
  background-color: white;
  align-items: center;
  padding: 5px;
`;

export const HeaderTitle = styled.div`
  font-weight: 600;
  font-size: 30px;
`;

export const BodyWrapper = styled.div``;

export const AboutWrapper = styled.div`
  display: flex;
  width: 100%;
  height: 500px;
`;

export const Image = styled.div`
  background-color: #cfd8fe;

  max-width: 350px;
  min-width: 350px;
`;

export const About = styled.div`
  padding-left: 40px;
`;
export const Title = styled.div`
  font-size: 30px;
  font-weight: 600;
`;
export const Author = styled.div`
  font-size: 20px;
  font-style: italic;
`;
export const Description = styled.div`
  margin-top: 20px;
  height: 350px;
  overflow-y: scroll;
`;

export const PopupFooterWrapper = styled.div`
  display: flex;
  justify-content: end;
`;
