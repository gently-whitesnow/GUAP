import { useEffect } from "react";
import { useStore } from "../../store";
import { AuthPageWrapper } from "./AuthPage.styles";
import { observer } from "mobx-react-lite";
import { useLocation, useNavigate } from "react-router-dom";

const AuthPage = () => {
  const { stateStore } = useStore();
  const { getAuth } = stateStore;

  const { search } = useLocation();

  useEffect(() => {
    const urlParams = new URLSearchParams(search);

    const userId = urlParams.get("userId");
    const userName = urlParams.get("userName");
    getAuth(userId, userName);
  }, []);

  return <AuthPageWrapper>Авторизация</AuthPageWrapper>;
};

export default observer(AuthPage);
