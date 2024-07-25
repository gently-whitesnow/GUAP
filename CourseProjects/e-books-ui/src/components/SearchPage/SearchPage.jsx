import { useEffect } from "react";
import booksSummaryMock from "../../mocks/books_summary_mock";
import { useStore } from "../../store";
import BooksGrid from "../common/BooksGrid/BooksGrid";

import SearchInput from "./SearchInput";
import { SearchInputWrapper, SearchPageWrapper } from "./SearchPage.styles";
import { observer } from "mobx-react-lite";

const SearchPage = () => {

  const { summaryStore } = useStore();
  const { getSummaryBooks, summaryData } = summaryStore;

  useEffect(() => {
    getSummaryBooks();
  }, []);


  return (
    <SearchPageWrapper>
      <SearchInputWrapper>
        <SearchInput />
      </SearchInputWrapper>
      <BooksGrid data={summaryData} />
    </SearchPageWrapper>
  );
};

export default observer(SearchPage);
