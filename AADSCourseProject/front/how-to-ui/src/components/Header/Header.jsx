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
  const { colorStore, courseStore } = useStore();
  const { getColorTheme, setColorTheme } = colorStore;
  const { addArticle } = courseStore;

  const navigate = useNavigate();
  const onClickHandler = (path) => {
    navigate(path);
  };

  const location = useLocation();
  let path = location.pathname;

  let maybeIsCourse = path.length > 1;
  let maybeIsArticle = path.substring(1, path.length).includes("/");

  const getCurrentHeader = () => {
    if (maybeIsCourse && maybeIsArticle) {
      return (
        <>
          <Button
            onClick={() =>
              onClickHandler(path.substring(0, path.lastIndexOf("/")))
            }
            content="Назад"
          />
          <Colorimetr color={getColorTheme()} onClick={setColorTheme} />
        </>
      );
    } else if (maybeIsCourse) {
      return (
        <>
          <Button
            onClick={() =>
              onClickHandler(path.substring(0, path.lastIndexOf("/")))
            }
            content="Назад"
          />
          <ColorimetrWrapper>
            <Button
              onClick={() => addArticle()}
              content="Добавить статью"
            />
            <Colorimetr color={getColorTheme()} onClick={setColorTheme} />
          </ColorimetrWrapper>
        </>
      );
    }
    return (
      <>
        <div />
        <ColorimetrWrapper>
          <Button
            onClick={() => onClickHandler("/edit?create")}
            content="Добавить курс"
          />
          <Colorimetr color={getColorTheme()} onClick={setColorTheme} />
        </ColorimetrWrapper>
      </>
    );
  };

  return (
    <>
      <HeaderWrapper>
        <HeaderContent>{getCurrentHeader()}</HeaderContent>
      </HeaderWrapper>
    </>
  );
};

export default observer(Header);
