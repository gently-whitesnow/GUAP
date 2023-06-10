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

  summaryData = {};
  summaryStaticData = {};

  setSummaryCoursesData = (courses) => {
    this.summaryData.courses = courses;
  };

  getSummaryCourses = () => {
    this.rootStore.stateStore.setIsLoading(true);
    api
      .getSummaryCourses()
      .then(({ data }) => {
        this.rootStore.stateStore.setIsLoading(false);

        console.log(data);
        this.summaryData = data;
        this.summaryStaticData = data;
      })
      .catch((err) => {
        this.rootStore.stateStore.setIsLoading(false);

        console.error(err);
        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
      });
  };

  clearStore = () => {
    this.summaryData = {};
    this.summaryStaticData = {};
  };
}

export default SummaryStore;
