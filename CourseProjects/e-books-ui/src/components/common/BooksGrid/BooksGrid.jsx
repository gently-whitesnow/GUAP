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
      {data?.books?.map((data) => {
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
