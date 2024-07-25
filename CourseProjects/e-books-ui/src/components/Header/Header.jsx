import { useLocation, useNavigate, useParams } from "react-router-dom";

import Button from "../common/Button";
import { ButtonsWrapper, HeaderWrapper, ProductName } from "./Header.styles";
import { observer } from "mobx-react-lite";
import colorThemes from "../../colorThemes";
import { useState } from "react";
import BookPopup from "../common/BookPopup";
import { useStore } from "../../store";

const Header = () => {
  const { popupStore, bookStore } = useStore();
  const { setIsOpen, isOpen, clearStore, setPopupData } = popupStore;
  const { book } = bookStore;

  const navigate = useNavigate();

  const location = useLocation();
  let path = location.pathname;
  let maybeIsBook = path.length > 1 && !isNaN(path.substring(1, path.length));

  const openPopupHandler = () => {
    if (!maybeIsBook) {
      setIsOpen(true);
      return;
    }
    setPopupData(
      book.id,
      book.title,
      book.author,
      book.description,
      book.count,
      book.image
    );
    setIsOpen(true);
  };

  return (
    <HeaderWrapper>
      <ProductName onClick={() => navigate(`/`)}>
        Корпоративна библиотека
      </ProductName>
      <ButtonsWrapper>
        {!isOpen ? (
          <Button
            content={maybeIsBook ? "Изменить книгу" : "Добавить книгу"}
            color={colorThemes.colors.green}
            onClick={openPopupHandler}
          />
        ) : null}

        <Button content="Мои книги" onClick={() => navigate(`/your`)} />
      </ButtonsWrapper>
      <BookPopup
        open={isOpen}
        onCloseHandler={() => {
          setIsOpen(false);
          clearStore();
        }}
        // close={() => setOpenPopup(false)}
      />
    </HeaderWrapper>
  );
};

export default observer(Header);
