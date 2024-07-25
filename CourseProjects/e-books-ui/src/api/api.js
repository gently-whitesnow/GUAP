import axios from "axios";
import { camelizeKeys, decamelizeKeys , camelize} from "humps";

export class Api {
  constructor() {
    this.client = axios.create();
    // this.client.defaults.baseURL = "https://nginx-proxy-server.ru/gw/";
    this.client.defaults.baseURL = "http://localhost:1234/";
    this.client.defaults.headers["Access-Control-Allow-Origin"] = "*";
    this.client.defaults.withCredentials = true;
    this.client.timeout = 3000;

    this.client.interceptors.response.use((response) => {
      if (
        response.data &&
        response.headers["content-type"].includes("application/json")
      ) {
        response.data = camelizeKeys(response.data);
      }

      return response;
    });

    // Axios middleware to convert all api requests to snake_case
    this.client.interceptors.request.use((config) => {
      const newConfig = { ...config };
      if (newConfig.headers["Content-Type"] === "multipart/form-data")
        return newConfig;

      if (config.params) {
        newConfig.params = decamelizeKeys(config.params);
      }

      if (config.data) {
        newConfig.data = decamelizeKeys(config.data);
      }
      return newConfig;
    });
  }

  clientWrapper = (method, url, data, config = {}) => {
    const clientResult = this.client[method](url, data, config);
    return clientResult;
  };

  upsertBook = (id, title, description, author, count, image) => {
    const formData = new FormData();
    if (id !== undefined) {
      formData.append("Id", id);
    }

    if (image !== undefined) {
      formData.append("File", image);
    }

    formData.append("Title", title);
    formData.append("Description", description);
    formData.append("Author", author);
    formData.append("Count", count);

    return this.clientWrapper("post", "v1/books", formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
  };

  // auth
  // http://localhost:5173/auth?userId=1&userName=testSanya&userRole=1
  getFakeAuth = (userId, userName, userRole) =>
    this.clientWrapper(
      "get",
      `api/fakeauth?userId=${userId}&userName=${userName}&userRole=${userRole}`
    );

  getAuth = () =>
    this.clientWrapper(
      "get",
      `api/auth`
    );

  // books

  getSummaryBooks = (searchQuery, isAvailable, skip, take) =>
    this.clientWrapper("post", `v1/books/search`,{
      searchQuery,
      isAvailable,
      skip,
      take
    });

    getBook = (bookId) =>
    this.clientWrapper("get", `v1/books/${bookId}`);

}
const api = new Api();

export default api;
