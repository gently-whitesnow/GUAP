import { useNavigate } from "react-router-dom";
import Button from "../common/Button";
import { ButtonsWrapper, HeaderWrapper, ProductName } from "./Header.styles";
import { observer } from "mobx-react-lite";
import colorThemes from "../../colorThemes";

const Header = () => {
  const navigate = useNavigate();

  return (
    <HeaderWrapper>
      <ProductName onClick={() => navigate(`/`)}>
        Корпоративна библиотека
      </ProductName>
      <ButtonsWrapper>
        <Button content="Добавить книгу" color={colorThemes.colors.green} />
        <Button content="Мои книги" onClick={() => navigate(`/your`)} />
      </ButtonsWrapper>
    </HeaderWrapper>
  );
};

export default observer(Header);
