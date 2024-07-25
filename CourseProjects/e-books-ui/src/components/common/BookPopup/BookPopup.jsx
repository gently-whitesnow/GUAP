import {
  About,
  AboutWrapper,
  Author,
  BodyWrapper,
  BookContentWrapper,
  BookPopupWrapper,
  Description,
  HeaderTitle,
  HeaderWrapper,
  Image,
  PopupFooterWrapper,
  StyledPopup,
  Title,
} from "./BookPopup.styles";
import CrossIcon from "../../../images/cross.svg?react";
import { observer } from "mobx-react-lite";

import Button from "../Button";
import colorThemes from "../../../colorThemes";
import Textarea from "../Textarea/Textarea";
import { useStore } from "../../../store";
import InputNumber from "../InputNumber/InputNumber";
import { useRef } from "react";
import ImageButton from "../ImageButton/ImageButton";

const BookPopup = ({ open, onCloseHandler }) => {
  const imageInputRef = useRef(null);
  const { popupStore } = useStore();
  const {
    setTitle,
    title,
    setDescription,
    description,
    setAuthor,
    author,
    setCount,
    count,
    upsertBook,
    image,
    setImage,
    isNew,
  } = popupStore;

  const onUpsertClichHandler = () => {
    setImage(imageInputRef.current?.files[0]);
    upsertBook();
  };

  return (
    <BookPopupWrapper>
      <StyledPopup
        open={open}
        onClose={onCloseHandler}
        position="center"
        closeOnDocumentClick={false}
        lockScroll
      >
        <BookContentWrapper>
          <HeaderWrapper>
            <HeaderTitle>
              {isNew ? "Добавление книги" : "Изменение книги"}
            </HeaderTitle>
            <img src={CrossIcon} onClick={onCloseHandler} />
          </HeaderWrapper>
          <BodyWrapper>
            <AboutWrapper>
              <ImageButton imageRef={imageInputRef} image={image} />
              <About>
                <Textarea
                  value={title}
                  onChange={(e) => setTitle(e.target.value)}
                  maxLength={100}
                  height={"80px"}
                  placeholder={"Название книги"}
                  fontsize={"30px"}
                ></Textarea>
                <Textarea
                  value={author}
                  onChange={(e) => setAuthor(e.target.value)}
                  maxLength={100}
                  height={"60px"}
                  placeholder={"Автор книги"}
                  fontsize={"24px"}
                ></Textarea>
                <Textarea
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  maxLength={500}
                  height={"300px"}
                  placeholder={"Описание книги"}
                  fontsize={"20px"}
                ></Textarea>

                <InputNumber
                  placeholder={"Кол-во книг"}
                  value={count}
                  onChange={(e) => setCount(e.target.value)}
                ></InputNumber>
              </About>
            </AboutWrapper>
          </BodyWrapper>
          <PopupFooterWrapper>
            <Button
              content={isNew ? "Добавить книгу" : "Изменить книгу"}
              color={colorThemes.colors.green}
              onClick={onUpsertClichHandler}
            />
          </PopupFooterWrapper>
        </BookContentWrapper>
      </StyledPopup>
    </BookPopupWrapper>
  );
};

export default observer(BookPopup);
