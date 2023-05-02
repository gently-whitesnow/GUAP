import ArticlePage from "../ArticlePage/ArticlePage";
import CoursePage from "../CoursePage/CoursePage";
import Header from "../Header/Header";
import HomePage from "../HomePage/HomePage";
import ScrollToTop from "../common/ScrollToTop";
import { ContentWrapper } from "./Content.styles";
import { observer } from "mobx-react-lite";
import { Routes, Route, BrowserRouter } from "react-router-dom";

const Content = () => {
  return (
    <BrowserRouter>
        <Header />
        <ScrollToTop />
        <ContentWrapper>
          
        <Routes>
          <Route
            path="/"
            element={
              <HomePage/>
            }
          />
          <Route
            path="/:course"
            element={
              <CoursePage/>
            }
          />
          <Route
            path="/:course/:article"
            element={
              <ArticlePage/>
            }
          />
        </Routes>

        </ContentWrapper>
      </BrowserRouter>
  );
};

export default observer(Content);
