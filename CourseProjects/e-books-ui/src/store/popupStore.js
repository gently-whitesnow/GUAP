import { makeAutoObservable, configure } from "mobx";
import api from "../api/api";

class PopupStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    this.stateStore = rootStore.stateStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  isOpen = false;

  setIsOpen =(value)=>{
    this.isOpen = value;
  }

  id = undefined;
  title = "";
  author = "";
  description = "";
  count = 1;
  image = undefined;
  isNew = true;

  setPopupData = (id, title, author, decription,count, image) => {
    this.id = id;
    this.title = title;
    this.author = author;
    this.description = decription;
    this.count = count;
    this.image = image;
    this.isNew = false;
  };

  setTitle = (value) => {
    this.title = value;
  };

  setAuthor = (value) => {
    this.author = value;
  };

  setDescription = (value) => {
    this.description = value;
  };

  setCount = (value) => {
    this.count = value;
  };

  setImage = (value) => {
    this.image = value;
  };

  article = {};

  upsertBook = () => {
    this.rootStore.stateStore.setIsLoading(true);
    api
      .upsertBook(this.id, this.title, this.description, this.author, this.count, this.image)
      .then(() => {})
      .catch((err) => {
        this.rootStore.stateStore.setIsLoading(false);
        console.error(err);
        if (err.response?.status === 401) {
          this.rootStore.stateStore.setIsAuthorized(false);
        }
      });
  };

  clearStore = () => {
    this.id = undefined;
    this.title = "";
    this.author = "";
    this.description = "";
    this.count = 1;
    this.image = undefined;
    this.isNew = true;
  };
}

export default PopupStore;
