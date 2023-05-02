import { observer } from "mobx-react-lite";
import {
  TrackerWrapper,
  TrackerBody,
  LeftSide,
  RightSide,
  Description,
  Title,
  RightUpSide,
  RightBottomSide,
  LeftSideImage,
  ContinueButton,
} from "./Tracker.styles";


import Image from "react-graceful-image";
import theme from "../../../theme";
import { useNavigate } from "react-router";
import ProgressBar from "../ProgressBar/ProgressBar";
import { useStore } from "../../../store";

const Tracker = () => {
  const { colorStore } = useStore();
  const { getColorTheme } = colorStore;

  const navigate = useNavigate();
  const onClickHandler = () => {
    navigate("/ast");
  };

 
  return (
    <TrackerWrapper>
      <TrackerBody onClick={onClickHandler}>
        <LeftSide>
          <LeftSideImage color={getColorTheme()}></LeftSideImage>
        </LeftSide>
        <RightSide>
          <RightUpSide>
            <Title color={getColorTheme()}>C++ для сишарпников</Title>
            <Description>
              Перед вами учебник по основам языка C++. При его создании мы
              вдохновлялись специализацией «Искусство разработки на современном
              C++» на «Курсере», созданной при поддержке Яндекса, и курсом
              «Основы и методология программирования», который читался в
              2014–2021 годах на факультете компьютерных наук ВШЭ.
            </Description>
          </RightUpSide>
          <RightBottomSide>
            <ContinueButton color={getColorTheme()} className="continue-button">Продолжить</ContinueButton>
            <ProgressBar percents={Math.round(2/3*100)} color={getColorTheme()}/>
          </RightBottomSide>
        </RightSide>
      </TrackerBody>
    </TrackerWrapper>
  );
};

export default observer(Tracker);
