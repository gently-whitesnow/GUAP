import booksSummaryMock from "../../mocks/books_summary_mock";
import BooksGrid from "../common/BooksGrid/BooksGrid";
import { YourPageWrapper } from "./YourPage.styles";
import { observer } from "mobx-react-lite";

const YourPage = () => {
  return (
    <YourPageWrapper>
      <BooksGrid data={booksSummaryMock} />
    </YourPageWrapper>
  );
};

export default observer(YourPage);
