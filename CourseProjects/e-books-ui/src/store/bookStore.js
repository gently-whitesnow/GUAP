import { makeAutoObservable, configure } from "mobx";
import api from "../api/api";
import { setFile } from "../helpers/IOHelper";

class BookStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    this.stateStore = rootStore.stateStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  book = undefined;

  getBook = (id) => {
    this.rootStore.stateStore.setIsLoading(true);
    api
      .getBook(id)
      .then((data) => {
        this.book = data.data;
        console.log(this.book)
        this.book.image = setFile(this.book.files?.shift());
      })
      .catch((err) => {
        this.rootStore.stateStore.setIsLoading(false);
        console.error(err);
        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
      });
  };
}

export default BookStore;
