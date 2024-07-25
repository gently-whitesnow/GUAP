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

  getSummaryBooks = () => {
    this.clearStore();
    this.rootStore.stateStore.setIsLoading(true);
    api
      .getSummaryBooks("", false, 0, 10)
      .then(({ data }) => {
        this.summaryData = data;
        this.summaryData?.books?.forEach((book) => {
          book.image = setFile(book.files?.shift());
        });
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
