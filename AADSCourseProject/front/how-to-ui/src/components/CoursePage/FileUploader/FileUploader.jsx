import { useRef, useState } from "react";
import { FileUploaderWrapper } from "./FileUploader.styles";
import { observer } from "mobx-react-lite";

const FileUploader = (props) => {
  const [name, setName] = useState();

  const onChangeHandler = () => {
    setName(props.fileInputRef.current?.files[0]?.name);
  };
  return (
    <FileUploaderWrapper color={props.color}>
      <label for="myfile" className="chous">
        {name != undefined ? name : "Выберите md файл"}
      </label>
      <input
        type="file"
        className="my"
        id="myfile"
        name="myfile"
        ref={props.fileInputRef}
        onChange={onChangeHandler}
        accept=".md"
      />
    </FileUploaderWrapper>
  );
};

export default observer(FileUploader);
