import axios from "axios";

export class Api {
  constructor() {
    this.client = axios.create();
    // this.client.defaults.baseURL = "https://thousandwords.ru/";
    this.client.defaults.baseURL = "http://localhost:1999/";
    this.client.defaults.headers["Access-Control-Allow-Origin"] = "*";
    this.client.defaults.headers["Content-Type"] =
      "application/json;charset=UTF-8";
    this.client.defaults.withCredentials = true;
    this.client.timeout = 3000;
  }

  clientWrapper = (method, url, data, config = {}) => {
    const clientResult = this.client[method](url, data, config);
    return clientResult;
  };

  // summary

  getSummaryCourses = () => this.clientWrapper("get", `api/summary/courses`);

  // courses

  getCourse = (path) => this.clientWrapper("get", `api/courses/${path}`);
  upsertCourse = (courseId, title, description, path, image) =>
    this.clientWrapper("post", `api/courses`, {
      course_id: courseId,
      title: title,
      description: description,
      path: path,
      image: image,
    });
  deleteCourse = (id) => this.clientWrapper("delete", `api/courses/${id}`);

  // articles

  getArticle = (coursePath, articlePath) =>
    this.clientWrapper("get", `api/articles/${coursePath}/${articlePath}`);
  upsertArticle = (articleId, courseId, title, fullPath, files) =>
    this.clientWrapper("post", `api/articles`, {
      article_id: articleId,
      course_id: courseId,
      title: title,
      full_path: fullPath,
      files: files,
    });
  deleteArticle = (id) => this.clientWrapper("delete", `api/articles/${id}`);

  // views

  postApprovedView = (articleId) =>
    this.clientWrapper("post", `api/views/approved`, {
      article_id: articleId
    });
}
const api = new Api();

export default api;
