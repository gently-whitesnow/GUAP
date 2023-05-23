import ArticlePage from "../ArticlePage/ArticlePage";
import AuthPage from "../AuthPage/AuthPage";
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
            path="/auth"
            element={
              <AuthPage/>
            }
          />
          <Route
            path="/:courseId"
            element={
              <CoursePage/>
            }
          />
          <Route
            path="/:courseId/:articleId"
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
