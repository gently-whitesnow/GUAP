import { observer } from "mobx-react-lite";
import {
  Input,
  SearchInputHolder,
  SearchInputWrapper,
} from "./SearchInput.styles";
import { useStore } from "../../../store";
import { ReactComponent as SearchIcon } from "../../../icons/search.svg";

const SearchInput = (props) => {
  const { colorStore, summaryStore } = useStore();
  const { currentColorTheme } = colorStore;
  const { summaryStaticData, setSummaryCoursesData } = summaryStore;

  const onInputHandler = (e) => {
    let search = e.target.value;
    if (!search) {
      setSummaryCoursesData(summaryStaticData.courses);
      return;
    }
    setSummaryCoursesData(
      summaryStaticData.courses?.filter((e) => {
        if (IsSuetableTitle(search.toLowerCase(), e.title.toLowerCase()))
          return e;
      })
    );
  };

  const IsSuetableTitle = (search, title) => {
    let searchIndex = 0;
    for (let titleIndex = 0; titleIndex < title.length; titleIndex++) {
      if (title[titleIndex] === search[searchIndex]) {
        searchIndex++;
      } else {
        searchIndex = 0;
      }
      if (searchIndex === search.length) return true;
    }
    return false;
  };

  return (
    <SearchInputHolder>
      <SearchInputWrapper>
        <Input color={currentColorTheme} onInput={onInputHandler}></Input>
        <SearchIcon color={currentColorTheme} />
      </SearchInputWrapper>
    </SearchInputHolder>
  );
};

export default observer(SearchInput);
