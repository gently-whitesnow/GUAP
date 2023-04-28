import Header from "../Header/Header";
import HomePage from "../HomePage/HomePage";
import { ContentWrapper } from "./Content.styles";
import { observer } from "mobx-react-lite";

const Content = () => {
  return (
    <>
      <Header />
      <ContentWrapper>
        <HomePage />
      </ContentWrapper>
    </>
  );
};

export default observer(Content);
