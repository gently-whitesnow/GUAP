import theme from "../../theme";
import {
  Colorimetr,
  ColorimetrWrapper,
  HeaderContent,
  HeaderWrapper,
  Title,
} from "./Header.styles";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router";
import { useLocation } from "react-router-dom";
import Button from "../common/Button/Button";
import { useStore } from "../../store";

const Header = () => {
  const { colorStore } = useStore();
  const { getColorTheme, setColorTheme } = colorStore;

  const navigate = useNavigate();
  const onClickHandler = (path) => {
    navigate(path);
  };

  const location = useLocation();
  let path = location.pathname;

  return (
    <>
      {path !== "/" ? (
        <HeaderWrapper>
          <HeaderContent>
            <Button
              onClick={() =>
                onClickHandler(path.substring(0, path.lastIndexOf("/")))
              }
              content="Назад"
            />
            <Colorimetr color={getColorTheme()} onClick={setColorTheme} />
          </HeaderContent>
        </HeaderWrapper>
      ) : (
        <HeaderWrapper>
          <HeaderContent>
            <div />
            <ColorimetrWrapper>
              <Button
                onClick={() => onClickHandler("/edit?create")}
                content="Добавить курс"
              />
              <Colorimetr color={getColorTheme()} onClick={setColorTheme} />
            </ColorimetrWrapper>
          </HeaderContent>
        </HeaderWrapper>
      )}
    </>
  );
};

export default observer(Header);
