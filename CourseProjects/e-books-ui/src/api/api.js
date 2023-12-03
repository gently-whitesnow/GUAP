import axios from "axios";
import { camelizeKeys, decamelizeKeys , camelize} from "humps";

export class Api {
  constructor() {
    this.client = axios.create();
    // this.client.defaults.baseURL = "https://nginx-proxy-server.ru/gw/";
    this.client.defaults.baseURL = "http://localhost:1999/";
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

  // auth
  // http://localhost:3000/auth?userId=69550bf7-e7e1-4650-801d-e9159530decb&userName=testSanya&userRole=1
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

  getSummaryBooks = () =>
    this.clientWrapper("get", `v1/books`);

}
const api = new Api();

export default api;
