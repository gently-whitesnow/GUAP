import { observer } from "mobx-react-lite";
import {
  TrackerWrapper,
  StyledSquircle,
  LeftSide,
  RightSide,
  Description,
  Title,
  ProgressBar,
  ProgressBarWrapper,
  ProgressBarTitle,
  Bar,
  RightUpSide,
  RightBottomSide,
  LeftSideStyledSquircle,
} from "./Tracker.jsx.styles";
import { Squircle } from "corner-smoothing";
import Image from "react-graceful-image";

const Tracker = () => {
  return (
    <TrackerWrapper>
      <Squircle cornerRadius={40} borderWidth={1} as={StyledSquircle}>
        <LeftSide>
          <Squircle
            cornerRadius={40}
            borderWidth={1}
            as={LeftSideStyledSquircle}
          />
        </LeftSide>
        <RightSide>
          <RightUpSide>
            <Title>C++ для сишарпников</Title>
            <Description>
              Перед вами учебник по основам языка C++. При его создании мы
              вдохновлялись специализацией «Искусство разработки на современном
              C++» на «Курсере», созданной при поддержке Яндекса, и курсом
              «Основы и методология программирования», который читался в
              2014–2021 годах на факультете компьютерных наук ВШЭ.
            </Description>
          </RightUpSide>
          <RightBottomSide>
            <ProgressBarWrapper>
              <ProgressBarTitle>2/5</ProgressBarTitle>
              <ProgressBar>
                <Bar className="active first">&nbsp;</Bar>
                <Bar className="active">&nbsp;</Bar>
                <Bar className="last">&nbsp;</Bar>
              </ProgressBar>
            </ProgressBarWrapper>
          </RightBottomSide>
        </RightSide>
      </Squircle>
    </TrackerWrapper>
  );
};

export default observer(Tracker);
