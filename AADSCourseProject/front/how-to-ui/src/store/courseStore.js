import { makeAutoObservable, configure } from "mobx";
import { NavigateToAuthorize } from "./navigateHelper";
import api from "../api/api";

class CourseStore {
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

  courseData = [];

  id = undefined;
  title = "Введите название курсафывдфывофды фвдлы фd";
  description =
    "It is important to define your styled components outside of the render method, otherwise it will be recreated on every single render pass. Defining a styled component within the render method will thwart caching and drastically slow down rendering speed, and should be avoided.";
  path = "";
  createdAt = "";
  updatedAt = "";
  isCourseEditing = false;
  isCourseContributor = false;

  contributors = [];
  articles = [];

  image = undefined;

  setTitle = (text) => {
    this.title = text;
  };

  setDescription = (text) => {
    this.description = text;
  };

  setCourseData = (data) => {
    this.title = data.title;
    this.description = data.description;
    this.articles = data.articles;
    this.id = data.id;
    this.path = data.path;
    this.createdAt = data.created_at;
    this.updatedAt = data.updated_at;
    this.contributors = data.contributors;
  };

  courseActionError = "";

  setCourseActionError = (error) => {
    this.courseActionError = error;
  };

  articleActionError = [];

  setArticleActionError = (error) => {
    this.articleActionError = error;
  };

  addArticle = () => {
    this.articles.push({
      id: this.articles.length != 0 ? this.articles.slice(-1).id + 1 : 0,
      title: "Введите название статьи",
      path: "/адрес-статьи",
      isAuthor: true,
      isRead: true,
      isArticleEditing: true,
    });
  };

  updateArticle = (id, title, path) => {
    this.articles = this.articles.map((article) => {
      console.log(article);
      if (article.id === id) {
        article.title = title;
        article.path = path;
        article.isArticleEditing = false;
        article.isAuthor = true;
      }
      return article;
    });
  };

  setIsCourseEditing = (value) => {
    this.isCourseEditing = value;
  };

  getCourse = (path) => {
    api
      .getCourse(path)
      .then(({ data }) => {
        this.setIsLoading(false);
        this.setCourseData(data);
        console.log(data);
      })
      .catch((err) => {
        this.setIsLoading(false);
        // todo remove
        this.isCourseEditing = true;
        this.isCourseContributor = true;
        //

        console.error(err);
        if (err.response?.status === 401) {
          NavigateToAuthorize();
        } else if (err.response?.status === 404) {
          this.isCourseEditing = true;
          this.isCourseContributor = true;
        }
      });
  };

  upsertCourse = (courseId, title, description, path, image) => {
    api
      .upsertCourse(courseId, title, description, path, image)
      .then(({ data }) => {
        this.setIsLoading(false);
        this.setCourseData(data);
        console.log(data);
        this.setIsCourseEditing(false);
      })
      .catch((err) => {
        this.setIsLoading(false);
        console.error(err);
        this.setCourseActionError(err.response?.data?.message);
        this.setCourseActionError("save Internal server error");
        if (err.response?.status === 401) {
          NavigateToAuthorize();
        }
        //todo remove
        this.setIsCourseEditing(false);
      });
  };

  deleteCourse = (courseId) => {
    api
      .deleteCourse(courseId)
      .then(({ data }) => {
        this.setIsLoading(false);
        // todo redirect and cleaning
      })
      .catch((err) => {
        this.setIsLoading(false);
        console.error(err);
        this.setCourseActionError(err.response?.data?.message);
        this.setCourseActionError("delete Internal server error");
        if (err.response?.status === 401) {
          NavigateToAuthorize();
        }
        //todo remove
        this.setIsCourseEditing(false);
      });
  };
}

export default CourseStore;
