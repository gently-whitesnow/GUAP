import { makeAutoObservable, configure } from "mobx";
import api from "../api/api";

class ArticleStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    this.stateStore = rootStore.stateStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  isLoading = false;

  setIsLoading = (value) => {
    this.isLoading = value;
  };

  articleActionError = [];

  setArticleActionError = (error) => {
    this.articleActionError = error;
  };

  article = {};

  setArticleData = (data) => {
    this.article= {
        id: data.article.id,
        courseId: data.article.course_id,
        title: data.article.title,
        createdAt: data.article.created_at,
        updatedAt: data.article.updated_at,
        author: { userId: data.article.author?.user_id, name: data.article.author?.name },
        isAuthor: data.article.is_author,
        isViewed: data.article.is_viewed,
      }
      this.setFiles(data.files)
  };

  setArticleIsViewed = (value) => {
    this.article.isViewed = value;
  }

  setFiles = (files) => {
    if(files === undefined || files.length === 0) return;
    let file = files[0];
    let fileData = atob(file);
    let fileByteArray = new Uint8Array(fileData.length);
    for (let i = 0; i < fileData.length; i++) {
      fileByteArray[i] = fileData.charCodeAt(i);
    }
    let fileMdData = new Blob([fileByteArray], { type: 'application/octet-stream' });
    this.article.fileURL = URL.createObjectURL(fileMdData);
    this.rootStore.stateStore.setIsLoading(false);
  }

  getArticle = (courseId, articleId) => {
    this.clearStore();
    this.rootStore.stateStore.setIsLoading(true);
    api
      .getArticle(courseId, articleId)
      .then(({ data }) => {
         this.setArticleData(data)
      })
      .catch((err) => {
        this.rootStore.stateStore.setIsLoading(false);

        console.error(err);
        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
        this.setArticleActionError(err.response?.data?.reason)
      });
  };

  clearStore = () => {
    this.article = {};
  }
}

export default ArticleStore;
