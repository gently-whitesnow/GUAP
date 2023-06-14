import { makeAutoObservable, configure } from "mobx";
import api from "../api/api";
import { setFile } from "../helpers/IOHelper";

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

  setSummaryCoursesData = (data) => {
    data.courses?.forEach((course) => {
      course.image = setFile(course.files?.shift());
    });
    data.last_course.image = data.courses?.find(
      (course) => course.id === data.last_course.id
    ).image;

    this.summaryData = data;
    this.summaryStaticData = data;
  };

  getSummaryCourses = () => {
    this.clearStore();
    this.rootStore.stateStore.setIsLoading(true);
    api
      .getSummaryCourses()
      .then(({ data }) => {
        this.setSummaryCoursesData(data);
        this.rootStore.stateStore.setIsLoading(false);
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
