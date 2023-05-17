import { makeAutoObservable, configure } from "mobx";
import { NavigateToAuthorize } from "./navigateHelper";
import api from "../api/api";

class ArticleStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  isLoading = false;

  setIsLoading = (value) => {
    this.isLoading = value;
  };

  articleData = {};

  getArticle = () => {
    api
      .getArticle()
      .then(({ data }) => {
        this.setIsLoading(false);

        console.log(data);
        this.articleData = data;
      })
      .catch((err) => {
        this.setIsLoading(false);

        console.error(err);
        if (err.response?.status === 401) {
          NavigateToAuthorize();
        }
      });
  };
}

export default ArticleStore;
