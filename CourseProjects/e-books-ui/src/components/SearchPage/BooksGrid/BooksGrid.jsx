import BookCard from "../../common/BookCard";
import { BooksGridWrapper } from "./BooksGrid.styles";
import { observer } from "mobx-react-lite";

import { useNavigate } from "react-router-dom";

const BooksGrid = ({data}) => {
  const navigate = useNavigate();
  const onBookClickHandler = (id) => {
    navigate(`/${id}`);
  };

  return (
    <BooksGridWrapper>
      {data?.books?.map((book) => {
        return (
          <BookCard
            title={book.title}
            id={book.id}
            onClick={onBookClickHandler}
          />
        );
      })}
    </BooksGridWrapper>
  );
};

export default observer(BooksGrid);
