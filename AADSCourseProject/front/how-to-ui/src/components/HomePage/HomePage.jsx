import { observer } from "mobx-react-lite";
import { HomePageWrapper } from "./HomePage.styles";
import { Squircle } from 'corner-smoothing'
import Tracker from "./Tracker/Tracker";

const HomePage = () => {
  return <HomePageWrapper>
    <Tracker/>
  </HomePageWrapper>;
};

export default observer(HomePage);
