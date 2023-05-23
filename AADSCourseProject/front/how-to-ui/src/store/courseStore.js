import { makeAutoObservable, configure } from "mobx";
import { NavigateToAuthorize } from "./navigateHelper";
import api from "../api/api";

class CourseStore {
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

  courseData = [];

  id = undefined;
  title = "Введите название курса";
  description =
    "It is important to define your styled components outside of the render method, otherwise it will be recreated on every single render pass. Defining a styled component within the render method will thwart caching and drastically slow down rendering speed, and should be avoided.";
  path = "";
  createdAt = "";
  updatedAt = "";

  contributors = [];
  articles = [];

  image = undefined;

  setTitle = (text) => {
    this.title = text;
  };

  setDescription = (text) => {
    this.description = text;
  };

  setCourseData = (course) => {
    this.title = course.title;
    this.description = course.description;
    this.articles = course.articles?.map((a) => {
      return {
        id: a.id,
        courseId: a.course_id,
        title: a.title,
        createdAt: a.created_at,
        updatedAt: a.updated_at,
        author: { userId: a.author?.user_id, name: a.author?.name },
        isAuthor: a.is_author,
        isViewed: a.is_viewed,
      };
    });
    this.id = course.id;
    this.path = course.path;
    this.createdAt = course.created_at;
    this.updatedAt = course.updated_at;
    this.contributors = course.contributors;
    this.isAuthor = course.is_author;
  };

  setArticleData = (article) => {
    if(this.articles === undefined) this.articles = [];

    this.articles.push( {
        id: article.id,
        courseId: article.course_id,
        title: article.title,
        createdAt: article.created_at,
        updatedAt: article.updated_at,
        author: { userId: article.author?.user_id, name: article.author?.name },
        isAuthor: article.is_author,
      }
    );
  };

  courseActionError = "";

  setCourseActionError = (error) => {
    this.courseActionError = error;
  };

  newArticle = undefined;

  addNewArticle = () => {
    console.log("click");
    if (this.newArticle !== undefined) {
      return;
    }
    this.newArticle = {
      title: "Введите название страницы",
      isAuthor: true,
      isArticleEditing: true,
      isNewArticle: true,
      courseId: this.id,
    };
  };

  setNewArticle = (value) => {
    this.newArticle = value;
  };

  isCourseEditing = false;

  setIsCourseEditing = (value) => {
    this.isCourseEditing = value;
  };

  isAuthor = false;
  setIsCourseContributor = (value) => {
    this.isAuthor = value;
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

        console.error(err);
        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
        let course = {
          title: err.response?.data?.error,
          description: err.response?.data?.reason,
        };
        this.setCourseData(course);
      });
  };

  upsertCourse = (courseId, title, description, image, callback) => {
    api
      .upsertCourse(courseId, title, description, image)
      .then(({ data }) => {
        this.setIsLoading(false);
        this.setCourseData(data);
        console.log(data);
        this.setIsCourseEditing(false);
        callback(data.id);
      })
      .catch((err) => {
        this.setIsLoading(false);
        console.error(err);
        this.setCourseActionError(err.response?.data?.reason);
        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
        callback();
      });
  };

  deleteCourse = (courseId, callback) => {
    api
      .deleteCourse(courseId)
      .then(({ data }) => {
        this.setIsLoading(false);
        // todo redirect and cleaning
        callback(true);
      })
      .catch((err) => {
        this.setIsLoading(false);
        console.error(err);
        this.setCourseActionError(err.response?.data?.reason);
        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
        callback(false);
      });
  };

  upsertArticle = (
    articleId,
    courseId,
    title,
    file,
    isNewArticle,
    errorCallback
  ) => {
    api
      .upsertArticle(articleId, courseId, title, file)
      .then(({ data }) => {
        console.log(data);

        if (isNewArticle) {
          this.setNewArticle(undefined)
          this.setArticleData(data);
        }
      })
      .catch((err) => {
        console.error(err);

        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
        errorCallback(err.response?.data?.reason);
      });
  };

  deleteArticle = (courseId, articleId, errorCallback) => {
    api
      .deleteArticle(courseId, articleId)
      .then(({ data }) => {
        this.articles = this.articles.filter((obj) => obj.id !== articleId);
      })
      .catch((err) => {
        console.error(err);
        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
        errorCallback(err.response?.data?.reason);
      });
  };
}

export default CourseStore;
