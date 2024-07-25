import { useEffect } from "react";
import { useStore } from "../../store";
import { AuthPageWrapper } from "./AuthPage.styles";
import { observer } from "mobx-react-lite";
import { useLocation } from "react-router-dom";

const AuthPage = () => {
  const { stateStore } = useStore();
  const { getFakeAuth } = stateStore;

  const { search } = useLocation();

  useEffect(() => {
    const urlParams = new URLSearchParams(search);

    const userId = urlParams.get("userId");
    const userName = urlParams.get("userName");
    const userRole = urlParams.get("userRole");
    getFakeAuth(userId, userName, userRole);
  }, []);

  return <AuthPageWrapper>Авторизация</AuthPageWrapper>;
};

export default observer(AuthPage);
