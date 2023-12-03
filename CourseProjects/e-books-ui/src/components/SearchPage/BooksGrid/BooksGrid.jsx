import booksSummaryMock from "../../../mocks/books_summary_mock";
import BookCard from "../../common/BookCard";
import { BooksGridWrapper } from "./BooksGrid.styles";
import { observer } from "mobx-react-lite";

import { useNavigate } from "react-router-dom";

const BooksGrid = () => {
  const navigate = useNavigate();
  const onBookClickHandler = (id) => {
    navigate(`/${id}`);
  };

  return (
    <BooksGridWrapper>
      {booksSummaryMock?.books?.map((data) => {
        return (
          <BookCard
            title={data.title}
            id={data.id}
            onClick={onBookClickHandler}
          />
        );
      })}
    </BooksGridWrapper>
  );
};

export default observer(BooksGrid);
