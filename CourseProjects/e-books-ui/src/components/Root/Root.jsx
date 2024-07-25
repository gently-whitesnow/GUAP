import BookPage from "../BookPage";
import SearchPage from "../SearchPage";
import YourPage from "../YourPage";
import AuthPage from "../AuthPage";
import ScrollToTop from "../common/ScrollToTop";

import { observer } from "mobx-react-lite";
import { Routes, Route, BrowserRouter } from "react-router-dom";
import { RootLimiter, RootWrapper } from "./Root.styles";
import Header from "../Header";

const Root = () => {
  return (
    <BrowserRouter>
      <Header />
      <ScrollToTop />
      <RootWrapper>
        <RootLimiter>
          <Routes>
            <Route path="/" element={<SearchPage />} />
            {/* <Route
            path="/auth"
            element={
              <AuthPage/>
            }
          /> */}
            <Route path="/:bookId" element={<BookPage />} />
            <Route path="/your" element={<YourPage />} />
            <Route path="/auth" element={<AuthPage />} />
          </Routes>
        </RootLimiter>
      </RootWrapper>
    </BrowserRouter>
  );
};

export default observer(Root);
