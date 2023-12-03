import { Input, SearchInputWrapper } from "./SearchInput.styles";
import { observer } from "mobx-react-lite";

const SearchInput = () => {
  return (
    <SearchInputWrapper><Input/></SearchInputWrapper>
  );
};

export default observer(SearchInput);
