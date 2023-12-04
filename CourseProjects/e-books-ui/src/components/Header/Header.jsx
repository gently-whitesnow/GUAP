import { useNavigate, useParams } from "react-router-dom";

import Button from "../common/Button";
import { ButtonsWrapper, HeaderWrapper, ProductName } from "./Header.styles";
import { observer } from "mobx-react-lite";
import colorThemes from "../../colorThemes";
import { useState } from "react";
import BookPopup from "../common/BookPopup";

const Header = () => {
  const [openPopup, setOpenPopup] = useState(false);
  const navigate = useNavigate();

  const { bookId } = useParams();

  console.log(bookId);
  return (
    <HeaderWrapper>
      <ProductName onClick={() => navigate(`/`)}>
        Корпоративна библиотека
      </ProductName>
      <ButtonsWrapper>
        {!openPopup ? (
          <Button
            content={bookId ? "Изменить книгу" : "Добавить книгу"}
            color={colorThemes.colors.green}
            onClick={() => setOpenPopup(true)}
          />
        ) : null}

        <Button content="Мои книги" onClick={() => navigate(`/your`)} />
      </ButtonsWrapper>
      <BookPopup
        open={openPopup}
        onCloseHandler={() => setOpenPopup(false)}
        // close={() => setOpenPopup(false)}
      />
    </HeaderWrapper>
  );
};

export default observer(Header);
