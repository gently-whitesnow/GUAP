import { makeAutoObservable, configure } from "mobx";
import api from "../api/api";

class StateStore {
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

  authData = {};
  isAuthorized = true;

  setIsAuthorized = (value) => {
    console.log(value);
    this.isAuthorized = value;
  }

  isNotFound = false;

  setIsNotFound = (value) => {
    this.isNotFound = value;
  }

  getAuth = (userId, userName) => {
    api
      .getAuth(userId, userName)
      .then(({ data }) => {
        this.setIsLoading(false);
        console.log(data);
        this.authData = data;
        this.setIsAuthorized(true);
      })
      .catch((err) => {
        this.setIsLoading(false);

        console.error(err);
      });
  };
}

export default StateStore;
