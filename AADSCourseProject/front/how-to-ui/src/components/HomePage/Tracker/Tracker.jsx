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
  ImageWrapper,
} from "./Tracker.styles";

import Image from "react-graceful-image";
import theme from "../../../theme";
import { useNavigate } from "react-router";
import ProgressBar from "../ProgressBar/ProgressBar";
import { useStore } from "../../../store";

const Tracker = () => {
  const { colorStore, summaryStore } = useStore();
  const { currentColorTheme } = colorStore;
  const { summaryData } = summaryStore;

  const navigate = useNavigate();
  const onClickHandler = () => {
    navigate(`/${summaryData?.last_course?.id}`);
  };

  let percent =
    summaryData?.last_course?.articles_count == 0
      ? 0
      : Math.round(
          (summaryData?.last_course?.user_approved_views /
            summaryData?.last_course?.articles_count) *
            100
        );

  return (
    <TrackerWrapper>
      <TrackerBody onClick={onClickHandler}>
        <LeftSide>
          <LeftSideImage color={currentColorTheme}>
            <ImageWrapper>
              {summaryData?.last_course?.image ? (
                <img src={summaryData?.last_course?.image}></img>
              ) : null}
            </ImageWrapper>
          </LeftSideImage>
        </LeftSide>
        <RightSide>
          <RightUpSide>
            <Title color={currentColorTheme}>
              {summaryData?.last_course?.title}
            </Title>
            <Description>{summaryData?.last_course?.description}</Description>
          </RightUpSide>
          <RightBottomSide>
            <ContinueButton
              color={currentColorTheme}
              className="continue-button"
            >
              Продолжить
            </ContinueButton>
            <ProgressBar percents={percent} color={currentColorTheme} />
          </RightBottomSide>
        </RightSide>
      </TrackerBody>
    </TrackerWrapper>
  );
};

export default observer(Tracker);
