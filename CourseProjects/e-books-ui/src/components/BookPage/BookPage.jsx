import { useParams } from "react-router-dom";
import Button from "../common/Button";
import {
  About,
  AboutWrapper,
  Author,
  BookPageWrapper,
  Count,
  CountWrapper,
  Description,
  Footer,
  Image,
  Reservations,
  Title,
} from "./BookPage.styles";
import { observer } from "mobx-react-lite";
import { useStore } from "../../store";
import { useEffect } from "react";
import ImageDisplay from "../common/BookCard/ImageDisplay/ImageDisplay";

const BookPage = () => {
  const { bookId } = useParams();
  const { bookStore } = useStore();

  const { getBook, book } = bookStore;

  useEffect(() => {
    getBook(bookId);
  }, []);

  return (
    <BookPageWrapper>
      <AboutWrapper>
        <ImageDisplay image={book?.image}/>
        <About>
          <Title>
          {book?.title}
          </Title>
          <Author>
            {book?.author}
          </Author>
          <Description>
            {book?.description}
          </Description>
        </About>
      </AboutWrapper>
      <Footer>
        <Reservations>Сейчас читают: Васили Иванов, Петя Петров</Reservations>
        <CountWrapper>
          <Count>В наличии: {book?.count}</Count>
          <Button content={"Забронировать"} />
        </CountWrapper>
      </Footer>
    </BookPageWrapper>
  );
};

export default observer(BookPage);
