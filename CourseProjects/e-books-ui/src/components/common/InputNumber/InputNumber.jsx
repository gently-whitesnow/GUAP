import { observer } from "mobx-react-lite";
import { InputNumberWrapper } from "./InputNumber.styles";

const InputNumber = ({ onChange, value, placeholder }) => {
  const onChangeHandler = (e) => {
    console.log("dadad")
    onChange(e);
  };

  return (
    <InputNumberWrapper
      onChange={(e)=>onChangeHandler(e)}
      maxlength="8"
      placeholder={placeholder}
      value={value}
    />
  );
};

export default observer(InputNumber);
