import {  HeaderWrapper, Title } from "./Header.styles";
import { observer } from "mobx-react-lite";

const Header = () => {
  return <HeaderWrapper>
    <Title>
    How to
    </Title>
    </HeaderWrapper>;
};

export default observer(Header);
