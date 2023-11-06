import { useState } from "react";
import { ErrorLineHandlerContent, ErrorLine } from "./ErrorLineHandler.styles";
import { observer } from "mobx-react-lite";

const ErrorLineHandler = (props) => {
  return (
    <ErrorLineHandlerContent>
      {props.children}

      {props.error != "" ? (
        <ErrorLine onClick={() => props.setActionError("")}>{props.error}</ErrorLine>
      ) : null}
    </ErrorLineHandlerContent>
  );
};

export default ErrorLineHandler;
