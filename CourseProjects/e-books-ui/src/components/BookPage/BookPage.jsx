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

const BookPage = () => {
  const { bookId } = useParams();
  console.log(bookId)
  return (
    <BookPageWrapper>
      <AboutWrapper>
        <Image></Image>
        <About>
          <Title>Все закончится, а ты нет. Книга силы, утешения и поддержки</Title>
          <Author>Все закончится, а ты нет. Книга силы, утешения и поддержки</Author>
          <Description>
            Даже если вы никогда не имели дела с программированием, эта книга
            поможет вам освоить язык C# и научиться писать на нем программы
            любой сложности. Для читателей, которые уже знакомы с каким-либо
            языком программирования, процесс изучения C# только упростится, но
            иметь опыт программирования для чтения книги совершенно
            необязательно. .Из этой книги вы узнаете не только о типах,
            конструкциях и операторах языка C#, но и о ключевых концепциях
            объектно-ориентированного программирования, реализованных в этом
            языке, который в настоящее время представляет собой один из наиболее
            приспособленных для создания программ для Windows инструментов. Если
            вы в начале большого пути в программирование, смелее покупайте эту
            книгу: она послужит вам отличным путеводителем, который облегчит
            ваши первые шаги на этом длинном, но очень увлекательном пути.
            .Узнайте, как создать консольное приложение и что такое делегаты,
            события и интерфейсы! .C# - мощный язык программирования, который
            стал любимым инструментом программистов, работающих с Visual Studio,
            и эта книга поможет вам быстро и безболезненно освоить новейшую его
            версию. Вы научитесь создавать приложения для Windows, использовать
            графику, потоки, контейнеры, базы данных и многое другое, узнаете,
            что такое .NET Framework, полиморфизм, наследование и обобщенное
            программирование, а также изучите множество других важных и
            интересных вещей. .
          </Description>
        </About>
      </AboutWrapper>
      <Footer>
        <Reservations>Сейчас читают: Васили Иванов, Петя Петров</Reservations>
        <CountWrapper>
          <Count>В наличии: 2</Count>
          <Button content={"Забронировать"} />
        </CountWrapper>
        
      </Footer>
    </BookPageWrapper>
  );
};

export default observer(BookPage);
