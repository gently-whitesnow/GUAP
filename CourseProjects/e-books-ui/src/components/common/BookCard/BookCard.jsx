import { BookCardWrapper, ImageWrapper, TitleWrapper } from "./BookCard.styles";
import { observer } from "mobx-react-lite";

const BookCard = ({ id, title, onClick }) => {
  const maxTitleLength = 43;
  const getLimitedTitle = () => {
    let lastSpace = maxTitleLength;
    if (title.length <= maxTitleLength) return title;

    for (let index = 0; index < maxTitleLength; index++) {
      if (title[index] === " ") lastSpace = index;
    }
    return title?.slice(0, lastSpace);
  };

  return (
    <BookCardWrapper className="book-card" onClick={() => onClick(id)}>
      <ImageWrapper />
      <TitleWrapper>{getLimitedTitle()}</TitleWrapper>
    </BookCardWrapper>
  );
};

export default observer(BookCard);
