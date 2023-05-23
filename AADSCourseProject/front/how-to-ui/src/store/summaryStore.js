import { makeAutoObservable, configure } from "mobx";
import api from "../api/api";

class SummaryStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    this.stateStore = rootStore.stateStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  isLoading = false;

  summaryData = {};

  setIsLoading = (value) => {
    this.isLoading = value;
  };

  getSummaryCourses = () => {
    api
      .getSummaryCourses()
      .then(({ data }) => {
        this.setIsLoading(false);

        console.log(data);
        this.summaryData = data;
      })
      .catch((err) => {
        this.setIsLoading(false);

        console.error(err);
        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
      });
  };
}

export default SummaryStore;
