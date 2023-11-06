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
    this.isAuthorized = value;
  };

  isNotFound = false;

  setIsNotFound = (value) => {
    this.isNotFound = value;
  };

  getAuth = (userId, userName) => {
    api
      .getAuth(userId, userName)
      .then(({ data }) => {
        this.setIsLoading(false);
        this.authData = data;
        // небольшой костыль позволяющий дождатся проставления кук перед последующими запросами
        setTimeout(this.setIsAuthorized(true), 1000);
      })
      .catch((err) => {
        this.setIsLoading(false);

        console.error(err);
      });
  };
}

export default StateStore;
