import booksSummaryMock from "../../mocks/books_summary_mock";
import BooksGrid from "../common/BooksGrid/BooksGrid";

import SearchInput from "./SearchInput";
import { SearchInputWrapper, SearchPageWrapper } from "./SearchPage.styles";
import { observer } from "mobx-react-lite";

const SearchPage = () => {
  return (
    <SearchPageWrapper>
      <SearchInputWrapper>
        <SearchInput />
      </SearchInputWrapper>
      <BooksGrid data={booksSummaryMock} />
    </SearchPageWrapper>
  );
};

export default observer(SearchPage);
