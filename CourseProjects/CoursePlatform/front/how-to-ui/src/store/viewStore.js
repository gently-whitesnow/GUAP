import { makeAutoObservable, configure } from "mobx";
import api from "../api/api";

class ViewStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  value = {};

  setValue = (value) => {
    this.value = value;
  };

  addApprovedView = (courseId, articleId) => {
    api
      .postApprovedView(courseId, articleId)
      .then(({ data }) => {

      })
      .catch((err) => {
        console.error(err);
      });
  };
}

export default ViewStore;
